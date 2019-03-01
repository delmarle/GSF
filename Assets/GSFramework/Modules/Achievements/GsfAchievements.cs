using System.Collections.Generic;
using UnityEngine;
using GameSparks.Api.Messages;
using GameSparks.Api.Requests;
using Dispatcher;

namespace GSFramework
{
	public class AchievementEntry
	{
		public string Name;
		public string Description;
		public bool Earned;

		public AchievementEntry(string name, string description, bool earned)
		{
			Name = name;
			Description = description;
			Earned = earned;
		}
	}

	public class GsfAchievements : Module 
	{
		#region OVERRIDES
		protected override void Register ()
		{
			base.Register ();
			AchievementEarnedMessage.Listener += AchievementEarnedMessageHandler;
			EventManager.Subscribe<EventData.AchievementListRequest> (RequestAchivementList);
		}

		protected override void UnRegister ()
		{
			base.UnRegister ();
			AchievementEarnedMessage.Listener -= AchievementEarnedMessageHandler;
			EventManager.Unsubscribe<EventData.AchievementListRequest> (RequestAchivementList);
		}
		#endregion


		protected void AchievementEarnedMessageHandler(AchievementEarnedMessage message)
		{
			EventManager.SendEvent(new EventData.NoticeMessage(
				"[ "+message.AchievementName+" ]",
				"Your earned: "+message.CurrencyAwards.JSON
			));
		}

		#region Requests

		protected void RequestAchivementList(EventData.AchievementListRequest request)
		{
			new ListAchievementsRequest()
				.Send ((response) => {

					if(!response.HasErrors)
					{
						var entries = new List<AchievementEntry>();	
						foreach(var entry in response.Achievements) // iterate through the leaderboard data
						{
							entries.Add(new AchievementEntry( 
								entry.Name,
								entry.Description,
								entry.Earned != null && entry.Earned.Value
							));

						}

						EventManager.SendEvent(new EventData.AchievementListResponse(entries.ToArray()));


					}
					else
					{
						Debug.Log("Error Retrieving Leaderboard Data...");
					}

				});
		}
		#endregion


	}
}

