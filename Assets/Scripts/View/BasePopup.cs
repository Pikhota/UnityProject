using strange.extensions.mediation.impl;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.View
{
    public abstract class BasePopup : EventView
    {
        [SerializeField] private Button CloseButton;

        private PopupView _rootView;
		
        public virtual void Show(PopupView rootView)
        {
            _rootView = rootView;
            if (CloseButton != null)
            {
                CloseButton.OnClickAsObservable().Subscribe(r =>
                {
                    _rootView.Close(this);
                });
            }
        }
    }
}