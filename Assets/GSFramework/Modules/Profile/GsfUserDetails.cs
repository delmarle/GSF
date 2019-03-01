using GameSparks.Api.Requests;
using Dispatcher;
using GSFramework;

/// <summary>
/// Contain Core code related to the Profile ( the player, not the character ) & function to upgrade and update account
/// </summary>
public class GsfUserDetails : Module 
{
	#region OVERRIDE
	protected override void Register ()
	{
		base.Register ();
		EventManager.Subscribe<EventData.ChangeUserDetailsEvent>(RequestChangeUserDetails);

	}

	protected override void UnRegister ()
	{
		base.UnRegister ();
		EventManager.Unsubscribe<EventData.ChangeUserDetailsEvent>(RequestChangeUserDetails);
	}

	protected override void OnAuthenticated (EventData.AuthenticatedEvent result)
	{
		base.OnAuthenticated (result);

		if (result.Response.HasErrors == false) 
		{
			RequestAccountDetail ();
		}
	}

	#endregion


	public void RequestAccountDetail()
	{
		new AccountDetailsRequest().Send((accountResponse) => {
			if (accountResponse.HasErrors)
			{
				//It didn't worked
			}
			else
			{
				DataManager.Instance.Add_STRING(Keys.DisplayName, accountResponse.DisplayName);
				DataManager.Instance.Add_STRING(Keys.Country, accountResponse.Location.Country);
		

				DataManager.Instance.Add_INT(Keys.Currency1, (int)accountResponse.Currency1);
				DataManager.Instance.Add_INT(Keys.Currency2, (int)accountResponse.Currency2);
				DataManager.Instance.Add_INT(Keys.Currency3, (int)accountResponse.Currency3);


				EventManager.SendEvent(new EventData.AccountDetailsEvent(
					accountResponse.Location.Country,
					accountResponse.Location.City,
					accountResponse.UserId
				));
					
				EventManager.SendEvent(new EventData.CurrencyUpdateEvent(
					(int)accountResponse.Currency1,
					(int)accountResponse.Currency2,
					(int)accountResponse.Currency3
				));
					
				//string userId = accountResponse.UserId;
				//GSData virtualGoods = accountResponse.VirtualGoods;
				
			}
		}  );
	}

	private void RequestChangeUserDetails(EventData.ChangeUserDetailsEvent request)
	{

		//string currentAuthMethod = DataManager.Instance.Get_string (GSFKeys.authType);
		string currentPass = DataManager.Instance.Get_string (Keys.Password);
		/*
		GSRequestData additionalData = new GSRequestData();
		additionalData.Add (GSFKeys.age, 16);
		*/
		if (currentPass != "") 
		{
			//we previously didnt had password
			
			var req = new ChangeUserDetailsRequest ();

			if (request.DisplayName != "")
				req.SetDisplayName(request.DisplayName);
			if(request.Username != "")
				req.SetUserName (request.Username)
				.SetLanguage (request.Language);
			if (request.Password != "")
			{
				req.SetOldPassword(request.OldPassword);
				req.SetNewPassword(request.Password);
			}
			//.SetScriptData(additionalData)
			req.Send(response => 
					{
						if (response.HasErrors)
						{
							print(response.Errors.JSON);
							//It didn't work
							if(response.Errors.ContainsKey("USERNAME"))
							{
								if(response.Errors.GetString("USERNAME") == "TAKEN")
									EventManager.SendEvent(new EventData.ErrorChangeDetailsTaken());
							}
							if(response.Errors.ContainsKey("DETAILS"))
							{
								if(response.Errors.GetString("DETAILS") == "UNRECOGNISED")
									EventManager.SendEvent(new EventData.ErrorChangeDetailsUnrecognised());
							}
						}
						else
						{	
							LocalCache.SaveValue(Keys.UserName,request.Username);
							LocalCache.SaveValue(Keys.Password,request.Password);
							
							DataManager.Instance.Add_STRING(Keys.DisplayName,request.DisplayName);
							DataManager.Instance.Add_STRING(Keys.Language,request.Language);
							DataManager.Instance.Add_STRING(Keys.UserName,request.Username);
							DataManager.Instance.Add_STRING(Keys.Password,request.Password);
							EventManager.SendEvent(new EventData.SuccessChangeUserDetails());
						}
					}
				);

		} 
		else 
		{
			//we previously Had a password

			var req = new ChangeUserDetailsRequest();
			if (request.DisplayName != "")
				req.SetDisplayName(request.DisplayName);
			if (request.Username != "")
				req.SetUserName(request.Username);
			
			req.SetLanguage(request.Language);
					
			if (request.Password != "")
			{
				req.SetNewPassword(request.Password);
			}
				//.SetScriptData(additionalData)
			req.Send(response => 
					{
						if (response.HasErrors)
						{
							print(response.Errors.JSON);

							if(response.Errors.ContainsKey("DETAILS"))
							{
								if(response.Errors.GetString("DETAILS") == "UNRECOGNISED")
									EventManager.SendEvent(new EventData.ErrorChangeDetailsUnrecognised());
							}

						}
						else
						{
							LocalCache.SaveValue(Keys.UserName,request.Username);
							LocalCache.SaveValue(Keys.Password,request.Password);
							DataManager.Instance.Add_STRING(Keys.DisplayName,request.DisplayName);
							DataManager.Instance.Add_STRING(Keys.Language,request.Language);
							DataManager.Instance.Add_STRING(Keys.UserName,request.Username);
							DataManager.Instance.Add_STRING(Keys.Password,request.Password);
							EventManager.SendEvent(new EventData.SuccessChangeUserDetails());
						}
					}
				);
		}

	}
}
