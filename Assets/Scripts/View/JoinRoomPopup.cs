using Assets.Models;
using TMPro;
using UnityEngine;

namespace Assets.View
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