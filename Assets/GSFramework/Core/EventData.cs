using GameSparks.Api.Responses;
using GameSparks.Core;
using UnityEngine;

namespace GSFramework
{
	/// <summary>
	/// Contain keys to identify event by types using dispatcher
	/// </summary>
	public class EventData 
	{
		#region Auth & connection

		public struct UpdateAuthStatus
		{
			public string StatusName;

			public UpdateAuthStatus(string statusName)
			{
				StatusName = statusName;
			}
		}

		public struct AuthenticatedEvent
		{
			public AuthenticationResponse Response;
			public enum AuthType
			{
				DeviceLogin,
				Login,
				GameCenter,
				Wechat,
				Facebook,
				Amazon
			}
			public AuthType UsedAuthentication;

			public AuthenticatedEvent(AuthenticationResponse msg, AuthType usedType)
			{
				Response = msg;
				UsedAuthentication = usedType;
			}
		}

		public struct AvaillableEvent
		{
			public bool AvaillableKey;
			public bool LoggedInKey;

			public AvaillableEvent(bool isAvaillable,bool isLoggedIn)
			{
				AvaillableKey = isAvaillable;
				LoggedInKey = isLoggedIn;
			}
		}

		public struct ConnectionDropped
		{

		}

		public struct OnLoginSelection
		{

		}

		public struct RequestDeviceLogin
		{
			public string DisplayName;

			public RequestDeviceLogin(string displayName)
			{
				DisplayName = displayName;
			}
		}

		public struct RequestRegisterNewAccount
		{
			public string DisplayName;
			public string UserName;
			public string Password;

			public RequestRegisterNewAccount(string displayName,string userName,string password)
			{
				DisplayName = displayName;
				UserName = userName;
				Password = password;
			}
		}

		public struct RequestAmazonConnect
		{
			public string AccessToken;

			public RequestAmazonConnect(string accessToken)
			{
				AccessToken = accessToken;
			}
		}
		
		public struct RequestWechatConnect
		{
			public string WechatId;

			public RequestWechatConnect(string wechatId)
			{
				WechatId = wechatId;
			}
		}
		
		public struct RequestGamecenterConnect
		{
			
		}

		public struct ErrorRegisterTaken
		{
			public string UserName;
			public ErrorRegisterTaken(string userName)
			{
				UserName = userName;
			}
		}

		public struct TryConnectFacebook
		{
			
		}

		#endregion

		#region Currency
		public struct CurrencyUpdateEvent
		{
			public int Currency1;
			public int Currency2;
			public int Currency3;

			public CurrencyUpdateEvent(int c1,int c2,int c3)
			{
				Currency1 = c1;
				Currency2 = c2;
				Currency3 = c3;
			}
		}
		#endregion

		#region account
		public struct ChangeUserDetailsEvent
		{
			public string DisplayName;
			public string Language;
			public string Password;
			public string OldPassword;
			public string Username;

			public ChangeUserDetailsEvent(string displayName, string language, string password,string oldPassword, string username)
			{
				DisplayName = displayName;
				Language = language;
				Password = password;
				OldPassword = oldPassword;
				Username = username;
			}
		}

		public struct AccountDetailsEvent
		{
			public string Country;
			public string City;
			public string UserId;

			public AccountDetailsEvent(string country,string city,string userId)
			{
				Country = country;
				City = city;
				UserId = userId;
			}
		}

		public struct SuccessChangeUserDetails
		{

		}
		
		public struct ErrorChangeDetailsTaken
		{

		}

		public struct ErrorChangeDetailsUnrecognised
		{

		}
		#endregion

		#region Mails
		/// <summary>
		/// sent by UI to the GSF_Mail class
		/// </summary>
		public struct RequestMailListEvent
		{
			public string Receiver;

			public RequestMailListEvent(string receiver)
			{
				Receiver = receiver;
			}
		}

		public struct RequestSendEmailEvent
		{
			public MailContent Mail;

			public RequestSendEmailEvent(MailContent mail)
			{
				Mail = mail;
			}
		}

		public struct RequestMailDeleteEvent
		{
			public string MailId;

			public RequestMailDeleteEvent(string mailId)
			{
				MailId = mailId;
			}
		}
		
		public struct RequestMailAttachmentEvent
		{
			public string MailId;

			public RequestMailAttachmentEvent(string mailId)
			{
				MailId = mailId;
			}
		}
		
		public struct ResponseRetreiveMailAttachmentEvent
		{
			public bool Success;
			public MailContent UpdatedMail;
			public long Currency1;
			public long Currency2;

			public ResponseRetreiveMailAttachmentEvent(bool success, MailContent mail, long currency1, long currency2)
			{
				Success = success;
				UpdatedMail = mail;
				Currency1 = currency1;
				Currency2 = currency2;
			}
		}

		/// <summary>
		/// Received from server to GSF_Mail, listen by UI_Mail
		/// </summary>
		public struct ResponseMailListEvent
		{
			public MailContent[] Mails;

			public ResponseMailListEvent(MailContent[] mails)
			{
				Mails = mails;
			}
		}

		public struct ResponseSendMailEvent
		{
			public bool Result;

