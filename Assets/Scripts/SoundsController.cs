using UnityEngine;
using System.Collections;

public class SoundsController : MonoBehaviour {

	public AudioSource[] sources;

	void Start () {
		
	}
	
	void Update () {
		
	}

	public void ToggleSource(int sourceIndex)
	{
		sources[sourceIndex].mute = !sources[sourceIndex].mute;
	}
}
