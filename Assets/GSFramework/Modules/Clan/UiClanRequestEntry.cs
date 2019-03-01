using Dispatcher;
using UnityEngine;
using UnityEngine.UI;

namespace GSFramework.UI
{
    public class UiClanRequestEntry : MonoBehaviour 
    {
        #region FIELDS
        [SerializeField] private Text _playerNameText;
        [SerializeField] private Text _playerLevelText;
        [SerializeField] private Button _buttonAccept;
        [SerializeField] private Button _buttonRefuse;
        

        private ClanRequestData _cachedData = new ClanRequestData();
        #endregion

        public void Setup(ClanRequestData data)
        {
            _cachedData = data;
            _playerNameText.text = data.PlayerDisplayName;
            _playerLevelText.text = "1";
            _buttonAccept.interactable = true;
            _buttonRefuse.interactable = true;
        }

        public void Accept()
        {
            _buttonAccept.interactable = false;
            _buttonRefuse.interactable = false;
            EventManager.SendEvent(new ClanEventData.ClanDecisionRequest(true, _cachedData.PlayerId));
        }

        public void Decline()
        {
            _buttonAccept.interactable = false;
            _buttonRefuse.interactable = false;
            EventManager.SendEvent(new ClanEventData.ClanDecisionRequest(false, _cachedData.PlayerId));
        }

    }
}
