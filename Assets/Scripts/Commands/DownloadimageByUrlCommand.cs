using System;
using System.Net;
using Assets.Scripts.Models;
using Assets.Scripts.Signals;
using strange.extensions.command.impl;
using UniRx;
using UnityEngine;

namespace Assets.Scripts.Commands
{
    public class DownloadImageByUrlCommand : Command
	{
		[Inject]
		public ImageLoadedSignal LoadedSignal { get; set; }
		
		[Inject]
		public ImageLoadErrorSignal LoadErrorSignal { get; set; }
		
		[Inject]
		public ICacheService CacheService { get; set; }
		
		[Inject]
		public string ImgUrl { get; set; }

		private const int MaxLoadTryCount = 3;
		
		public override void Execute()
		{	
			Retain();

			var errorMsg = "";
			ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
			
			Observable.Start(() =>
			{
				if (CacheService.Exist(ImgUrl))
				{
					return CacheService.Get(ImgUrl);
				}

				var hasError = false;
				var currentTryCount = 0;
				byte[] result = null;

				while (currentTryCount < MaxLoadTryCount)
				{
					currentTryCount++;

					try
					{
						var loader = new WebClient();
						result = loader.DownloadData(ImgUrl);
						hasError = false;
						break;
					}
					catch (Exception e)
					{
						errorMsg = e.Message;
						hasError = true;
					}
				}

				if (hasError)
				{
					return null;
				}

				CacheService.Save(ImgUrl, result);
				return result;
				
			}).ObserveOnMainThread().Subscribe(data => {
				
				if (data != null)
				{
					var tex = new Texture2D(2,2);
					tex.LoadImage(data);
					LoadedSignal.Dispatch(new AsyncImageData()
					{
                        url = ImgUrl,
                        texture = tex
					});
				}
				else
				{
                    Debug.LogError(errorMsg);
                    LoadErrorSignal.Dispatch();
				}
			});
		}
	}
}