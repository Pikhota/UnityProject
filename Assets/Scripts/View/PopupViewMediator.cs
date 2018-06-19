using Assets.Signals;
using Assets.Models;

namespace Assets.View
{
    public class PopupViewMediator : TargetMediator<PopupView>
    {
        [Inject]
        public ShowPopupSignal ShowPopupSignal { get; set; }

        public override void OnRegister()
        {
            ShowPopupSignal.AddListener(OnShowRequested);
        }

        public void OnShowRequested(object data)
        {
            View.Show((Popups)data);
        }
    }
}