using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.View
{
    public class AsyncImageView : EventView
    {
        enum ImageState
        {
            Loading,
            Loaded
        }
		
        private string _url;

        [SerializeField] private GameObject _loadingState;
        [SerializeField] private GameObject _normalState;
        [SerializeField] private RawImage _img;
		
		
        public void InitUrl(string url)
        {
            var cache = AppContext.Get<ICacheService>();
            
            _url = url;
            if (cache.Exist(url))
            {
                var tex = new Texture2D(2,2);
                tex.LoadImage(cache.Get(url));
                _img.texture = tex;
                SetLoadingState(ImageState.Loaded);
            }
            else
            {
                SetLoadingState(ImageState.Loading);
            }
        }
		
        public void OnImageLoaded(string url, Texture2D texture)
        {			
            if (_url == url)
            {
                SetLoadingState(ImageState.Loaded);
                _img.texture = texture;
            }
        }

        private void SetLoadingState(ImageState state)
        {
            _loadingState.SetActive(state == ImageState.Loading);
            _normalState.SetActive(state == ImageState.Loaded);
        }

    }
}