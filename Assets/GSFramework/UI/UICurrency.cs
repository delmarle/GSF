using UnityEngine;
using UnityEngine.UI;
using Dispatcher;

namespace GSFramework.UI
{
    public class UiCurrency : MonoBehaviour 
    {
        [SerializeField,Range(1,3)]private int _currencyId;
        [SerializeField] private Text _currencyValueText;

        protected void OnEnable()
        {
            EventManager.Subscribe<EventData.CurrencyUpdateEvent>(OnUpdateCurrencies);
        }

        protected void OnDisable()
        {
            EventManager.Unsubscribe<EventData.CurrencyUpdateEvent>(OnUpdateCurrencies);
        }


        protected virtual void OnUpdateCurrencies(EventData.CurrencyUpdateEvent result)
        {
            var newCurrencyValue = 0;
            switch (_currencyId)
            {
                case 1:
                    newCurrencyValue = result.Currency1;
                    break;
                case 2:
                    newCurrencyValue = result.Currency2;
                    break;
                case 3:
                    newCurrencyValue = result.Currency3;
                    break;
            }
            _currencyValueText.text = newCurrencyValue.ToString ();
        }

    }
}
