using Assets.Models;
using strange.extensions.signal.impl;

namespace Assets.Signals
{
    public class ImageLoadedSignal : Signal<AsyncImageData>
    {

    }

    public class ImageLoadErrorSignal : Signal
    {
        
    }
}