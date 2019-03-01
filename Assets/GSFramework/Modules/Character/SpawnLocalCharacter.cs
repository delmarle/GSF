using Dispatcher;
using UnityEngine;

namespace GSFramework
{
	public class SpawnLocalCharacter : MonoBehaviour
	{
		[SerializeField] private CharacterCache _cache;
		
		void Start()
		{
			if (HasCharacter())
			{
				EventManager.SendEvent(new CharacterEventData.LoadLocalCharacter
				(
					_cache.CurrentCharacterHeight,
					_cache.CurrentCharacterType,
					transform.position
				));
			}
			else
			{
				Debug.LogError("No character found");
			}

		}


		bool HasCharacter()
		{
			return _cache.CurrentCharacterType != "";
		}
	}
}
