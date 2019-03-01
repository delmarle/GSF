using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Dispatcher;
using GameSparks.Api.Responses;

namespace GSFramework.UI
{
	 public class UiTeam : Module 
	{
		#region FIELDS
		public static UiTeam Instance;
	
		[Header("Tabs")]
		[SerializeField] private GameObject _loadingTab;
		[SerializeField] private GameObject _playerTeamTab;
		[SerializeField] private GameObject _leavingTab;
		[SerializeField] private GameObject _listTeamTab;
	
		[Header("Category: Player Team")]
		[SerializeField] private UIPanel _panel;
		[SerializeField] private Text _teamName;
		[SerializeField] private Text _teamLeader;
		[SerializeField] private Text _memberCount;
		private GenericUIList<GetMyTeamsResponse._Team._Player> _members;
		[SerializeField] private LayoutGroup _listMembersLayoutGroup;
		[SerializeField] private UiTeamMember _memberPrefab;
	
	
		[Header("Category: List of Teams")]
		[SerializeField] private UiTeamButton _teamPrefab;
		private GenericUIList<ListTeamsResponse._Team> _teams;
		[SerializeField] private LayoutGroup _listTeamLayoutGroup;
		[SerializeField] private Button _createTeamButton;
		[SerializeField] private InputField _createTeamName;
		[SerializeField] private Text _createTeamStatus;
		#endregion
		#region Overrides
		protected override void Register ()
		{
			base.Register ();
			EventManager.Subscribe<EventData.GetCurrentTeamResponse> (OnReceiveOwnTeamResponse);
			EventManager.Subscribe<EventData.LeaveTeamResponse> (OnLeaveTeamResponse);
			EventManager.Subscribe<EventData.GetTeamListResponse> (OnReceiveTeamListResponse);
			EventManager.Subscribe<EventData.JoinTeamResponse> (OnReceiveJoinTeamResponse);
			EventManager.Subscribe<EventData.CreateTeamResponse> (OnReceiveCreateTeamResponse);
			EventManager.Subscribe<EventData.ErrorCantCreateTeam> (OnCreateTeamError);
			EventManager.Subscribe<EventData.DropTeamResponse> (OnDropTeamResponse);
		}
	
	
		protected override void UnRegister ()
		{
			base.UnRegister ();
			EventManager.Unsubscribe<EventData.GetCurrentTeamResponse> (OnReceiveOwnTeamResponse);
			EventManager.Unsubscribe<EventData.LeaveTeamResponse> (OnLeaveTeamResponse);
			EventManager.Unsubscribe<EventData.GetTeamListResponse> (OnReceiveTeamListResponse);
			EventManager.Unsubscribe<EventData.JoinTeamResponse> (OnReceiveJoinTeamResponse);
			EventManager.Unsubscribe<EventData.CreateTeamResponse> (OnReceiveCreateTeamResponse);
			EventManager.Unsubscribe<EventData.ErrorCantCreateTeam> (OnCreateTeamError);
			EventManager.Unsubscribe<EventData.DropTeamResponse> (OnDropTeamResponse);
		}
		#endregion
	
		private void Awake()
		{
			Instance = this;
			_teams = new GenericUIList<ListTeamsResponse._Team>(_teamPrefab.gameObject, _listTeamLayoutGroup);
			_members = new GenericUIList<GetMyTeamsResponse._Team._Player> (_memberPrefab.gameObject, _listMembersLayoutGroup);
		}
		#region public UI calls
		public void OpenTeamWindow()
		{
			_panel.Show ();
			EventManager.SendEvent (new EventData.CurrentTeamRequest ());
			_loadingTab.SetActive (true);
			_leavingTab.SetActive (false);
			_listTeamTab.SetActive (false);
		}
	
		public void RequestJoinTeam(string teamId)
		{
			_loadingTab.SetActive (true);
			_listTeamTab.SetActive (false);
			EventManager.SendEvent (new EventData.RequestJoinTeam (teamId));
		}
	
		private void RequestTeamList()
		{
			EventManager.SendEvent (new EventData.GetTeamListRequest ());
			_loadingTab.SetActive (true);
		}
	
		public void LeaveTeamRequest()
		{
			_playerTeamTab.SetActive (false);
			_leavingTab.SetActive (true);
			EventManager.SendEvent (new EventData.LeaveTeamRequest (_teamName.text));
		}
	
		public void TryCreateTeam()
		{
			string teamName = _createTeamName.text;
			_createTeamButton.interactable = false;
			EventManager.SendEvent (new EventData.CreateTeamRequest (teamName));
			_createTeamStatus.text = "Creating ...";
		}
		#endregion
		#region UI updates
		private void SetTeamList(IEnumerable<ListTeamsResponse._Team> data)
		{
			_teams.Generate<UiTeamButton>(data, (entry, item) => { item.SetupTeamItem(entry); });
	
		}
	
