using System.Collections.Generic;
using System.Linq;
using Assets.Models;
using strange.extensions.mediation.impl;
using Assets.Signals;
using TMPro;
using UnityEngine;

namespace Assets.View
{
    public class RoomsView : EventView
	{
		[SerializeField] private TMP_Dropdown _currentGameView;
		
		[SerializeField] private SuperGameViewIem _superGamePrefab;
		[SerializeField] private RoomViewItem _roomPrefab;
		[SerializeField] private RectTransform _root;

		private List<GameObject> _activeItems = new List<GameObject>();
		
		[Inject]
		public Data Data { get; set; }
		
		private void OnEnable()
		{
			_currentGameView.onValueChanged.AddListener((index) => OnCurrentGameChanged(index));
		}

		private void OnDisable()
		{
			_currentGameView.onValueChanged.RemoveAllListeners();
		}

		public void OnRoomsFetched(Data data)
		{
			if (data.Games.Any())
			{
				_currentGameView.options = data.Games.Select(p => new TMP_Dropdown.OptionData(p.Name)).ToList();
				_currentGameView.value = 0;
				
				OnCurrentGameChanged(0);
			}
		}

		private void OnCurrentGameChanged(int gameIndex)
		{
			_activeItems.ForEach(p => Destroy(p.gameObject));
			_activeItems.Clear();

			foreach (var item in Data.Games[gameIndex].GameItems)
			{
				if (item is SuperGame)
				{
                    SuperGameViewIem _instance = Instantiate(_superGamePrefab);
					_instance.Init((SuperGame)item);
                    _instance.transform.SetParent(_root.transform);
					_instance.transform.localScale = Vector3.one;
					_activeItems.Add(_instance.gameObject);
				}
				else if (item is Room)
				{
                    RoomViewItem _instance = Instantiate(_roomPrefab);
					_instance.Init((Room)item);
					_instance.transform.SetParent(_root.transform);
					_instance.transform.localScale = Vector3.one;
					_activeItems.Add(_instance.gameObject);
				}
			}
		}
	}

	public class RoomsViewMediator : TargetMediator<RoomsView>
	{
		[Inject]
		public RoomsFetchedSignal RoomsFetchedSignal { get; set; }
		
		[Inject]
		public Data Data { get; set; }

		public override void OnRegister()
		{
			RoomsFetchedSignal.AddListener(OnRoomsFetched);	
		}

		public void OnRoomsFetched()
		{
			View.OnRoomsFetched(Data);
		}
	}
}