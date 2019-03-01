
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using Dispatcher;

using UnityEngine.UI;


namespace GSFramework.UI
{
    public class UiClan : Module 
    {
        #region FIELDS
        [SerializeField] private GameObject _overlayLoading;
        [SerializeField] private GameObject _tabClanInfo;
        [SerializeField] private GameObject _tabClanMembers;
        [SerializeField] private GameObject _tabClanRequests;
        [SerializeField] private GameObject _tabClanCreate;
        [SerializeField] private GameObject _tabClanSearch;
        [SerializeField] private UIPanel _panelClan;

        [Header("menu Buttons: ")] 
        [SerializeField] private Button _btnClanInfo;
        [SerializeField] private Button _btnMembers;
        [SerializeField] private Button _btnRequests;
        [SerializeField] private Button _btnSearchClan;
        [SerializeField] private Button _btnCreateClan;
        
        //SEARCH
        [SerializeField] private Button _searchButton;
        [SerializeField] private InputField _searchInputField;
        [SerializeField] private UiPublicClanEntry _mailPrefab;
        [SerializeField] private LayoutGroup _layoutGroup;
        private GenericUIList<PublicClanData> _foundClans;
        
        
        //Members
        [SerializeField] private Text _clanMembersCountText;
        [SerializeField] private UiPrivateClanMemberEntry _memberPrefab;
        [SerializeField] private LayoutGroup _layoutGroupMembers;
        private GenericUIList<ClanMemberData> _clanMembersList;
        
        //Requests
        [SerializeField] private UiClanRequestEntry _requestPrefab;
        [SerializeField] private LayoutGroup _layoutGroupRequests;
        private GenericUIList<ClanRequestData> _clanRequestsList;
        
        //Logs 
        [SerializeField] private Text _logPrefab;
        [SerializeField] private LayoutGroup _layoutGroupLogs;
        private GenericUIList<string> _logsList;
        
        
        [Header("Create clan: ")] 
        [SerializeField] private InputField _createClanName;
        [SerializeField] private InputField _createClanMessage;
        [SerializeField] private Button _createClanButton;
        [SerializeField] private Text _checkClanStatus;
        
        [Header("Clan Infos: ")]
        [SerializeField] private Text _clanInfoName;
        [SerializeField] private Text _clanInfoMembersText;
        [SerializeField] private Text _clanInfoRankText;
        [SerializeField] private Slider _clanInfoRankSlider;
        [SerializeField] private Text _clanInfoMessageText;
        [SerializeField] private Button _leaveClanButton;

        [Header("Clan Details: ")] 
        [SerializeField] private UIPanel _panelClanDetails;
        [SerializeField] private Text _clanDetailsName;
        [SerializeField] private Text _clanDetailsMembers;
        [SerializeField] private Text _clanDetailsMessage;
        [SerializeField] private Button _clanDetailsRequestButton;
        
        /// <summary>
        /// the current clan we have opened
        /// </summary>
        private PublicClanData _cachedClanDetails;
        #endregion
        
        #region Helpers
        private ClanData GetClan()
        {
            
            if (DataManager.Instance.HasKey_Object("clan"))
            {
                var ob = DataManager.Instance.Get_Object("clan");
                return (ClanData) ob;
            }
            else
            {
                return null;
            }
        }

        #endregion
        
	    #region OVERRIDES

        private void Awake()
        {
            _foundClans = new GenericUIList<PublicClanData>(_mailPrefab.gameObject, _layoutGroup);
            _clanMembersList = new GenericUIList<ClanMemberData>(_memberPrefab.gameObject,_layoutGroupMembers);
            _clanRequestsList = new GenericUIList<ClanRequestData>(_requestPrefab.gameObject,_layoutGroupRequests);
            _logsList = new GenericUIList<string>(_logPrefab.gameObject,_layoutGroupLogs);
        }

