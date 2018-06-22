using Assets.Scripts.Models;
using strange.extensions.signal.impl;

namespace Assets.Scripts.Signals
{
    public class ImageLoadedSignal : Signal<AsyncImageData>
    {

    }

    public class ImageLoadErrorSignal : Signal
    {
        
    }
}