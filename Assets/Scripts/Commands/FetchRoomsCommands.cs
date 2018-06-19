using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml.Serialization;
using Assets.Models;
using strange.extensions.command.impl;
using Assets.Signals;
using UniRx;
using UnityEngine;

namespace Assets.Commands
{
    /// <summary>
    /// Load rooms data from server and move to Data model. Server url stored in config passed as command line argument. 
    /// </summary>
    public class FetchRoomsCommands : Command
	{
		[Inject]
		public LoadImageSignal LoadImageSignal { get; set; }

		[Inject]
		public RoomsFetchedSignal RoomsFetchedSignal { get; set; }
		
		[Inject] 
		public Data Data { get; set; }

		private const string DefaultConfigPath = "config";
		
		public override void Execute()
		{
			var args = Environment.GetCommandLineArgs();
			var configPath = "";
			
			if (args.Length < 2 || args.ToList().IndexOf("-config") == -1)
			{
				configPath = DefaultConfigPath;
                Debug.LogError("missing config path. Use default instead");
            }
			else
			{
				var configIndex = args.ToList().IndexOf("-config");
				configPath = args[configIndex + 1];
			}


			var configTxt = Resources.Load<TextAsset>(configPath).text;
			var config = JsonUtility.FromJson<Config>(configTxt);
			
			Observable.Start(() =>
			{
				var response = "";
				using (var webClient = new WebClient())
				{
					response = webClient.DownloadString(config.url);
				}
	
				var serializer = new XmlSerializer(typeof(Data));
				Data roomModel;

				using (TextReader reader = new StringReader(response))
				{
					roomModel = (Data) serializer.Deserialize(reader);
				}

				Data.Settings = roomModel.Settings;
				Data.Games = roomModel.Games.Where(p => config.games.Contains(p.GameId)).ToArray();
				
			}).ObserveOnMainThread().Subscribe(r =>
			{
				var imagesToLoad = Data.Settings.Select(p => p.Value);
				foreach (var imgUrl in imagesToLoad)
				{
					LoadImageSignal.Dispatch(imgUrl);
				}
				
				RoomsFetchedSignal.Dispatch();
			});

		}
	}
}
