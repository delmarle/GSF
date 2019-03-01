using System.Collections;
using Dispatcher;
using UnityEngine;
using UnityEngine.UI;

namespace GSFramework.UI
{
    public class UiRemoteCharacterButton : MonoBehaviour
    {
        [SerializeField] private Text _displayName;
        [SerializeField] private Text _level;
        [SerializeField] private Button _callButon;

        private CharacterEventData.CharacterData _cache;

        public void Setup(CharacterEventData.CharacterData remote)
        {
            _callButon.interactable = true;
            _cache = remote;
            _displayName.text = remote.DisplayName == ""? "Guest": remote.DisplayName;
            _level.text = "Level " + remote.Level;
        }

        /// <summary>
        /// called from editor event
        /// </summary>
        public void CallRemote()
        {
            _callButon.interactable = false;
            EventManager.SendEvent(new CharacterEventData.LoadRemoteCharacter(_cache));
            StartCoroutine(DelayEnable());
        }

        private IEnumerator DelayEnable()
        {
            yield return new WaitForSeconds(2);
            _callButon.interactable = true;
        }
    }
}


