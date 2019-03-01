using System.Collections.Generic;
using UnityEngine;
using Dispatcher;
using GameSparks.Api.Requests;


namespace GSFramework
{
	public class GsfCharacterLoader : Module
	{
		#region FIELD
		[SerializeField] private CharacterCache _cache;
		private GameObject _localPlayer;
		private GameObject _remotePlayer;
		#endregion
		#region Overrides
		protected override void Register ()
		{
			base.Register ();
			EventManager.Subscribe<CharacterEventData.LoadLocalCharacter> (LoadLocalCharacter);
			EventManager.Subscribe<CharacterEventData.LoadRemoteCharacter> (LoadRemoteCharacter);
			EventManager.Subscribe<CharacterEventData.CharacterFindRemoteRequest> (RequestRemotePlayersList);
		}

		protected override void UnRegister ()
		{
			base.UnRegister ();
			EventManager.Unsubscribe<CharacterEventData.LoadLocalCharacter> (LoadLocalCharacter);
			EventManager.Unsubscribe<CharacterEventData.LoadRemoteCharacter> (LoadRemoteCharacter);
			EventManager.Unsubscribe<CharacterEventData.CharacterFindRemoteRequest> (RequestRemotePlayersList);
		}
			
		#endregion


		private void LoadLocalCharacter(CharacterEventData.LoadLocalCharacter data)
		{
			if(_localPlayer != null)
				Destroy(_localPlayer);

			GameObject go;
			_cache.CharacterDict.TryGetValue(data.Body, out go);
			
			_localPlayer = Instantiate (go);
			_localPlayer.transform.position = data.Position;
			
			_localPlayer.transform.localScale = Vector3.one*_cache.CurrentCharacterHeight;

			_localPlayer.AddComponent<Demo.LocalPlayerInput>();

			EventManager.SendEvent(new CharacterEventData.OnLocalCharacterInstantiated(_localPlayer.transform));
		}

		private void LoadRemoteCharacter(CharacterEventData.LoadRemoteCharacter response)
		{
			if(_remotePlayer != null)
				Destroy(_remotePlayer);

			GameObject go;
			_cache.CharacterDict.TryGetValue(response.Data.BodyType, out go);
			
			_remotePlayer = Instantiate (go);
			if(_localPlayer)
				_remotePlayer.transform.position = _localPlayer.transform.position+Vector3.left;
			_remotePlayer.name = response.Data.DisplayName+" (remote)";
			_remotePlayer.transform.localScale = Vector3.one*response.Data.Height;


			TrySetupCharacter(_remotePlayer, response.Data);
			
			EventManager.SendEvent(new EventData.NoticeMessage(
				"[ "+response.Data.DisplayName+" Joined ]",
				"Remote player"
			));
		}

		void TrySetupCharacter(GameObject remotePlayer, CharacterEventData.CharacterData data)
		{
			var component = remotePlayer.GetComponent<CharacterLogic>();
			if (component == null) return;
			
			component.SetupRemoteCharacter(data);
		}

		#region REQUEST

		private void RequestRemotePlayersList(CharacterEventData.CharacterFindRemoteRequest request)
		{
			new LogEventRequest_gsf_character_get_random()
				.Send(response =>
				{
					if (response.HasErrors)
					{
						
					}
					else
					{

						var listPeople = response.ScriptData.GetGSDataList("result");

						List<CharacterEventData.CharacterData> rebuiltList = new List<CharacterEventData.CharacterData>();
						foreach (var pple in listPeople)
						{
							rebuiltList.Add
							(
								new CharacterEventData.CharacterData()
								{
									Id = pple.GetString("_display_name"),
									DisplayName = pple.GetString("_display_name"),
									BodyType = pple.GetString("_body"),
									Height = float.Parse(pple.GetString("_height")),
									Level = pple.GetInt("_level").Value,
									XpCurrent = pple.GetInt("_xp_current").Value,
									XpMax = pple.GetInt("_xp_max").Value
								}
							);
							
						}
							
						EventManager.SendEvent(new CharacterEventData.CharacterFindRemoteResponse(rebuiltList.ToArray()));
					}
				}  );
		}

		#endregion

	}
}
