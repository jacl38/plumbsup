using UnityEngine;
using System.Collections;

public class SoundsController : MonoBehaviour {

	public static bool soundMuted = false;
	public static bool musicMuted = false;

	public AudioSource[] soundSources;
	public AudioSource musicSource;

	void Start () {
		
	}
	
	void Update () {
		
	}

	public void toggleSounds()
	{
		soundMuted = !soundMuted;
		foreach(AudioSource source in soundSources)
		{
			source.mute = soundMuted;
		}
	}

	public void toggleMusic()
	{
		musicMuted = !musicMuted;
		musicSource.mute = musicMuted;
	}
}
