using strange.extensions.mediation.impl;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.View
{
    public abstract class BasePopup : EventView
    {
        [SerializeField] private Button CloseButton;
        [SerializeField] private Button CreateServerBtn;
        [SerializeField] private Button JoinServerBtn;

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

            if(CreateServerBtn != null)
            {
                CreateServerBtn.OnClickAsObservable().Subscribe(s => 
                {

                });
            }

            if (JoinServerBtn != null)
            {

            }
        }
    }
}