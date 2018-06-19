using Assets.Models;
using Assets.Signals;

namespace Assets.View
{
    public class AsyncImageMediator : TargetMediator<AsyncImageView>
    {
        [Inject]
        public ImageLoadedSignal ImageLoadedSignal { get; set; }

        public override void OnRegister()
        {
            ImageLoadedSignal.AddListener(OnImageLoaded);
        }
        
        public void OnImageLoaded(AsyncImageData imgData)
        {
            View.OnImageLoaded(imgData.url, imgData.texture);
        }
    }
}