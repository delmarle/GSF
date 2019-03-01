using UnityEngine;
using UnityEngine.UI;
using Dispatcher;

namespace GSFramework.UI
{
    public class UiUpdateUserDetails : MonoBehaviour 
    {
        [SerializeField] private InputField _displayNameField;
        [SerializeField] private InputField _languageField;
        [SerializeField] private InputField _passwordField;
        [SerializeField] private InputField _oldPasswordField;
        [SerializeField] private GameObject _oldPasswordtext;
        [SerializeField] private InputField _usernameField;


        private void OnEnable()
        {
            if (GsfMaster.Instance == null || GsfMaster.Instance.IsAuthenticated == false)
                return;

            _displayNameField.text = DataManager.Instance.Get_string (Keys.DisplayName);
            _languageField.text = DataManager.Instance.Get_string (Keys.Language);
            string pass = DataManager.Instance.Get_string (Keys.Password);

            _oldPasswordField.gameObject.SetActive (pass != "");
            _oldPasswordtext.gameObject.SetActive (pass !=  "");
            _oldPasswordField.text = pass;
            _passwordField.text = pass;
            _usernameField.text = DataManager.Instance.Get_string (Keys.UserName);


        }
		

        public void SendUpdateEvent()
        {
            EventManager.SendEvent (new EventData.ChangeUserDetailsEvent(
                _displayNameField.text,
                _languageField.text,
                _passwordField.text,
                _oldPasswordField.text,
                _usernameField.text
            ));
        }
    }

}

