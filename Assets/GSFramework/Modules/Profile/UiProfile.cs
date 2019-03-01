using UnityEngine;
using UnityEngine.UI;
using Dispatcher;

namespace GSFramework.UI
{
	public class UiProfile : Module 
	{
		#region FIELDS
		[SerializeField] private Text _userNameText;
		[SerializeField] private Text _countryText;
		[SerializeField] private Text _cityText;



		#endregion
		protected override void Register ()
		{
			base.Register ();
			EventManager.Subscribe<EventData.AccountDetailsEvent>(OnUpdateProfile);
			EventManager.Subscribe<EventData.ChangeUserDetailsEvent> (OnChangeUserDetails);
		}

		protected override void UnRegister ()
		{
			base.UnRegister ();
			EventManager.Unsubscribe<EventData.AccountDetailsEvent>(OnUpdateProfile);
			EventManager.Unsubscribe<EventData.ChangeUserDetailsEvent> (OnChangeUserDetails);
		}


		protected virtual void OnChangeUserDetails(EventData.ChangeUserDetailsEvent result)
		{
			_userNameText.text = result.Username;
		}
		protected virtual void OnUpdateProfile(EventData.AccountDetailsEvent result)
		{
			_userNameText.text = DataManager.Instance.Get_string (Keys.UserName);
			_countryText.text = result.City;
			_cityText.text = result.Country;
		}
	}
}


