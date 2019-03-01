using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dispatcher;
using GSFramework;

/// <summary>
/// simplified class to handle our sounds
/// </summary>
public class SoundManager : MonoBehaviour 
{
	#region FIELDS
	public static SoundManager Instance;
	public List<AudioClip> soundList = new List<AudioClip>();

	private Dictionary<AudioClip, AudioSource> cachedList = new Dictionary<AudioClip, AudioSource> ();
	#endregion

	void Awake()
	{
		Initialize ();
		EventManager.Subscribe<EventData.PlaySoundEvent> (PlaySound);
	}

	void Initialize()
	{
		Instance = this;
		
		foreach (AudioClip entry in soundList) 
		{
			AudioSource source = this.gameObject.AddComponent<AudioSource> ();
			source.clip = entry;
			source.playOnAwake = false;
			cachedList.Add (entry, source);
		}
	}

	public void PlaySound(EventData.PlaySoundEvent eventData)
	{
		if (eventData.SoundName == null)
			return;
		AudioSource source = null;
		cachedList.TryGetValue (eventData.SoundName, out source);

		if (source && source.isPlaying == false)
			source.Play ();
	}
}
