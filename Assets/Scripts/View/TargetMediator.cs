using strange.extensions.mediation.impl;

namespace Assets.Scripts.View
{
    public class TargetMediator<T> : EventMediator
        where T : EventView
    {
        [Inject]
        public T View { get; set; }
    }
}