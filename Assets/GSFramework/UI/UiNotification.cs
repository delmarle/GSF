using UnityEngine;
using UnityEngine.UI;
using Dispatcher;

namespace GSFramework.UI
{
	public class UiNotification : Module
	{
		#region FIELDS
		[SerializeField] private Text _status;
		[SerializeField] private Text _result;

		[SerializeField] private UIPanel _panel;
		#endregion

		public void OnReceiveTouch()
		{
			if (_result.text == "...")
				return;

			_panel.Hide ();
		}

		protected override void Register ()
		{
			base.Register ();
			EventManager.Subscribe<EventData.ChangeUserDetailsEvent>(OnRequestChangeUserDetails);
			EventManager.Subscribe<EventData.ErrorChangeDetailsUnrecognised>(OnChangeDetailsError_Unrecognized);
			EventManager.Subscribe<EventData.ErrorChangeDetailsTaken>(OnChangeDetailsError_Taken);
			EventManager.Subscribe <EventData.SuccessChangeUserDetails> (OnSuccessUpdateDetails);
			EventManager.Subscribe <EventData.ErrorRegisterTaken> (OnRegisterError_Taken);
		}

		protected override void UnRegister ()
		{
			base.UnRegister ();
			EventManager.Unsubscribe<EventData.ChangeUserDetailsEvent>(OnRequestChangeUserDetails);
			EventManager.Unsubscribe<EventData.ErrorChangeDetailsUnrecognised>(OnChangeDetailsError_Unrecognized);
			EventManager.Unsubscribe<EventData.ErrorChangeDetailsTaken>(OnChangeDetailsError_Taken);
			EventManager.Unsubscribe < EventData.SuccessChangeUserDetails> (OnSuccessUpdateDetails);
			EventManager.Subscribe <EventData.ErrorRegisterTaken> (OnRegisterError_Taken);
		}

		private void OnRegisterError_Taken(EventData.ErrorRegisterTaken error)
		{
			if (_panel.IsVisible == false) 
			{
				_panel.Show ();
			}
			_result.color = Color.red;
			_result.text = "Username "+error.UserName+" is already taken !";
		}

		private void OnRequestChangeUserDetails(EventData.ChangeUserDetailsEvent error)
		{
			_status.text = "Changing details";
			_result.color = Color.white;
			_result.text = "...";
			_panel.Show ();
		}

		private void OnChangeDetailsError_Unrecognized(EventData.ErrorChangeDetailsUnrecognised error)
		{
			if (_panel.IsVisible == false) 
			{
				_panel.Show ();
			}
			_result.color = Color.red;
			_result.text = "Wrong Login or Password";
		}

		private void OnChangeDetailsError_Taken(EventData.ErrorChangeDetailsTaken empty)
		{
			_result.color = Color.red;
			_result.text = "UserName already taken";
		}

		private void OnSuccessUpdateDetails(EventData.SuccessChangeUserDetails empty)
		{
			_result.color = Color.green;
			_result.text = "Details Updated";
		}


	}
}

