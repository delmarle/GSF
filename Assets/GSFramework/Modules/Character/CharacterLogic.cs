using GSFramework;
using UnityEngine;
using GSFramework.Utils;

public class CharacterLogic : MonoBehaviour 
{
	#region FIELD

	private CharacterEventData.CharacterData _cachedData;
	[SerializeField] private TextMesh _text;
	[SerializeField] private Invokable[] _invokablesForRemote;

	public void SetupRemoteCharacter(CharacterEventData.CharacterData data)
	{
		_cachedData = data;
		_text.text = _cachedData.DisplayName;
		_text.transform.localPosition = (data.Height+0.75f) * Vector3.up;

		InvokeAll();
	}

	private void InvokeAll()
	{
		foreach (var inv in _invokablesForRemote)
		{
			inv.InvokeLogic();
		}
	}

	#endregion

}