        protected override void Register()
        {
           EventManager.Subscribe<ClanEventData.GetClanResponse> (OnReceivePlayerClan);
           EventManager.Subscribe<ClanEventData.SearchClansResponse>(OpenSearchClans);
           EventManager.Subscribe<ClanEventData.CheckClanResponse>(CheckClanResponse);
           EventManager.Subscribe<ClanEventData.CreateClanResponse>(CreateClanResponse);
           EventManager.Subscribe<ClanEventData.OpenClanDetail>(OpenClanDetails);
           EventManager.Subscribe<ClanEventData.ResponseJoinClan>(ResponseJoinClanRequest);
           EventManager.Subscribe<ClanEventData.LeaveClanResponse>(LeaveClanResponse);
           EventManager.Subscribe<ClanEventData.DropClanResponse>(DropClanResponse);
           EventManager.Subscribe<ClanEventData.UpdateClanMessageResponse>(UpdateClanMessageResponse);
           EventManager.Subscribe<ClanEventData.ReceiveClanRequests>(ReceiveClanRequests);
           EventManager.Subscribe<ClanEventData.GetClanLogsResponse>(ReceiveClanLogs);
        }

        protected override void UnRegister()
        {
            EventManager.Unsubscribe<ClanEventData.GetClanResponse> (OnReceivePlayerClan);
            EventManager.Unsubscribe<ClanEventData.SearchClansResponse>(OpenSearchClans);
            EventManager.Unsubscribe<ClanEventData.CheckClanResponse>(CheckClanResponse);
            EventManager.Unsubscribe<ClanEventData.CreateClanResponse>(CreateClanResponse);
            EventManager.Unsubscribe<ClanEventData.OpenClanDetail>(OpenClanDetails);
            EventManager.Unsubscribe<ClanEventData.ResponseJoinClan>(ResponseJoinClanRequest);
            EventManager.Unsubscribe<ClanEventData.DropClanResponse>(DropClanResponse);
            EventManager.Unsubscribe<ClanEventData.UpdateClanMessageResponse>(UpdateClanMessageResponse);
            EventManager.Unsubscribe<ClanEventData.ReceiveClanRequests>(ReceiveClanRequests);
            EventManager.Unsubscribe<ClanEventData.GetClanLogsResponse>(ReceiveClanLogs);
        }

        #endregion
        #region OPEN/Close
        private void CloseAllTabs()
        {
            _tabClanInfo.SetActive(false);
            _tabClanMembers.SetActive(false);
            _tabClanCreate.SetActive(false);
            _tabClanSearch.SetActive(false);
            _tabClanRequests.SetActive(false);
        }

        public void OpenClanWindow()
        {
            _panelClan.Show();
            CloseAllTabs();
            _overlayLoading.SetActive(true);
            EventManager.SendEvent(new ClanEventData.GetClanRequest());
        }
        
        public void OpenClanInfoTab()
        {
            _tabClanInfo.SetActive(true);
            _tabClanMembers.SetActive(false);
            _tabClanCreate.SetActive(false);
            _tabClanSearch.SetActive(false);
            _tabClanRequests.SetActive(false);
        }

        public void OpenClanMembersTab()
        {
            _tabClanInfo.SetActive(false);
            _tabClanMembers.SetActive(true);
            _tabClanCreate.SetActive(false);
            _tabClanSearch.SetActive(false);
            _tabClanRequests.SetActive(false);
        }
        
        public void OpenRequestTab()
        {
            _tabClanMembers.SetActive(false);
            _tabClanInfo.SetActive(false);
            _tabClanCreate.SetActive(false);
            _tabClanSearch.SetActive(false);
            _tabClanRequests.SetActive(true);
            //generate members list
        }  
        
        public void OpenSearchClanTab()
        {
       
            _tabClanInfo.SetActive(false);
            _tabClanMembers.SetActive(false);
            _tabClanCreate.SetActive(false);
            _tabClanSearch.SetActive(true);
            _tabClanRequests.SetActive(false);
        }
        
