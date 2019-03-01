using System.Collections.Generic;
using GameSparks.Api.Requests;
using GameSparks.Api.Responses;
using Dispatcher;

namespace GSFramework
{
	public class GsfTeam : Module 
	{
		#region OVERRIDES
		protected override void Register ()
		{
			base.Register ();
			EventManager.Subscribe<EventData.CurrentTeamRequest> (GetCurrentTeamRequest);
			EventManager.Subscribe<EventData.RequestJoinTeam> (JoinTeamRequest);
			EventManager.Subscribe<EventData.LeaveTeamRequest> (LeaveCurrentTeamRequest);
			EventManager.Subscribe<EventData.GetTeamListRequest> (GetTeamListRequest);
			EventManager.Subscribe<EventData.CreateTeamRequest> (CreateTeamRequest);
			EventManager.Subscribe<EventData.DropTeamRequest> (DropTeamRequest);
		}

		protected override void UnRegister ()
		{
			base.UnRegister ();
			EventManager.Unsubscribe<EventData.CurrentTeamRequest> (GetCurrentTeamRequest);
			EventManager.Unsubscribe<EventData.RequestJoinTeam> (JoinTeamRequest);
			EventManager.Unsubscribe<EventData.LeaveTeamRequest> (LeaveCurrentTeamRequest);
			EventManager.Unsubscribe<EventData.GetTeamListRequest> (GetTeamListRequest);
			EventManager.Unsubscribe<EventData.CreateTeamRequest> (CreateTeamRequest);
			EventManager.Unsubscribe<EventData.DropTeamRequest> (DropTeamRequest);
		}
		#endregion
		#region REQUESTS
		public void JoinTeamRequest(EventData.RequestJoinTeam request)
		{
			new LogEventRequest_gsf_team_join()
				.Set_team_id(request.TeamId)
				.Send((response) =>
					{
						if (response.HasErrors)
						{
							// MAX_TEAMS_REACHED
							//NOT_UNIQUE
							print(response.Errors.JSON);
						}
						else
						{
							var teamData = new JoinTeamResponse(response.ScriptData.GetGSData("team_data"));
							EventManager.SendEvent(new EventData.JoinTeamResponse(
								teamData
							));
						}
					}  );
		}

		public void CreateTeamRequest(EventData.CreateTeamRequest request)
		{
			new LogEventRequest_gsf_team_create()
				.Set_team_id(request.TeamName)
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

						}
						else
						{
							var teamData = new CreateTeamResponse(response.ScriptData.GetGSData("team_data"));
							EventManager.SendEvent(new EventData.CreateTeamResponse(
											teamData.Members,
											teamData.Owner,
											null,
											teamData.TeamName));
						}
					}  );

			/* //old request
			new CreateTeamRequest()
				.SetTeamType(GSFKeys.clanTeamType)
				.SetTeamId(request.clanName)
				.SetTeamName(request.clanName)
				.Send((response) =>
					{
						if (response.HasErrors)
						{
							// MAX_TEAMS_REACHED
							if(response.Errors.ContainsKey("teamType"))
							{
								// MAX_TEAMS_REACHED
								if(response.Errors.GetString("teamType") == "MAX_TEAMS_REACHED")
									EventManager.SendEvent(new GSF_Error.UserAlreadyUsed());

								if(response.Errors.GetString("teamType") == "CANNOT_LEAVE_OR_JOIN_OWNED_MANDATORY_TEAM")
									print("cannot leave as leader");

							}

							if(response.Errors.ContainsKey("teamId"))
							{
								if(response.Errors.GetString("teamId") == "NOT_UNIQUE")
									print("//NOT_UNIQUE");
							}

						}
						else
						{
							EventManager.SendEvent<GSF_Event.CreateClanResponse>(new GSF_Event.CreateClanResponse(
								response.Members,
								response.Owner,
								response.ScriptData.JSON,
								response.TeamName));
						}
					}  );
			*/
		}

		public void GetCurrentTeamRequest(EventData.CurrentTeamRequest empty)
		{
			var typesTeams = new List<string> ();
			typesTeams.Add (Keys.BasicTeamType);

			new GetMyTeamsRequest ()
				.SetTeamTypes (typesTeams)
				.SetOwnedOnly (false)
				.Send((response) =>
					{
						if (response.HasErrors)
						{
							// MAX_TEAMS_REACHED
							//NOT_UNIQUE
						}
						else
						{
							EventManager.SendEvent(new EventData.GetCurrentTeamResponse(
								response.Teams
							));
						}
					}  );
		}

		public void LeaveCurrentTeamRequest(EventData.LeaveTeamRequest request)
		{
			new LogEventRequest_gsf_team_leave()
				.Set_team_type (Keys.BasicTeamType)
				.Set_team_id(request.TeamId)
				.Send ((response) => {print(response.JSONString);
					if (response.HasErrors) 
					{
						// MAX_TEAMS_REACHED
						//NOT_UNIQUE
						if(response.Errors.ContainsKey("team"))
						{
							if(response.Errors.GetString("team") == "CANNOT_LEAVE_OWNED_TEAM")
							{
								//TODO popup need to drop request team
								EventManager.SendEvent (new EventData.LeaveTeamResponse (EventData.LeaveTeamResponseType.CannotLeaveOwnedTeam));
							}
						}

					} 
					else 	
					{
						EventManager.SendEvent(new EventData.GetTeamListRequest());

						EventManager.SendEvent (new EventData.LeaveTeamResponse (EventData.LeaveTeamResponseType.Success));

					}
				});
		}

		public void GetTeamListRequest(EventData.GetTeamListRequest empty)
		{
			new ListTeamsRequest()
				.SetTeamTypeFilter(Keys.BasicTeamType)
				.Send ((response) => {
					if (response.HasErrors) 
					{
						// MAX_TEAMS_REACHED
						//NOT_UNIQUE
						print(response.Errors.JSON);

					} else 
					{
						EventManager.SendEvent (new EventData.GetTeamListResponse (response.Teams));
					}
				});
		}

		protected void DropTeamRequest(EventData.DropTeamRequest request)
		{
			new LogEventRequest_gsf_team_drop()
				.Set_team_id(request.TeamId)
				.Send ((response) => {
					if (response.HasErrors) 
					{
						print(response.Errors.JSON);

					} else 
					{

						EventManager.SendEvent (new EventData.GetTeamListRequest ());
					}
				});
		}
		#endregion
	}
}
