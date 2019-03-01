using UnityEngine;
using GameSparks.Core;
using GameSparks.Api.Requests;
using Dispatcher;
using System.Collections.Generic;
#if UNITY_IOS
using UnityEngine.SocialPlatforms.GameCenter;
#endif
#if FACEBOOK
using Facebook.Unity;
#endif

namespace GSFramework
{
	public class GsfAuthentication : Module
	{
		#region FIELDS
		[Header("Configuration")]
		[SerializeField] private bool _loginIfSavedLocally = true;
		[SerializeField] private bool _forceDeviceLogin = true;

		private bool _isLoggedIn;
		#endregion
		#region OVERRIDES
		protected override void Register ()
		{
			base.Register ();
			GS.GameSparksAvailable += OnGameSparksAvailable;
			EventManager.Subscribe<EventData.RequestDeviceLogin> (DeviceLogin);
			EventManager.Subscribe < EventData.RequestRegisterNewAccount> (RegisterAccount);
			EventManager.Subscribe<EventData.TryConnectFacebook>(CheckConnectedFacebook);
			#if UNITY_IOS
			EventManager.Subscribe<EventData.RequestGamecenterConnect>(CheckGameCenter);
			#endif
		}

		protected override void UnRegister ()
		{
			base.UnRegister ();
			GS.GameSparksAvailable -= OnGameSparksAvailable;
			EventManager.Unsubscribe<EventData.RequestDeviceLogin> (DeviceLogin);
			EventManager.Unsubscribe < EventData.RequestRegisterNewAccount> (RegisterAccount);
			EventManager.Unsubscribe<EventData.TryConnectFacebook>(CheckConnectedFacebook);
			#if UNITY_IOS
			EventManager.Unsubscribe<EventData.RequestGamecenterConnect>(CheckGameCenter);
			#endif
		}
		#endregion
		protected virtual void OnGameSparksAvailable(bool available)
		{
			EventManager.SendEvent(new EventData.AvaillableEvent(available,_isLoggedIn));

			if (available && _isLoggedIn == false )
			{
				if (_loginIfSavedLocally && 
					LocalCache.GetValue(Keys.UserName) != "" &&
					LocalCache.GetValue(Keys.Password) != ""
				) 
				{
					Authenticate (LocalCache.GetValue (Keys.UserName), LocalCache.GetValue (Keys.Password));
					return;
				}
				if (_forceDeviceLogin && LocalCache.GetValue(Keys.DisplayName) != "") 
				{
					//we already had registered a displayname before
					DeviceLogin (new EventData.RequestDeviceLogin(LocalCache.GetValue(Keys.DisplayName)));
					return;
				}
				//open Login Selection
				EventManager.SendEvent(new EventData.OnLoginSelection());
			}

			if (available == false && _isLoggedIn)
			{
				//We are connected or the connection has dropped
				// "Reconnecting..."
				EventManager.SendEvent(new EventData.ConnectionDropped());
			}

		}
		#region AUTHENTICATION
		public void DeviceLogin(EventData.RequestDeviceLogin request)
		{
			new DeviceAuthenticationRequest()
				.SetDisplayName(request.DisplayName)
				.Send((authResponse) =>
					{
						if (authResponse.HasErrors)
						{
							//ERROR
							if(authResponse.Errors.ContainsKey("deviceOS"))
							{
								//The supplied deviceOS was not in the accepted range
							}
						}
						else
						{
							_isLoggedIn = true;
							DataManager.Instance.Add_STRING(Keys.DisplayName,request.DisplayName);
							LocalCache.SaveValue(Keys.DisplayName,request.DisplayName);
							DataManager.Instance.Add_STRING(Keys.AuthType,EventData.AuthenticatedEvent.AuthType.DeviceLogin.ToString());
							EventManager.SendEvent(new EventData.AuthenticatedEvent(authResponse,EventData.AuthenticatedEvent.AuthType.DeviceLogin));
						}
					}  );
		}

