using System.Collections.Generic;
using UnityEngine;
using GameSparks.Api.Messages;
using Dispatcher;

namespace GSFramework
{
	public class LeaderboardEntry
	{
		public int Rank;
		public string PlayerName;
		public int ScoreValue;

		public LeaderboardEntry(int rank, string playerName, int scoreValue)
		{
			Rank = rank;
			PlayerName = playerName;
			ScoreValue = scoreValue;
		}
	}
	public class GsfLeaderboard : Module 
	{
		#region Overrides
		protected override void Register ()
		{
			base.Register ();
			GlobalRankChangedMessage.Listener += RankChangedMessageHandler;
			NewHighScoreMessage.Listener += HighScoreMessageHandler;
			EventManager.Subscribe<EventData.LeaderboardListRequest> (GetLeaderboard);

		}

		protected override void UnRegister ()
		{
			base.UnRegister ();
			GlobalRankChangedMessage.Listener -= RankChangedMessageHandler;
			NewHighScoreMessage.Listener -= HighScoreMessageHandler;
			EventManager.Unsubscribe<EventData.LeaderboardListRequest> (GetLeaderboard);
		}
		#endregion
		#region Messages

		protected void RankChangedMessageHandler(GlobalRankChangedMessage message)
		{
			EventManager.SendEvent(new EventData.NoticeMessage(
				"[ "+message.LeaderboardName+" ]",
				"Your position is: "+message.You.Rank.ToString()
			));

		}

		protected void HighScoreMessageHandler (NewHighScoreMessage message)
		{
			EventManager.SendEvent(new EventData.NoticeMessage(
				"[ "+message.LeaderboardName+" ]",
				"Your Score is updated !")
			);

		}
		#endregion
		#region Requests
		public void GetLeaderboard(EventData.LeaderboardListRequest request)
		{
			new GameSparks.Api.Requests.LeaderboardDataRequest ()
				.SetLeaderboardShortCode (request.LeaderboardName)
				.SetEntryCount(Keys.LeaderBoardEntryCount)
				.Send ((response) => {

					if(!response.HasErrors)
					{
						var entries = new List<LeaderboardEntry>();	
						foreach(var entry in response.Data) // iterate through the leaderboard data
						{
							entries.Add(new LeaderboardEntry( 
								(int)entry.Rank,
								entry.UserName,
								int.Parse(entry.JSONData[Keys.LeaderboardXpAmount].ToString())));
							
						}

						EventManager.SendEvent(new EventData.LeaderBoardXpTopListReponse(entries.ToArray()));

					
					}
					else
					{
						Debug.Log("Error Retrieving Leaderboard Data...");
					}

				});
		}

		public void GetLeaderboardArroundMe(EventData.ArroundMeLeaderboardListRequest request)
		{
			new GameSparks.Api.Requests.AroundMeLeaderboardRequest ()
				.SetLeaderboardShortCode (request.LeaderboardName)
				.SetEntryCount(Keys.LeaderBoardEntryCount)
				.Send ((response) => {

					if(!response.HasErrors)
					{
						var entries = new List<LeaderboardEntry>();	
						foreach(var entry in response.Data) // iterate through the leaderboard data
						{
							entries.Add(new LeaderboardEntry( 
								(int)entry.Rank,
								entry.UserName,
								int.Parse(entry.JSONData[Keys.LeaderboardXpAmount].ToString())));

						}

						EventManager.SendEvent(new EventData.LeaderBoardXpTopListReponse(entries.ToArray()));
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




