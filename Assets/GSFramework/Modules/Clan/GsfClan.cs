using System.Collections.Generic;
using Dispatcher;
using GameSparks.Api.Requests;
using GameSparks.Core;


namespace GSFramework
{
    public class PublicClanData
    {
        public string ClanId;
        public string ClanName;
        public string ClanMessage;
        public int ClanLevel;
        public int ClanMembers;
    }

    public class ClanData
    {
        public string ClanName;
        
        public string ClanMessage;
        public int ClanLevel;
        public int XpCurrent;
        public int XpMax;
        public int PlayerRank;
        public int PlayerCount;

        public ClanMemberData[] Members;
        public string RequestResult;
        public ClanRequestData[] PlayersRequest;
        public string[] Logs;
        public string CreationDate;
    }

    public class ClanMemberData
    {
        public string PlayerId;
        public string PlayerDisplayName;
        public string ClanId;
        public int Rank;
        public string JoinDate;
    }

    public class ClanRequestData
    {
        public string PlayerId;
        public string PlayerDisplayName;
    }

    public class GsfClan : Module 
    {
        #region FIELDS

        private ClanData PlayerClan 
        {
            get { return _playerClanCache; }
            set
            {
                _playerClanCache = value;
                if (value == null)
                {
                    if(DataManager.Instance.HasKey_Object("clan"))
                    DataManager.Instance.DeleteKey_Object("clan");
                }
                else
                {
                    DataManager.Instance.Add_Object("clan",value);
                }
            }
        }

        private ClanData _playerClanCache;
        
        #endregion
        #region OVERRIDES

        protected override void Register()
        {
            EventManager.Subscribe<ClanEventData.CreateClanRequest> (RequestCreateClan);
            EventManager.Subscribe<ClanEventData.GetClanRequest> (RequestGetOwnClan);
            EventManager.Subscribe<ClanEventData.CheckClanRequest> (RequestCheckClanExist);
            EventManager.Subscribe<ClanEventData.SearchClansRequest> (SearchRandomClans);
            EventManager.Subscribe<ClanEventData.RequestJoinClan>(RequestJoinClan);
            EventManager.Subscribe<ClanEventData.LeaveClanRequest>(RequestLeaveClan);
            EventManager.Subscribe<ClanEventData.DropClanRequest>(RequestDropClan);
            EventManager.Subscribe<ClanEventData.RequestUpdateClanMessage>(RequestClanMessageUpdate);
            EventManager.Subscribe<ClanEventData.ClanDecisionRequest>(RequestDecisionRequest);
        }

        protected override void UnRegister()
        {
            EventManager.Unsubscribe<ClanEventData.CreateClanRequest> (RequestCreateClan);
            EventManager.Unsubscribe<ClanEventData.GetClanRequest> (RequestGetOwnClan);
            EventManager.Unsubscribe<ClanEventData.CheckClanRequest> (RequestCheckClanExist);
            EventManager.Unsubscribe<ClanEventData.SearchClansRequest> (SearchRandomClans);
            EventManager.Unsubscribe<ClanEventData.RequestJoinClan>(RequestJoinClan);
            EventManager.Unsubscribe<ClanEventData.LeaveClanRequest>(RequestLeaveClan);
            EventManager.Unsubscribe<ClanEventData.DropClanRequest>(RequestDropClan);
            EventManager.Unsubscribe<ClanEventData.RequestUpdateClanMessage>(RequestClanMessageUpdate);
            EventManager.Unsubscribe<ClanEventData.ClanDecisionRequest>(RequestDecisionRequest);
        }

        #endregion
	    #region REQUESTS