		public void Authenticate(string userName, string password)
		{
			new AuthenticationRequest()
				.SetUserName(userName)
				.SetPassword(password)
				.Send((authResponse) =>
					{
						if (authResponse.HasErrors)
						{
							//ERROR
							if(authResponse.Errors.ContainsKey("DETAILS"))
							{
								if(authResponse.Errors.GetString("DETAILS") == "UNRECOGNISED")
									EventManager.SendEvent(new EventData.ErrorChangeDetailsUnrecognised());
							}

						}
						else
						{
							//SUCCESS
							_isLoggedIn = true;
							DataManager.Instance.Add_STRING(Keys.AuthType,EventData.AuthenticatedEvent.AuthType.Login.ToString());
							DataManager.Instance.Add_STRING(Keys.UserName,userName);
							DataManager.Instance.Add_STRING(Keys.Password,password);
							EventManager.SendEvent(new EventData.AuthenticatedEvent(authResponse,EventData.AuthenticatedEvent.AuthType.Login));
						}
					}  );
		}


		public void WechatConnect(EventData.RequestWechatConnect request)
		{
			new WeChatConnectRequest() .SetAccessToken("need an access token")
				.SetDoNotLinkToCurrentPlayer(true)
				.SetErrorOnSwitch(true)
				.SetOpenId(request.WechatId)
				.SetSwitchIfPossible(false)
				.SetSyncDisplayName(true)
				.Send((response) => 
					{
						if (response.HasErrors)
						{
							
						}
						else
						{
							_isLoggedIn = true;
							var authToken = response.AuthToken; 
							var displayName = response.DisplayName; 
							var newPlayer = response.NewPlayer; 
							var scriptData = response.ScriptData; 
							var switchSummary = response.SwitchSummary; 
							var userId = response.UserId;
							print(authToken+displayName+newPlayer+scriptData+switchSummary+userId);
							EventManager.SendEvent(new EventData.AuthenticatedEvent(response,EventData.AuthenticatedEvent.AuthType.Wechat));
						}
					});
		}

		public void AmazonConnect(EventData.RequestAmazonConnect request)
		{
			new AmazonConnectRequest() .SetAccessToken("need an access token")
				.SetDoNotLinkToCurrentPlayer(true)
				.SetErrorOnSwitch(true)
				.SetSwitchIfPossible(false)
				.SetSyncDisplayName(true)
				.SetAccessToken(request.AccessToken)
				.Send((response) => 
				{
					if (response.HasErrors)
					{
							
					}
					else
					{
						_isLoggedIn = true;
						var authToken = response.AuthToken; 
						var displayName = response.DisplayName; 
						var newPlayer = response.NewPlayer; 
						var scriptData = response.ScriptData; 
						var switchSummary = response.SwitchSummary; 
						var userId = response.UserId;
						print(authToken+displayName+newPlayer+scriptData+switchSummary+userId);
						EventManager.SendEvent(new EventData.AuthenticatedEvent(response,EventData.AuthenticatedEvent.AuthType.Amazon));
					}
				});
		}
		
		#if UNITY_IOS
		public void CheckGameCenter(EventData.RequestGamecenterConnect request)
		{
			Social.localUser.Authenticate(success => {
				if (success)
				{
					Debug.Log("Authentication successful");
					GameCenterDeviceRequest();
				}
				else
				{
					Debug.Log("Authentication failed");
				}
			});
		}
			
		private void GameCenterDeviceRequest()
		{
			
			new GameCenterConnectRequest()
				.SetExternalPlayerId(Social.localUser.id)
				.Send((response) => 
					{
						if (response.HasErrors)
					{
							
					}
					else
					{
						_isLoggedIn = true;
						var authToken = response.AuthToken; 
						var displayName = response.DisplayName; 
						var newPlayer = response.NewPlayer; 
						var scriptData = response.ScriptData; 
						var switchSummary = response.SwitchSummary; 
						var userId = response.UserId;
						print(authToken+displayName+newPlayer+scriptData+switchSummary+userId);
						EventManager.SendEvent(new EventData.AuthenticatedEvent(response,EventData.AuthenticatedEvent.AuthType.GameCenter));
					}
					});
		}

		#endif
		#region FaceBook

