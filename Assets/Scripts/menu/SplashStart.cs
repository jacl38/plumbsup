using UnityEngine;
using System.Collections;

public class SplashStart : MonoBehaviour {

	public float seconds;
	private float start;

	void Start () {
		start = Time.time;
	}
	
	void Update () {
		if(Time.time - start >= seconds)
		{
			gameObject.GetComponent<LoadScene>().Load("MenuScreen");
		}
	}
}