        #region Create
        private void RequestCreateClan(ClanEventData.CreateClanRequest request)
        {
            new LogEventRequest_gsf_clan_create()
                .Set_clan_name(request.ClanName)
                .Set_clan_message(request.ClanMessage)
                .Send((response) =>
                {
                    print(response.JSONString);
                    if (response.HasErrors)
                    {
                        if(response.Errors.ContainsKey("teamType"))
                        {
                            if(response.Errors.GetString("teamType") == "MAX_TEAMS_REACHED")
                                EventManager.SendEvent(new EventData.ErrorCantCreateTeam("MAX_TEAMS_REACHED"));

                            if(response.Errors.GetString("teamType") == "MAX_OWNED_REACHED")
                                EventManager.SendEvent(new EventData.ErrorCantCreateTeam("MAX_OWNED_REACHED"));

                        }

                        if(response.Errors.ContainsKey("teamId"))
                        {
                            if(response.Errors.GetString("teamId") == "NOT_UNIQUE")
                                EventManager.SendEvent(new EventData.ErrorCantCreateTeam("NOT_UNIQUE"));
                        }
                        
                        if(response.Errors.ContainsKey("error"))
                        {
                            if(response.Errors.GetString("error") == "not_enough_currency")
                                EventManager.SendEvent(new EventData.ErrorCantCreateTeam("not enough credits"));
                        }
                        

                    }
                    else
                    {
                        var cd = response.ScriptData.GetGSData("clan_data");
                        var members = response.ScriptData.GetGSDataList("members");
                       
                        
                        var memberList = new List<ClanMemberData>();
                            
                        foreach (var member in members)
                        {
                            var entry = new ClanMemberData();
                            entry.PlayerId = member.GetString("_player_id");
                            entry.PlayerDisplayName = member.GetString("_player_name");
                            entry.ClanId = member.GetString("_clan_id");
                            entry.Rank = member.GetInt("_rank").Value;
                            entry.JoinDate = member.GetString("_date");
                            memberList.Add(entry);
                        }
                        ClanData clanData = new ClanData();
                        clanData.Members = memberList.ToArray(); 
                        clanData.ClanName = cd.GetString("_clan_id");
                        clanData.PlayerCount = cd.GetInt("_player_count").Value;
                        clanData.ClanLevel = cd.GetInt("_level").Value;
                        clanData.XpCurrent = cd.GetInt("_xp_current").Value;
                        clanData.XpMax = cd.GetInt("_xp_max").Value;
                        clanData.ClanMessage = cd.GetString("_clan_message");
                        clanData.Logs = cd.GetStringList("_logs").ToArray();
                        clanData.CreationDate = cd.GetString("_creation_date");
                        PlayerClan = clanData;
                        RequestClanLogs();
                        EventManager.SendEvent(new ClanEventData.CreateClanResponse(clanData));
                    }
                }  );
        }

        private void RequestCheckClanExist(ClanEventData.CheckClanRequest request)
        {
            new LogEventRequest_gsf_clan_check()
                .Set_clan_name(request.ClanName)
                .Send((response) =>
                {
                    if (response.HasErrors)
                    {
                       
                    }
                    else
                    {
                        var result = response.ScriptData.GetBoolean("available").Value;
                        EventManager.SendEvent(new ClanEventData.CheckClanResponse(result));
                    }
                }  );
        }

        #endregion
        #region Main

        private void RequestClanMessageUpdate(ClanEventData.RequestUpdateClanMessage request)
        {
            new LogEventRequest_gsf_clan_update_message()
                .Set_clan_id(PlayerClan.ClanName)
                .Set_message(request.Message)
                .Send((response) =>
                {
                    print(response.JSONString);
                    if (response.HasErrors)
                    {
                        
                    }
                    else
                    {
                       bool success =  response.ScriptData.GetBoolean("result").Value;
                        if (success)
                        {
                            EventManager.SendEvent(new ClanEventData.UpdateClanMessageResponse(request.Message));
                            RequestClanLogs();
                        }
                        else
                        {
                            EventManager.SendEvent(new ClanEventData.UpdateClanMessageResponse("Failed to update message."));
                        }
                    }
                }  );
        }

