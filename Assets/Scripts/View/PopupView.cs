using System.Collections.Generic;
using Assets.Models;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Assets.View
{
    public class PopupView : EventView
    {
        [SerializeField] private RectTransform PopupRoot;
        [SerializeField] private List<PopupPair> _popupsList = new List<PopupPair>();

        private Queue<Popups> _popupsQueue = new Queue<Popups>();
        private bool _popupAlreadyOnScene;
		
        public void Show(Popups popup)
        {
            if (_popupAlreadyOnScene)
            {
                _popupsQueue.Enqueue(popup);
            }
            else
            {
                PlacePopupOnScene(popup);
            }
        }

        private void PlacePopupOnScene(Popups popup)
        {
            _popupAlreadyOnScene = true;
            var prefab = _popupsList.Find(p => p.Type == popup).Prefab;
            var _instance = Instantiate(prefab);
            _instance.transform.SetParent(PopupRoot);
            _instance.transform.localScale = Vector3.one;
            _instance.transform.localPosition = Vector3.zero;
            
            _instance.Show(this);
        }
		
        public void Close(BasePopup popup)
        {
            Destroy(popup.gameObject);
            _popupAlreadyOnScene = false;
            if (_popupsQueue.Count != 0)
            {
                PlacePopupOnScene(_popupsQueue.Dequeue());
            }
        }

    }
}