			public ResponseSendMailEvent(bool result)
			{
				Result = result;
			}
		}

		public struct ResponseDeleteMailEvent
		{
			public bool Result;
			public MailContent[] Mails;

			public ResponseDeleteMailEvent(bool result, MailContent[] mails)
			{
				Result = result;
				Mails = mails;
			}
		}

		public struct OpenMailById
		{
			public string Id;

			public OpenMailById(string id)
			{
				Id = id;
			}
		}


		#endregion

		#region Team (team)
		public struct CurrentTeamRequest
		{

		}

		public struct CreateTeamRequest
		{
			public string TeamName;

			public CreateTeamRequest(string teamName)
			{
				TeamName = teamName;
			}
		}

		public struct CreateTeamResponse
		{
			public GSEnumerable<GameSparks.Api.Responses.CreateTeamResponse._Player> Members;
			public GameSparks.Api.Responses.CreateTeamResponse._Player Owner;
			public string ScriptData;
			public string TeamName;

			public CreateTeamResponse(GSEnumerable<GameSparks.Api.Responses.CreateTeamResponse._Player> members, GameSparks.Api.Responses.CreateTeamResponse._Player owner,string scriptData,string teamName)
			{
				Members = members;
				Owner = owner;
				ScriptData = scriptData;
				TeamName = teamName;
			} 
		}

		public struct JoinTeamResponse
		{
			public GameSparks.Api.Responses.JoinTeamResponse Response;
			public JoinTeamResponse(GameSparks.Api.Responses.JoinTeamResponse response)
			{
				Response =response;
			}
		}

		public struct GetCurrentTeamResponse
		{
			public GSEnumerable<GetMyTeamsResponse._Team> Teams;
			public GetCurrentTeamResponse(GSEnumerable<GetMyTeamsResponse._Team> teams)
			{
				Teams = teams;
			}
		}

		public struct LeaveTeamRequest
		{
			public string TeamId;
			public LeaveTeamRequest(string teamId)
			{
				TeamId = teamId;
			}
		}

		public enum LeaveTeamResponseType
		{
			Success,
			CannotLeaveOwnedTeam
		}

		public struct LeaveTeamResponse
		{
			public LeaveTeamResponseType Success;
			public LeaveTeamResponse(LeaveTeamResponseType success)
			{
				Success = success;
			}
		}

		public struct DropTeamRequest
		{
			public string TeamId;
			public DropTeamRequest(string teamId)
			{
				TeamId = teamId;
			}
		}

		public struct DropTeamResponse
		{

		}

		public struct RequestJoinTeam
		{
			public string TeamId;
			public RequestJoinTeam(string teamId)
			{
				TeamId = teamId;
			}
		}

		public struct GetTeamListRequest
		{

		}

		public struct GetTeamListResponse
		{
			public GSEnumerable<ListTeamsResponse._Team> Teams;
			public GetTeamListResponse(GSEnumerable<ListTeamsResponse._Team> teams)
			{
				Teams = teams;
			}
		}
		
		public struct ErrorAlreadyHaveTeam
		{

		}

		public struct ErrorTeamIsFull
		{
			
		}

		public struct ErrorOwnTeamIsFull
		{

		}

		public struct ErrorCantCreateTeam
		{
			public string Reason;

			public ErrorCantCreateTeam(string reason)
			{
				Reason = reason;
			}
		}
		#endregion
		
		#region General UI
		public struct OnDialogEvent
		{
			public DialogBoxData Data;

			public OnDialogEvent(DialogBoxData data)
			{
				Data = data;
			}
		}

		public struct NoticeMessage
		{
			public string Message1;
			public string Message2;
			public NoticeMessage(string msg1,string msg2)
			{
				Message1 = msg1;
				Message2 = msg2;
			}
		}

		public struct ShowHud
		{
			public bool Show;
			public ShowHud(bool show)
			{
				Show = show;
			}
		}
		#endregion

	

		#region Leaderboards
		public struct ArroundMeLeaderboardListRequest
		{
			public string LeaderboardName;

			public ArroundMeLeaderboardListRequest(string leaderboardName)
			{
				LeaderboardName = leaderboardName;
			}
		}


		public struct LeaderboardListRequest
		{
			public string LeaderboardName;

			public LeaderboardListRequest(string leaderboardName)
			{
				LeaderboardName = leaderboardName;
			}
		}

		public struct LeaderBoardXpTopListReponse
		{
			public LeaderboardEntry[] Entries;

			public LeaderBoardXpTopListReponse(LeaderboardEntry[] entries)
			{
				Entries = entries;
			}
		}
		#endregion

		#region Achievements
		public struct AchievementListRequest
		{
			
		}

		public struct AchievementListResponse
		{
			public AchievementEntry[] Entries;

			public AchievementListResponse(AchievementEntry[] entries)
			{
				Entries = entries;
			}
		}
		#endregion

		#region utility
		public struct PlaySoundEvent
		{
			public AudioClip SoundName;

			public PlaySoundEvent(AudioClip soundName)
			{
				SoundName = soundName;
			}
		}
		#endregion
	}
}