        private void RequestGetOwnClan(ClanEventData.GetClanRequest request)
        {
            new LogEventRequest_gsf_clan_get()
                .Send((response) =>
                {
                    print(response.JSONString);
                    if (response.HasErrors)
                    {
                        
                    }
                    else
                    {
                        
                        if (response.ScriptData.ContainsKey("clan_data"))
                        {
                            var cd = response.ScriptData.GetGSData("clan_data");
                            var members = response.ScriptData.GetGSDataList("members");
                            var memberList = new List<ClanMemberData>();
                            
                            foreach (var member in members)
                            {
                                var entry = new ClanMemberData();
                                entry.PlayerId = member.GetString("_player_id");
                                entry.PlayerDisplayName = member.GetString("_player_name");
                                entry.ClanId = member.GetString("_clan_id");
                                entry.Rank = member.GetInt("_rank").Value;
                                entry.JoinDate = member.GetString("_date");
                                memberList.Add(entry);
                            }
                            ClanData clanData = new ClanData();
                            clanData.Members = memberList.ToArray(); 
                            
                            clanData.ClanName = cd.GetString("_clan_id");
                            clanData.PlayerCount = cd.GetInt("_player_count").Value;
                            clanData.ClanLevel = cd.GetInt("_level").Value;
                            clanData.XpCurrent = cd.GetInt("_xp_current").Value;
                            clanData.XpMax = cd.GetInt("_xp_max").Value;
                            clanData.ClanMessage = cd.GetString("_clan_message");
                            clanData.Logs = cd.GetStringList("_logs").ToArray();
                            clanData.CreationDate = cd.GetString("_creation_date");
                            //find requests
                            clanData.RequestResult = cd.GetString("request_result");
                            string requestResult = response.ScriptData.GetString("request_result");
                            if (requestResult == "success")
                            {
                                var requestList = new List<ClanRequestData>();
                                clanData.RequestResult = requestResult;
                                var requestData = response.ScriptData.GetGSDataList("request_entries");
                                foreach (var entry in requestData)
                                {
                                    ClanRequestData req = new ClanRequestData();
                                    req.PlayerId = entry.GetString("_player_id");
                                    req.PlayerDisplayName = entry.GetString("player_display_name");
                                    requestList.Add(req);
                                    
                                }
                                clanData.PlayersRequest = requestList.ToArray();
                            }
                            else
                            {
                                //rank to low or no requests
                            }
                         
                            PlayerClan = clanData;
                            RequestClanLogs();
                            EventManager.SendEvent(new ClanEventData.GetClanResponse(true,clanData));
                        }
                        else
                        {
                            var foundClanData = response.ScriptData;
                            SendFoundClan(foundClanData);
                        }
                       
                    }
                }  );
        }

        private void SearchRandomClans(ClanEventData.SearchClansRequest request)
        {
            if (request.SearchedField == "")
            {
                new LogEventRequest_gsf_clan_search_random()
                    .Send((response) =>
                    {
                        if (response.HasErrors)
                        {
                        
                        }
                        else
                        {
                            SendFoundClan(response.ScriptData);
                        }
                    }  );
            }
            else
            {
                new LogEventRequest_gsf_clan_search()
                    .Set_search_field(request.SearchedField)
                    .Send((response) =>
                    {
                        if (response.HasErrors)
                        {
                        
                        }
                        else
                        {
                            SendFoundClan(response.ScriptData);
                        }
                    }  );
            }  
        }

        private void SendFoundClan(GSData data)
        {
            List<PublicClanData> clanList = new List<PublicClanData>();   
            if (data.ContainsKey("clans"))
            {
                var clanData = data.GetGSDataList("clans");
          
                for (var i = 0; i < clanData.Count; i++)
                {
                    var cacheClan = new PublicClanData
                    {
                        ClanId =  clanData[i].GetString("_clan_id"),
                        ClanName = clanData[i].GetString("_clan_id"),
                        ClanLevel = clanData[i].GetInt("_level").Value,
                        ClanMembers = clanData[i].GetInt("_player_count").Value,
                        ClanMessage = clanData[i].GetString("_clan_id")
                    };
                    clanList.Add(cacheClan);
                }
                EventManager.SendEvent(new ClanEventData.SearchClansResponse(clanList.ToArray()));
            }   
        }

