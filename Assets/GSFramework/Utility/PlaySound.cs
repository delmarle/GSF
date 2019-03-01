using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dispatcher;
using GSFramework;

public class PlaySound : MonoBehaviour 
{
	[SerializeField] private AudioClip Clip;


	public void PlayAudioClip()
	{
		EventManager.SendEvent<EventData.PlaySoundEvent> (new EventData.PlaySoundEvent (Clip));
	}
}
