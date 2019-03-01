using GameSparks.Api.Responses;
using GameSparks.Core;
using UnityEngine;

namespace GSFramework
{
    public class ClanEventData  
    {
        #region Clan

		public struct ReceiveClanRequests
		{
			public ClanRequestData[] Entries;

			public ReceiveClanRequests(ClanRequestData[] entries)
			{
				Entries = entries;
			}
		}

		public struct ClanDecisionRequest
		{
			public bool Decision;
			public string PlayerId;

			public ClanDecisionRequest(bool decision, string playerId)
			{
				Decision = decision;
				PlayerId = playerId;
			}
		}

		public struct UpdateClanMessageResponse
		{
			public string Message;

			public UpdateClanMessageResponse(string message)
			{
				Message = message;
			}
		}
		
		public struct RequestUpdateClanMessage
		{
			public string Message;

			public RequestUpdateClanMessage(string message)
			{
				Message = message;
			}
		}

		public struct DropClanRequest
		{
			
		}

		public struct DropClanResponse
		{
			
		}

		public struct LeaveClanRequest
		{
		}
		
		public struct LeaveClanResponse
		{
			public EventData.LeaveTeamResponseType Result;
			public LeaveClanResponse(EventData.LeaveTeamResponseType result)
			{
				Result = result;
			}
		}

	    public struct RequestKickPlayer
	    {
		    
	    }

	    public struct RequestJoinClan
		{
			public string ClanId;

			public RequestJoinClan(string clanId)
			{
				ClanId = clanId;
			}
		}
		
		public struct ResponseJoinClan
		{
			public string ClanId;
			public string Result;

			public ResponseJoinClan(string clanId,string result)
			{
				ClanId = clanId;
				Result = result;
			}
		}

		public struct OpenClanDetail
		{
			public PublicClanData ClanData;

			public OpenClanDetail(PublicClanData clanData)
			{
				ClanData = clanData;
			}
		}

		public struct SearchClansRequest
		{
			public string SearchedField;

			public SearchClansRequest(string searchedField)
			{
				SearchedField = searchedField;
			}
		}

		public struct SearchClansResponse
		{
			public PublicClanData[] Clans;

			public SearchClansResponse(PublicClanData[] clans)
			{
				Clans = clans;
			}
		}

		public struct GetClanRequest
		{
			
		}

		public struct GetClanResponse
		{
			public bool HasClan;
			public ClanData ClanData;

			public GetClanResponse(bool hasClan, ClanData clanData)
			{
				HasClan = hasClan;
				ClanData = clanData;
			}
		}

		public struct GetClanLogsResponse
		{
			public string[] Logs;

			public GetClanLogsResponse(string[] logs)
			{
				Logs = logs;
			}
		}

		public struct CreateClanRequest
		{
			public string ClanName;
			public string ClanMessage;

			public CreateClanRequest(string clanName,string clanMessage)
			{
				ClanName = clanName;
				ClanMessage = clanMessage;
			}
		}

		public struct CreateClanResponse
		{
			public ClanData Data;

			public CreateClanResponse(ClanData data)
			{
				Data = data;
			}
		}

		public struct CheckClanRequest
		{
			public string ClanName;

			public CheckClanRequest(string clanName)
			{
				ClanName = clanName;
			}
		}

		public struct CheckClanResponse
		{
			public bool Valid;

			public CheckClanResponse(bool valid)
			{
				Valid = valid;
			}
		}

		#endregion

    }
}


