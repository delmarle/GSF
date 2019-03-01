using Dispatcher;
using GSFramework;
using UnityEngine;

public class UiOpenRemoteCharacterPanel : MonoBehaviour 
{
	public void OnMouseDown()
	{
		EventManager.SendEvent(new CharacterEventData.OpenRemotePanel());
	}

}
