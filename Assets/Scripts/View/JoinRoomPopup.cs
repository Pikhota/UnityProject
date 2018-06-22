using Assets.Scripts.Models;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.View
{
    public class JoinRoomPopup : BasePopup
    {
        [SerializeField] private TextMeshProUGUI _roomName;
        
        public override void Show(PopupView rootView)
        {
            base.Show(rootView);
            _roomName.text = AppContext.Get<GameState>().SelectedRoom;
        }
    }
}