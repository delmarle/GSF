using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Dispatcher;

namespace GSFramework.UI
{
	public class UiLeaderboard : Module
	{
		#region FIELDS
		[SerializeField] private UIPanel _panel;
		//XP
		[SerializeField] private UiLeaderboardEntry _xpEntryPrefab;
		[SerializeField] private LayoutGroup _xpLayoutGroup;
		private GenericUIList<LeaderboardEntry> _xpEntries;
		#endregion

		protected void Awake()
		{
			_xpEntries = new GenericUIList<LeaderboardEntry>(_xpEntryPrefab.gameObject, _xpLayoutGroup);
		}

		#region Overrides
		protected override void Register ()
		{
			base.Register ();
			EventManager.Subscribe<EventData.LeaderBoardXpTopListReponse> (OnLeaderboardXpResponse);
		}

		protected override void UnRegister ()
		{
			base.UnRegister ();
			EventManager.Unsubscribe<EventData.LeaderBoardXpTopListReponse> (OnLeaderboardXpResponse);
		}
		#endregion
		#region UI calls
		public void OpenLeaderboardXp()
		{
			GetLeaderBoardData ();
			_panel.Show ();
		}

		private void SetList(IEnumerable<LeaderboardEntry> data)
		{
			_xpEntries.Generate<UiLeaderboardEntry>(data, (entry, item) => { item.SetupEntry(entry); });

		}

		public void GetLeaderBoardData()
		{
			EventManager.SendEvent (new EventData.LeaderboardListRequest (Keys.LeaderboardXp));
		}

		public void GetLeaderBoardArroundMeData()
		{
			EventManager.SendEvent (new EventData.ArroundMeLeaderboardListRequest (Keys.LeaderboardXp));
		}
		#endregion
		#region Responses from GsfLeaderboard
		private void OnLeaderboardXpResponse(EventData.LeaderBoardXpTopListReponse response)
		{
			SetList (response.Entries);
		}
		#endregion
	}
}


