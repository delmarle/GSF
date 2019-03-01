using UnityEngine;
using UnityEngine.UI;
using Dispatcher;


namespace GSFramework.UI
{
	public class UiAuthentication : Module 
	{
		#region FIELDS
		
		[Header("Wechat")] 
		[SerializeField] private bool _displayWechatLogin;
		[SerializeField] private Button _buttonWechatLogin;
		[SerializeField] private UIPanel _wechatLoginPanel;
		
		[Header("Facebook")]
		[SerializeField] private bool _displayFacebookLogin;
		[SerializeField] private Button _buttonFacebookLogin;
		
		[Header("Amazon")]
		[SerializeField] private bool _displayAmazonLogin;
		[SerializeField] private Button _buttonAmazonLogin;
		
		[Header("Gamecenter")]
		[SerializeField] private bool _displayGamecenterLogin;
		[SerializeField] private Button _buttonGamecenterLogin;
		
		[Header(" other Panels")]
		[SerializeField] private UIPanel _statusPanel;
		[SerializeField] private UIPanel _buttonGroupPanel;
		[SerializeField] private UIPanel _loginSelectionPanel;
		[SerializeField] private Text _statusField;

		[Header("Manual Device Login")] 
		[SerializeField] private UIPanel _manualDeviceLoginPanel;
		[SerializeField] private Button _buttonManualDeviceLogin;
		[SerializeField] private InputField _displayName;


		[Header("Register New Account")]
		[SerializeField] private InputField _displayName2;
		[SerializeField] private InputField _userName2;
		[SerializeField] private InputField _password2;
		[SerializeField] private UIPanel _registerAccountPanel;
		[SerializeField] private Button _registerButton2;
		#endregion
		#region UI calls

		public void OpenWechatLoginPanel()
		{
			_wechatLoginPanel.Show();
		}
		
		public void OpenFacebookLoginPanel()
		{
			EventManager.SendEvent(new EventData.UpdateAuthStatus("Facebook - login"));
			EventManager.SendEvent(new EventData.TryConnectFacebook());
		}
		
		public void OpenAmazonLoginPanel()
		{
			EventManager.SendEvent(new EventData.UpdateAuthStatus("Amazon - Not implemented yet"));
		}
		
		public void OpenGamecenterLoginPanel()
		{
			EventManager.SendEvent(new EventData.UpdateAuthStatus("Gamecenter - login"));
			EventManager.SendEvent(new EventData.RequestGamecenterConnect());
		}

		public void OpenLoginSelection()
		{
			OpenLoginSelection(new EventData.OnLoginSelection());
		}

		private void OnUpdateStatus(EventData.UpdateAuthStatus message)
		{
			_statusPanel.Show();
			_statusField.text = message.StatusName;
		}

		public void OpenLoginSelection(EventData.OnLoginSelection empty)
		{
			_loginSelectionPanel.Show ();
			_buttonWechatLogin.gameObject.SetActive(_displayWechatLogin);
			_buttonFacebookLogin.gameObject.SetActive(_displayFacebookLogin);
			_buttonAmazonLogin.gameObject.SetActive(_displayAmazonLogin);
			_buttonGamecenterLogin.gameObject.SetActive(_displayGamecenterLogin);
		}

		public void OpenDeviceLogin()
		{
			_buttonManualDeviceLogin.interactable = true;
		}

		public void RequestDeviceLogin()
		{
			if (_displayName.text == "")
				return;

			_buttonManualDeviceLogin.interactable = false;
			EventManager.SendEvent (new EventData.RequestDeviceLogin (_displayName.text));
		}

		public void RequestRegisterNewAccount()
		{
			if (_displayName2.text == "" || _userName2.text == "" || _password2.text == "")
				return;

			_registerButton2.interactable = false;
			EventManager.SendEvent (new EventData.RequestRegisterNewAccount (_displayName2.text,_userName2.text,_password2.text));
		}
		#endregion

		#region OVERRIDES
		protected override void Register ()
		{
			base.Register ();
			EventManager.Subscribe<EventData.OnLoginSelection> (OpenLoginSelection);
			EventManager.Subscribe<EventData.ErrorRegisterTaken> (OnRegisterError_Taken);
			EventManager.Subscribe<EventData.UpdateAuthStatus>(OnUpdateStatus);
		}

		protected override void UnRegister ()
		{
			base.UnRegister ();
			EventManager.Unsubscribe<EventData.OnLoginSelection> (OpenLoginSelection);
			EventManager.Unsubscribe<EventData.ErrorRegisterTaken> (OnRegisterError_Taken);
			EventManager.Unsubscribe<EventData.UpdateAuthStatus>(OnUpdateStatus);
		}
		#endregion
		#region CALLBACKS
		protected override void OnAvaillable (EventData.AvaillableEvent result)
		{
			base.OnAvaillable (result);
			if (result.AvaillableKey && result.LoggedInKey == false) 
			{
				_statusField.text = "Connected";


			}
		}

		protected override void OnAuthenticated (EventData.AuthenticatedEvent result)
		{
			base.OnAuthenticated (result);
			_statusField.text = "Logged in: " + result.UsedAuthentication;

			var isDeviceLogin = result.UsedAuthentication == EventData.AuthenticatedEvent.AuthType.DeviceLogin;
			if (isDeviceLogin) 
			{
				_loginSelectionPanel.Hide ();
				_statusField.text = "Logged in using Device ID";
			}
			if (result.UsedAuthentication == EventData.AuthenticatedEvent.AuthType.Login) 
			{
				_registerAccountPanel.Hide ();
				_statusField.text = "Logged in as: " + DataManager.Instance.Get_string (Keys.UserName);
			}
			if (result.UsedAuthentication == EventData.AuthenticatedEvent.AuthType.Facebook) 
			{
				EventManager.SendEvent(new EventData.UpdateAuthStatus("Gamespark using Facebook"));
			}
			if (result.UsedAuthentication == EventData.AuthenticatedEvent.AuthType.Wechat) 
			{
				EventManager.SendEvent(new EventData.UpdateAuthStatus("Gamespark using Wechat"));
			}
			if (result.UsedAuthentication == EventData.AuthenticatedEvent.AuthType.Amazon) 
			{
				EventManager.SendEvent(new EventData.UpdateAuthStatus("Gamespark using Amazon"));
			}
			if (result.UsedAuthentication == EventData.AuthenticatedEvent.AuthType.GameCenter) 
			{
				EventManager.SendEvent(new EventData.UpdateAuthStatus("Gamespark using GameCenter"));
			}
			_loginSelectionPanel.Hide();
			_buttonGroupPanel.Show ();
		}
		#endregion
		#region Errors Responses
		public void OnRegisterError_Taken(EventData.ErrorRegisterTaken error)
		{
			//display notice
			//reset UI
			_registerButton2.interactable = true;
		}
		#endregion

	}

}
