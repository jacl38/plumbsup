using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class DelayedSound : MonoBehaviour {
	
	public float Delay;

	void Start () {
		if(!SoundsController.soundMuted)
		gameObject.GetComponent<AudioSource>().PlayDelayed(Delay);
	}
	
	void Update () {
	}
}