		private void SetMembersList(IEnumerable<GetMyTeamsResponse._Team._Player> data)
		{
			_members.Generate<UiTeamMember>(data, (entry, item) => { item.SetupMemberItem(entry); });
	
		}
		#endregion
		#region GS UPDATES
		private void OnCreateTeamError(EventData.ErrorCantCreateTeam _event)
		{
			_createTeamStatus.text = "failed: "+ _event.Reason;
			_createTeamName.text = string.Empty;
			_createTeamButton.interactable = true;
		}
	
		private void OnReceiveCreateTeamResponse(EventData.CreateTeamResponse data)
		{
			
			_teamName.text = data.TeamName;
			_teamLeader.text = data.Owner.DisplayName;
			_createTeamStatus.text = "Team "+ data.TeamName+" created !";
			_createTeamName.text = string.Empty;
	
			EventManager.SendEvent (new EventData.CurrentTeamRequest ());
			//
			_listTeamTab.SetActive (false);
			_loadingTab.SetActive (false);
			_playerTeamTab.SetActive (true);
	
		}
		private void OnReceiveJoinTeamResponse(EventData.JoinTeamResponse data)
		{
			_loadingTab.SetActive (false);
			_playerTeamTab.SetActive (true);
			_teamName.text = data.Response.TeamName;
			_teamLeader.text = data.Response.Owner.DisplayName;
			EventManager.SendEvent (new EventData.CurrentTeamRequest ());
		}
	
		private void OnReceiveOwnTeamResponse(EventData.GetCurrentTeamResponse response)
		{
			bool hasTeam = false;
			var enumerator = response.Teams.GetEnumerator ();
			while(enumerator.MoveNext())
			{
				hasTeam= true;
				DataManager.Instance.Add_BOOL ("hasTeam", true);
				if (enumerator.Current != null)
				{
					_loadingTab.SetActive (false);
					_playerTeamTab.SetActive (true);
					_teamName.text = enumerator.Current.TeamName;
					_teamLeader.text = enumerator.Current.Owner.DisplayName;
					var memberEnumerator = enumerator.Current.Members.GetEnumerator ();
					int playersCount = 0;
					List<GetMyTeamsResponse._Team._Player> memberList = new List<GetMyTeamsResponse._Team._Player> ();
					while (memberEnumerator.MoveNext ()) 
					{
						playersCount++;
						memberList.Add (memberEnumerator.Current);
					}
					SetMembersList (memberList);
					enumerator.Dispose();
					memberEnumerator.Dispose();
					_memberCount.text = playersCount+" Members";
				} 
				/*else 
				{
					RequestClanList ();
					loadingTab.SetActive (false);
					listClanTab.SetActive (true);
					print ("we dont have team");
				}
				*/
			}
	
			if (hasTeam == false) 
			{
				RequestTeamList ();
				_playerTeamTab.SetActive (false);
				_listTeamTab.SetActive (true);
				_createTeamButton.interactable = true;
				// no clan
			}
		}
	
		private void OnLeaveTeamResponse(EventData.LeaveTeamResponse response)
		{
			if (response.Success == EventData.LeaveTeamResponseType.Success) 
			{
				print ("success left");
				_leavingTab.SetActive (false);
				_listTeamTab.SetActive (true);
	
				DataManager.Instance.DeleteKey_BOOL ("hasTeam");
			} else
			{
				if (response.Success == EventData.LeaveTeamResponseType.CannotLeaveOwnedTeam) 
				{
					//Display Popup [ drop][cancel]
					var dialogData = DialogBoxData.CreateActionBox
						(
							"You cant leave a team as owner",
							() =>
							{
								EventManager.SendEvent (new EventData.DropTeamRequest (_teamName.text));
								_leavingTab.SetActive (true);
							},
							() =>
							{
								_playerTeamTab.SetActive (true);
								_leavingTab.SetActive (false);
								_listTeamTab.SetActive (false);
							},
							"Terminate Team"
						);
					EventManager.SendEvent (new EventData.OnDialogEvent (dialogData));
				}
	
			}
		}
	
		private void OnDropTeamResponse(EventData.DropTeamResponse response)
		{
			DataManager.Instance.DeleteKey_BOOL ("hasTeam");
			_createTeamButton.interactable = true;
			EventManager.SendEvent (new EventData.GetTeamListRequest ());
		}
	
		private void OnReceiveTeamListResponse(EventData.GetTeamListResponse response)
		{
			_loadingTab.SetActive (false);
			_leavingTab.SetActive (false);
			DataManager.Instance.DeleteKey_BOOL ("hasTeam");
			var listTeams = new List<ListTeamsResponse._Team> ();
			var enumerator = response.Teams.GetEnumerator ();
			while (enumerator.MoveNext ()) 
			{
				
				if (enumerator.Current != null) 
				{
					
					listTeams.Add (enumerator.Current);
				}
			}
			SetTeamList (listTeams);
			enumerator.Dispose();
			_createTeamButton.interactable = true;
			_createTeamStatus.text = "";
			_listTeamTab.SetActive (true);
		}
		#endregion
	}
}


