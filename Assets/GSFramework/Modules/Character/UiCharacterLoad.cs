using UnityEngine;
using UnityEngine.UI;
using Dispatcher;

namespace GSFramework.UI
{
	public class UiCharacterLoad :  Module
	{
		#region FIELDS

		[SerializeField]
		private CharacterCache _cache;
		[SerializeField] private UIPanel _characterSelection, _characterCreation;
		[SerializeField] private Slider _heightSlider;
		[SerializeField] private Text _materialName;
		[SerializeField] private Button _buttonCreate;
		[SerializeField] private Button _buttonDelete;
		#endregion
		#region OVERRIDES
		protected override void Register ()
		{
			base.Register ();
			EventManager.Subscribe<CharacterEventData.GetCharacterResponse> (OnReceiveCharacterResponse);
			EventManager.Subscribe<CharacterEventData.CharacterActionResponse> (OnReceiveCharacterActionResponse);
			EventManager.Subscribe<CharacterEventData.CharacterNotCreated> (OnCharacterNotCreated);
		}

		protected override void UnRegister ()
		{
			base.UnRegister ();
			EventManager.Unsubscribe<CharacterEventData.GetCharacterResponse> (OnReceiveCharacterResponse);
			EventManager.Unsubscribe<CharacterEventData.CharacterActionResponse> (OnReceiveCharacterActionResponse);
			EventManager.Unsubscribe<CharacterEventData.CharacterNotCreated> (OnCharacterNotCreated);
		}
		#endregion

		public void TryGetCharacter()
		{
			EventManager.SendEvent (new CharacterEventData.GetCharacterRequest());
		}
		#region GS responses
		public void OnReceiveCharacterResponse(CharacterEventData.GetCharacterResponse response)
		{
			
			if (response.HasCharacter) 
			{
				_characterCreation.Hide ();
				_characterSelection.Show ();
				_buttonDelete.interactable = true;
				_buttonCreate.interactable = false;
			} 
			else 
			{
				_characterSelection.Hide ();
				_characterCreation.Show ();
				_buttonCreate.interactable = true;
				_buttonDelete.interactable = false;
			}
			EventManager.SendEvent(new EventData.ShowHud(false));
			ReDrawUi ();
		}

		public void OnReceiveCharacterActionResponse(CharacterEventData.CharacterActionResponse response)
		{
			
		}

		public void OnCharacterNotCreated(CharacterEventData.CharacterNotCreated error)
		{
			var dialogData = DialogBoxData.CreateActionBox
				(
					"You must create a character first",
					() =>
					{
						//Create Character
						TryGetCharacter();

					},
					() =>
					{
						//Cancel
					},
					"Create Character"
				);
			EventManager.SendEvent (new EventData.OnDialogEvent (dialogData));
		}
		#endregion
		#region UI public calls
		public void ChangeCharacterMaterialLeft()
		{
			GsfCharacterCreation.Instance.ChangeCharacterType (true);
			ReDrawUi ();
		}

		public void ChangeCharacterMaterialRight()
		{
			GsfCharacterCreation.Instance.ChangeCharacterType (false);
			ReDrawUi ();
		}

		public void UpdateSliderHeight()
		{
			GsfCharacterCreation.Instance.ChangeCharacterHeight (_heightSlider.value);
		}

		public void SendCharacterCreationRequest()
		{
			_buttonCreate.interactable = false;
			EventManager.SendEvent (new CharacterEventData.CharacterCreationRequest (_cache.CurrentCharacterHeight,_cache.CurrentCharacterType
			));
		}

		public void TryDeleteCharacter()
		{
			_buttonDelete.interactable = false;
			_buttonCreate.interactable = false;
			EventManager.SendEvent (new CharacterEventData.CharacterDeleteRequest ());
		}

		public void ReDrawUi()
		{
			_materialName.text = _cache.CurrentCharacterType;
		}

		public void GoBack()
		{
			GsfCharacterCreation.Instance.DestroyPlayerObject ();
			_characterSelection.Hide ();
			_characterCreation.Hide ();
			EventManager.SendEvent(new EventData.ShowHud(true));
		}
		#endregion

	}
}

