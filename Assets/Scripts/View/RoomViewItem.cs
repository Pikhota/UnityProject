using System.Linq;
using Assets.Models;
using Assets.Signals;
using TMPro;
using UniRx;
using UnityEngine;
using Button = UnityEngine.UI.Button;


namespace Assets.View
{
    public class RoomViewItem : ViewWithModel<Models.Room>
	{
		[SerializeField] private AsyncImageView _img;
		[SerializeField] private TextMeshProUGUI _players;
		[SerializeField] private TextMeshProUGUI _roomName;
		[SerializeField] private GameObject _fullState;
		[SerializeField] private Button _joinRoom;

		enum RoomViewState
		{
			Full,
			Join
		}
		
		public override void Init(Room _model)
		{
			base.Init(_model);
			
			_img.InitUrl(AppContext.Get<Data>().Settings.First(p => p.Name == Constants.PlayerIcon).Value);
			_players.text = Model.Players.ToString();
			_roomName.text = Model.Name;
			
			_joinRoom.OnClickAsObservable().Subscribe(p =>
			{
				AppContext.Get<GameState>().SelectedRoom = _model.Name;
				AppContext.Get<ShowPopupSignal>().Dispatch(Popups.JoinRoomPopup);
			});
			
			SetRoomState(_model.Players == _model.MaxPlayers ? RoomViewState.Full : RoomViewState.Join);

		}

		private void SetRoomState(RoomViewState _state)
		{
			_fullState.SetActive(_state == RoomViewState.Full);
			_joinRoom.gameObject.SetActive(_state == RoomViewState.Join);
		}
	}
}