        private void RequestJoinClan(ClanEventData.RequestJoinClan request)
        {
            new LogEventRequest_gsf_clan_request_join()
                .Set_clan_id(request.ClanId)
                .Send((response) =>
                {
                    print(response.JSONString);
                    if (response.HasErrors)
                    {
                       
                    }
                    else
                    {
                        var result = response.ScriptData.GetString("result");
                        EventManager.SendEvent(new ClanEventData.ResponseJoinClan(request.ClanId,result));
                    }
                }  );
        }
        #endregion
        #region Logs

        private void RequestClanLogs()
        {
            new LogEventRequest_gsf_clan_logs_get()
                .Send((response) => {
                    if (response.HasErrors) 
                    {
                        if(response.Errors.ContainsKey("no_clan"))
                        {
                          
                        }
                    } 
                    else
                    {  
                        var logsList = response.ScriptData.GetGSDataList("logs");
                        List<string> logs = new List<string>();
                        foreach (var logEntry in logsList)
                        {
                            logs.Add(logEntry.GetString("_log"));
                        }
                        
                        EventManager.SendEvent(new ClanEventData.GetClanLogsResponse(logs.ToArray()));    
                    }
                });
        }

        #endregion
        #region Leave Clan

        private void RequestDropClan(ClanEventData.DropClanRequest request)
        {
            print("drrrroop "+PlayerClan.ClanName);
            new LogEventRequest_gsf_clan_drop()
                .Set_clan_id(PlayerClan.ClanName)
                .Send ((response) => 
                {
                    if (response.HasErrors) 
                    {
                        print(response.Errors.JSON);

                    } else 
                    {
                        PlayerClan = null;
                        EventManager.SendEvent (new ClanEventData.DropClanResponse());
                    }
                });
        }

        private void RequestLeaveClan(ClanEventData.LeaveClanRequest request)
        {
            print(PlayerClan.ClanName);
            new LogEventRequest_gsf_clan_leave()
                .Set_clan_name(PlayerClan.ClanName)
                .Send ((response) => {print(response.JSONString);
                    if (response.HasErrors) 
                    {
                        if(response.Errors.ContainsKey("team"))
                        {
                            if(response.Errors.GetString("team") == "CANNOT_LEAVE_OWNED_TEAM")
                            {
                                EventManager.SendEvent (new ClanEventData.LeaveClanResponse (EventData.LeaveTeamResponseType.CannotLeaveOwnedTeam));
                            }
                        }
                    } 
                    else 	
                    {
                        EventManager.SendEvent (new ClanEventData.LeaveClanResponse (EventData.LeaveTeamResponseType.Success));
                    }
                });
        }

        #endregion

        private void RequestDecisionRequest(ClanEventData.ClanDecisionRequest request)
        {
            print("request: "+request.Decision+"  > "+request.PlayerId);
            new LogEventRequest_gsf_clan_request_reply()
                .Set_decision(request.Decision? "true":"false")
                .Set_player_id(request.PlayerId)
                .Send ((response) => {print(response.JSONString);
                    if (response.HasErrors) 
                    {
                        if(response.Errors.ContainsKey("team"))
                        {
                          
                        }
                    } 
                    else
                    {
             
                        
                        var requestList = new List<ClanRequestData>();
                        
                        var requestData = response.ScriptData.GetGSDataList("entries");
                        foreach (var entry in requestData)
                        {
                            ClanRequestData req = new ClanRequestData();
                            req.PlayerId = entry.GetString("_player_id");
                            req.PlayerDisplayName = entry.GetString("player_display_name");
                            requestList.Add(req);
                                    
                        }

                        
                        EventManager.SendEvent(new ClanEventData.ReceiveClanRequests(requestList.ToArray()));
                        RequestGetOwnClan(new ClanEventData.GetClanRequest());
                        
                    }
                });
        }

        #endregion

    }
}


