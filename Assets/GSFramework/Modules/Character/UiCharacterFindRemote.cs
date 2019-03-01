using UnityEngine;
using UnityEngine.UI;
using Dispatcher;
using System.Collections.Generic;

namespace GSFramework.UI
{
    public class UiCharacterFindRemote : Module
    {
        #region FIELDS

        [SerializeField] private UIPanel _panel;
        [SerializeField] private Button _findButton;
        [SerializeField] private LayoutGroup _layout;
        [SerializeField] private UiRemoteCharacterButton _remotePrefab;
        private GenericUIList<CharacterEventData.CharacterData> _remotes;
        #endregion

        #region OVERRIDES

        protected override void Register()
        {
            base.Register();
            EventManager.Subscribe<CharacterEventData.CharacterFindRemoteResponse>(ReponseFindRemoteCharacters);
            EventManager.Subscribe<CharacterEventData.OpenRemotePanel>(OpenPanel);
           
        }

        protected override void UnRegister()
        {
            base.UnRegister();
            EventManager.Unsubscribe<CharacterEventData.CharacterFindRemoteResponse>(ReponseFindRemoteCharacters);
        }

        #endregion

        private void OpenPanel(CharacterEventData.OpenRemotePanel ev)
        {
            _panel.Show();
        }

        private void SetRemoteList(IEnumerable<CharacterEventData.CharacterData> data)
        {
            _remotes.Generate<UiRemoteCharacterButton>(data, (entry, item) => { item.Setup(entry); });

        }
        
        public void Awake()
        {
            _remotes = new GenericUIList<CharacterEventData.CharacterData>(_remotePrefab.gameObject, _layout);
        }

        public void PushFindButton()
        {
            _findButton.interactable = false;
            EventManager.SendEvent(new CharacterEventData.CharacterFindRemoteRequest());
        }


        private void ReponseFindRemoteCharacters(CharacterEventData.CharacterFindRemoteResponse response)
        {
            _findButton.interactable = true;
            SetRemoteList(response.Characters);
        }
    }
}


