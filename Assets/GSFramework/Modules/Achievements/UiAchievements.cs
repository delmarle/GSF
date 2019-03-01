using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Dispatcher;
using GameSparks.Api.Messages;

namespace GSFramework.UI
{
	public class UiAchievements : Module 
	{
		#region FIELDS
		[SerializeField] private UIPanel _panel;


		//xp section
		[SerializeField] private UiAchievementEntry _entryPrefab;
		[SerializeField] private LayoutGroup _layoutGroup;
		private GenericUIList<AchievementEntry> _entries;
		#endregion

		private void Awake()
		{
			_entries = new GenericUIList<AchievementEntry>(_entryPrefab.gameObject, _layoutGroup);
		}
		#region OVERRIDES
		protected override void Register ()
		{
			base.Register ();
			AchievementEarnedMessage.Listener += OnAchievementEarned;
			EventManager.Subscribe<EventData.AchievementListResponse> (OnGetAchievementList);
		}

		private void OnAchievementEarned(AchievementEarnedMessage message)
		{
			EventManager.SendEvent(new EventData.NoticeMessage("Achievement Earned:",message.AchievementName));
			if(_panel.IsVisible)
				EventManager.SendEvent (new EventData.AchievementListRequest ());
		}

		protected override void UnRegister ()
		{
			base.UnRegister ();
			EventManager.Unsubscribe<EventData.AchievementListResponse> (OnGetAchievementList);
		}
		#endregion
		#region UI CALLS
		public void OnOpenAchievementPanel()
		{
			EventManager.SendEvent (new EventData.AchievementListRequest ());
			_panel.Show ();
		}
		#endregion
		#region Response
		private void OnGetAchievementList(EventData.AchievementListResponse response)
		{
			SetList (response.Entries);
		}

	
	

		private void SetList(IEnumerable<AchievementEntry> data)
		{
			_entries.Generate<UiAchievementEntry>(data, (entry, item) => {item.SetupEntry(entry); 
			});
		}
		#endregion


	}
}