        public void OpenCreateClanTab()
        {
            //Reset
            _createClanMessage.text = "Clan message...";
            _createClanName.text = "";
            _checkClanStatus.text = "";
            _createClanButton.interactable = false;
            
            
            _tabClanInfo.SetActive(false);
            _tabClanMembers.SetActive(false);
            _tabClanCreate.SetActive(true);
            _tabClanSearch.SetActive(false);
        }
        #endregion
        private void OnReceivePlayerClan(ClanEventData.GetClanResponse response)
        {
            //setup 
            if (response.HasClan)
            {
                _btnClanInfo.interactable = true;
                _btnMembers.interactable = true;
                _btnRequests.interactable = response.ClanData.RequestResult == "success";
                _btnSearchClan.interactable = true;
                _btnCreateClan.interactable = false;
                
                _tabClanInfo.SetActive(true);
                LoadPlayerClan(response.ClanData);
            }
            else
            {
                _btnClanInfo.interactable = false;
                _btnMembers.interactable = false;
                _btnRequests.interactable = false;
                _btnSearchClan.interactable = true;
                _btnCreateClan.interactable = true;
                
            }
            print(response.ToString());
            _overlayLoading.SetActive(false);
        }
        #region search
        private void OpenSearchClans(ClanEventData.SearchClansResponse response)
        {
          
            SetClanSearchedList (response.Clans);
            bool hasClan = GetClan() != null;
            
            _btnClanInfo.interactable = hasClan;
            _btnMembers.interactable = hasClan;
            _btnRequests.interactable = hasClan;
            _btnSearchClan.interactable = true;
            _btnCreateClan.interactable = !hasClan;
                
            _tabClanInfo.SetActive(false);
            _overlayLoading.SetActive(false);
            _tabClanMembers.SetActive(false);
            _tabClanCreate.SetActive(false);
            _tabClanSearch.SetActive(true);
            _searchButton.interactable = true;
        }
        
        private void SetClanSearchedList(IEnumerable<PublicClanData> data)
        {
            _foundClans.Generate<UiPublicClanEntry>(data, (entry, item) => { item.Setup(entry); });

        }

        public void SearchClanRequest()
        {
            _searchButton.interactable = false;
            EventManager.SendEvent(new ClanEventData.SearchClansRequest(_searchInputField.text));
        }

        private void SetClanLogList(string[] logs)
        {
            _logsList.Generate<Text>(logs,(entry, item) => { item.text = entry; });
        }

        #endregion
        
        #region create
        
        public void OnClickCreateClan()
        {
            _createClanButton.interactable = false;
            EventManager.SendEvent(new ClanEventData.CreateClanRequest(_createClanName.text,_createClanMessage.text));
        }

        public void CreateClanResponse(ClanEventData.CreateClanResponse response)
        {
            if (response.Data == null)
            {
                //failed
               // response.Data.
            }
            else
            {
                //success
                _btnClanInfo.interactable = true;
                _btnMembers.interactable = true;
                _btnRequests.interactable = true;
                _btnSearchClan.interactable = true;
                _btnCreateClan.interactable = false;
                
                _tabClanSearch.SetActive(false);
                _tabClanCreate.SetActive(false);
                _tabClanInfo.SetActive(true);
                LoadPlayerClan(response.Data);
            }
        }

        public void OnClickCheckClan()
        {
            if (_createClanName.text == "")
            {
                _checkClanStatus.text = "name invalid";
                return;
            }

            _createClanButton.interactable = false;
            _checkClanStatus.text = "checking";
            EventManager.SendEvent(new ClanEventData.CheckClanRequest(_createClanName.text));
        }

        private void CheckClanResponse(ClanEventData.CheckClanResponse response)
        {
            if (response.Valid)
            {
                _createClanButton.interactable = true;
                _checkClanStatus.text = "Clan name valid, Click Create to proceed.";
            }
            else
            {
                _checkClanStatus.text = "Clan name is invalid.";
            }
        }

        #endregion

        private void LoadPlayerClan(ClanData data)
        {
            _clanInfoName.text = data.ClanName;
            _clanInfoMembersText.text = "Members: "+data.PlayerCount + "/10";
            _clanMembersCountText.text = "Members: "+data.PlayerCount + "/10";
            _clanInfoRankText.text = data.XpCurrent + "/" + data.XpMax;
            _clanInfoRankSlider.value = data.XpCurrent;
            _clanInfoRankSlider.maxValue = data.XpMax;
            _clanInfoMessageText.text = data.ClanMessage;
            SetPrivateMemberList(data.Members);
           
            if(data.PlayersRequest != null)
            SetClanRequestsList(data.PlayersRequest);
            else
            {
                SetClanRequestsList(new List<ClanRequestData>());
            }
        }