		void CheckConnectedFacebook(EventData.TryConnectFacebook request)
		{
			bool canUseFb = false;
			#if FACEBOOK
			canUseFb = true;
			if(FB.IsInitialized)
			{
				FB.ActivateApp();
				var perms = new List<string>(){"public_profile", "email", "user_friends"};
				FB.LogInWithReadPermissions(perms, (result) =>
					{
						if (FB.IsLoggedIn)
						{
							
							new FacebookConnectRequest()
								.SetSyncDisplayName(true)
								.SetAccessToken(AccessToken.CurrentAccessToken.TokenString)
								.SetDoNotLinkToCurrentPlayer(false)// we don't want to create a new account so link to the player that is currently logged in
								.SetSwitchIfPossible(true)//this will switch to the player with this FB account id they already have an account from a separate login
								.Send((response) => 
								{
									if (response.HasErrors)
									{
										EventManager.SendEvent(new EventData.OnLoginSelection());
									}
									else
									{
										_isLoggedIn = true;
										var authToken = response.AuthToken; 
										var displayName = response.DisplayName; 
										var newPlayer = response.NewPlayer; 
										var scriptData = response.ScriptData; 
										var switchSummary = response.SwitchSummary; 
										var userId = response.UserId;
										print(authToken+displayName+newPlayer+scriptData+switchSummary+userId);
										EventManager.SendEvent(new EventData.AuthenticatedEvent(response,EventData.AuthenticatedEvent.AuthType.Facebook));
									}
									
								});	
							
						}
						else
						{
						}
					}
				);

			}
			else
			{
				
				FB.Init(TryInitFacebook);
			}

			#endif
			if (canUseFb == false)
			{
				EventManager.SendEvent(new EventData.OnLoginSelection());
				EventManager.SendEvent(new EventData.NoticeMessage("Missing SDK:","Facebook"));
			}
			
		}

		void TryInitFacebook()
		{
			EventManager.SendEvent(new EventData.UpdateAuthStatus("Facebook: initialized"));
			EventManager.SendEvent(new EventData.TryConnectFacebook());
		}

		#endregion
		#endregion
		#region REGISTRATION
		public void QuickRegister(string username)
		{
			int rand = Random.Range (0, 999999);
			string user = username + rand;

			new RegistrationRequest ()
				.SetUserName (user)
				.SetPassword (user)
				.SetDisplayName (username)
				.Send (response =>
					{
						if (response.HasErrors)
						{
							//ERROR
							print(response.Errors.JSON);
							if(response.Errors.ContainsKey("USERNAME"))
							{
								if(response.Errors.GetString("USERNAME") == "TAKEN")
									EventManager.SendEvent(new EventData.ErrorRegisterTaken(user));
							}
						}
						else
						{
							LocalCache.SaveValue(Keys.UserName,user);
							LocalCache.SaveValue(Keys.Password,user);
							DataManager.Instance.Add_STRING(Keys.UserName,user);
							DataManager.Instance.Add_STRING(Keys.Password,user);
							DataManager.Instance.Add_STRING(Keys.DisplayName,username);

							Authenticate(user,user);
						}

					});
		}

		public void RegisterAccount(EventData.RequestRegisterNewAccount request)
		{
			new RegistrationRequest ()
				.SetUserName (request.UserName)
				.SetPassword (request.Password)
				.SetDisplayName (request.DisplayName)
				.Send (response =>
					{
						
						if (response.HasErrors)
						{
							//ERROR
							print(response.Errors.JSON);
							if(response.Errors.ContainsKey("USERNAME"))
							{
								if(response.Errors.GetString("USERNAME") == "TAKEN")
									EventManager.SendEvent(new EventData.ErrorRegisterTaken(request.UserName));
							}
						}
						else
						{
							
							LocalCache.SaveValue(Keys.UserName,request.UserName);
							LocalCache.SaveValue(Keys.Password,request.Password);
							DataManager.Instance.Add_STRING(Keys.UserName,request.UserName);
							DataManager.Instance.Add_STRING(Keys.Password,request.Password);
							DataManager.Instance.Add_STRING(Keys.DisplayName,request.DisplayName);

							Authenticate(request.UserName,request.Password);
						}

					});

		}


		#endregion
	}

}