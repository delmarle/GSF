using UnityEngine;
using UnityEngine.UI;
using Dispatcher;

namespace GSFramework.UI
{
    public class UiCharacterHud : Module 
    {
        #region fields
        [SerializeField] private Slider _xpSlider;
        [SerializeField] private Text _xpValue;
        [SerializeField] private Text _levelValue;
        [SerializeField] private UIPanel _panel;
        #endregion

        #region overrides
        protected override void Register ()
        {
            base.Register ();
            EventManager.Subscribe<CharacterEventData.CharacterActionResponse> (OnUpdateXp);
        }

        protected override void UnRegister ()
        {
            base.UnRegister ();
            EventManager.Unsubscribe<CharacterEventData.CharacterActionResponse> (OnUpdateXp);
        }

        protected override void OnAuthenticated (EventData.AuthenticatedEvent result)
        {
            EventManager.SendEvent(new CharacterEventData.CharacterActionRequest("null"));
        }

        public void SendActionRequest(string actionName)
        {
            EventManager.SendEvent(new CharacterEventData.CharacterActionRequest(actionName));
        }
        #endregion

        protected void OnUpdateXp(CharacterEventData.CharacterActionResponse response)
        {
            _xpSlider.maxValue = response.MaxXp;
            _xpSlider.value = response.CurrentXp;
            _xpValue.text = response.CurrentXp+"/"+response.MaxXp;
            _levelValue.text = "Level: "+response.Level;
            _panel.Show ();
        }
    }
}