        private void ReceiveClanLogs(ClanEventData.GetClanLogsResponse response)
        {
            SetClanLogList(response.Logs.ToArray());
        }

        private void ReceiveClanRequests(ClanEventData.ReceiveClanRequests response)
        {
            SetClanRequestsList(response.Entries);
            
        }

        private void SetPrivateMemberList(IEnumerable<ClanMemberData> data)
        {
            _clanMembersList.Generate<UiPrivateClanMemberEntry>(data, (entry, item) => { item.Setup(entry); });
        }
        
        private void SetClanRequestsList(IEnumerable<ClanRequestData> data)
        {
            if (data == null)
                return;
            _clanRequestsList.Generate<UiClanRequestEntry>(data, (entry, item) => { item.Setup(entry); });
        }
        #region Clan Info

        private void UpdateClanMessageResponse(ClanEventData.UpdateClanMessageResponse response)
        {
            _clanInfoMessageText.text = response.Message;
        }

        public void OpenEditClanPopup()
        {
            var dialogData = DialogBoxData.CreateTextInput
            (
                "Type your new clan message:",
                msg =>
                {
                    EventManager.SendEvent(new ClanEventData.RequestUpdateClanMessage(msg));
                    _clanInfoMessageText.text = "updating...";
                },
                "Update Message"
            );
            EventManager.SendEvent (new EventData.OnDialogEvent (dialogData));
        }

        public void LeaveClanRequest()
        {
            _leaveClanButton.interactable = false;
            EventManager.SendEvent (new ClanEventData.LeaveClanRequest());
        }

        private void LeaveClanResponse(ClanEventData.LeaveClanResponse response)
        {
            _leaveClanButton.interactable = true;
            
            if (response.Result == EventData.LeaveTeamResponseType.Success)
            {
                //reset and return to main page
                OpenClanWindow();
            }
            
            if (response.Result == EventData.LeaveTeamResponseType.CannotLeaveOwnedTeam)
            {
                //Display Popup [ drop][cancel]
                var dialogData = DialogBoxData.CreateActionBox
                (
                    "You cant leave clan as owner",
                    () =>
                    {
                        EventManager.SendEvent (new ClanEventData.DropClanRequest());
                        _overlayLoading.SetActive(true);                   
                    },
                    () =>
                    {
                       // _playerTeamTab.SetActive (true);
                       // _leavingTab.SetActive (false);
                       // _listTeamTab.SetActive (false);
                    },
                    "Terminate Clan"
                );
                EventManager.SendEvent (new EventData.OnDialogEvent (dialogData));
            }
        }

        private void DropClanResponse(ClanEventData.DropClanResponse response)
        {
            ClearClanDetails();
            OpenClanWindow();
        }

        public void ClearClanDetails()
        {
            _cachedClanDetails = null;
        }

        public void RequestJoinClan()
        {
            _clanDetailsRequestButton.interactable = false;
            EventManager.SendEvent(new ClanEventData.RequestJoinClan(_cachedClanDetails.ClanName));
        }

        void OpenClanDetails(ClanEventData.OpenClanDetail eventData)
        {
            _clanDetailsRequestButton.interactable = true;
            _cachedClanDetails = eventData.ClanData;
            _clanDetailsName.text = eventData.ClanData.ClanName;
            _clanDetailsMembers.text = eventData.ClanData.ClanMembers + " Members";
            _clanDetailsMessage.text = eventData.ClanData.ClanMessage;
            _panelClanDetails.Show();
        }

        private void ResponseJoinClanRequest(ClanEventData.ResponseJoinClan response)
        {
            switch (response.Result)
            {
                case  "success":
                    EventManager.SendEvent(new EventData.NoticeMessage("Your request has been sent", "Clan "+response.ClanId));
                break;
                    
                case  "already_applied":
                    EventManager.SendEvent(new EventData.NoticeMessage("Your already applied to:", "Clan "+response.ClanId));
                break;
            }
        }

        #endregion
    }
}


