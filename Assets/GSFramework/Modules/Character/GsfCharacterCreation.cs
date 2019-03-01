
using UnityEngine;
using GameSparks.Api.Requests;
using Dispatcher;

namespace GSFramework
{
	public class GsfCharacterCreation : Module 
	{
		#region FIELDS
		
		
		[SerializeField] private Transform _charPos;
		[SerializeField] private CharacterCache _cache;
		private GameObject _currentPlayer;
		private int _currentSelected = 0;
		
		

		public static GsfCharacterCreation Instance;

	
		#endregion
		#region Overrides
		protected override void Register ()
		{
			base.Register ();
			EventManager.Subscribe<CharacterEventData.GetCharacterRequest> (GetCharacter);
			EventManager.Subscribe<CharacterEventData.CharacterCreationRequest> (CharacterCreateRequest);
			EventManager.Subscribe<CharacterEventData.CharacterDeleteRequest> (CharacterDeleteRequest);
			EventManager.Subscribe<CharacterEventData.CharacterActionRequest> (CharacterActionRequest);
		}

		protected override void UnRegister ()
		{
			base.UnRegister ();
			EventManager.Unsubscribe<CharacterEventData.GetCharacterRequest> (GetCharacter);
			EventManager.Unsubscribe<CharacterEventData.CharacterCreationRequest> (CharacterCreateRequest);
			EventManager.Unsubscribe<CharacterEventData.CharacterDeleteRequest> (CharacterDeleteRequest);
			EventManager.Unsubscribe<CharacterEventData.CharacterActionRequest> (CharacterActionRequest);
		}
			
		#endregion
		#region Monobehaviour
		void Awake()
		{
			Instance = this;
		}

		#endregion
		#region Character object
		public void LoadCharacter()
		{
			DestroyPlayerObject ();
			
			if (_cache.CurrentCharacterType == "")
			{
				_cache.CurrentCharacterType = _cache.Characters[0].CharacterName;
			}
			GameObject go = null;
			_cache.CharacterDict.TryGetValue(_cache.CurrentCharacterType, out go);
			
			
			_currentPlayer = Instantiate (go);
			_currentPlayer.transform.localScale = Vector3.one*_cache.CurrentCharacterHeight;
			
			_currentPlayer.transform.position = _charPos.position;
			_currentPlayer.transform.rotation = _charPos.rotation;
		}
		
		

		public void DestroyPlayerObject()
		{
			if (_currentPlayer)
				Destroy (_currentPlayer);
		}
		
		#endregion
		#region update character object
		public void ChangeCharacterType(bool forward)
		{
			if (forward) 
			{
				_currentSelected++;
				if (_currentSelected >= _cache.Characters.Length)
					_currentSelected = 0;

			} else 
			{
				_currentSelected--;
				if (_currentSelected < 0)
					_currentSelected = _cache.Characters.Length-1;

			}
			_cache.CurrentCharacterType = _cache.Characters[_currentSelected].CharacterName;
			LoadCharacter();
		}
		public void ChangeCharacterHeight(float factor)
		{
			_cache.CurrentCharacterHeight = factor;
			LoadCharacter ();
		}

		
		#endregion
		#region Request
		/// <summary>
		/// Called when we want to learn xp
		/// response contain if level up and new xp values, we are using null to initialize the UI
		/// </summary>
		void CharacterActionRequest(CharacterEventData.CharacterActionRequest request)
		{
			new LogEventRequest_gsf_character_action()
				.Set_action_name(request.ActionName)
				.Send(response =>
					{
						if (response.HasErrors)
						{
							if(request.ActionName != "null")
								EventManager.SendEvent(new CharacterEventData.CharacterNotCreated());
						}
						else
						{
					
							var currentXp = response.ScriptData.GetInt("current_xp").Value;
							var xpMax = response.ScriptData.GetInt("xp_max").Value;
							var level = response.ScriptData.GetInt("level").Value;
							var earned = response.ScriptData.GetInt("earned_xp").Value;
							if(request.ActionName != "null")
								EventManager.SendEvent(new EventData.NoticeMessage("[ "+request.ActionName+" ]","+ "+earned+" xp"));
							
							EventManager.SendEvent(new CharacterEventData.CharacterActionResponse(level,currentXp,xpMax));
						}
					}  );
		}

		void GetCharacter(CharacterEventData.GetCharacterRequest request)
		{
			new LogEventRequest_gsf_character_get()
				.Send(response =>
					{
						if (response.HasErrors)
						{

						}
						else
						{
							var character = response.ScriptData.ContainsKey("character") ? response.ScriptData.GetGSData("character") : null;
							
							if(character != null)
							{
								print(character.JSON);
								if(character.ContainsKey("_height"))
								_cache.CurrentCharacterHeight = float.Parse(character.GetString("_height"));

								if (character.ContainsKey("_body"))
									_cache.CurrentCharacterType = character.GetString("_body");
								EventManager.SendEvent(new CharacterEventData.GetCharacterResponse(true,_cache.CurrentCharacterType,_cache.CurrentCharacterHeight));
								
								LoadCharacter();
							}
							else
							{
								//we dont have character, lets create one
								ChangeCharacterType(true);
								EventManager.SendEvent(new CharacterEventData.GetCharacterResponse(false,"",1));
								
							}
							
						}
					}  );
		}

		void CharacterCreateRequest(CharacterEventData.CharacterCreationRequest request)
		{
			
			new LogEventRequest_gsf_character_create ()
				.Set_body(request.Body)
				.Set_height(request.Height.ToString())
				.Send((authResponse) =>
					{
						if (authResponse.HasErrors)
						{
							print(authResponse.Errors.JSON);
						}
						else
						{
							GetCharacter(new CharacterEventData.GetCharacterRequest());
						}
					}  );
		}

		protected void CharacterDeleteRequest(CharacterEventData.CharacterDeleteRequest request)
		{
			DestroyPlayerObject ();
			new LogEventRequest_gsf_character_delete()
				.Send(response =>
					{
						if (response.HasErrors)
						{

						}
						else
						{
							EventManager.SendEvent(new CharacterEventData.GetCharacterRequest());
						}
					}  );
		}


		#endregion
	}
}

