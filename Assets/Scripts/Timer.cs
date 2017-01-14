using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

	public int seconds;
	float s;

	void Start () {
		s = seconds;
	}
	
	void Update () {
		if(s >= 0)
		s -= Time.deltaTime;
		seconds = (int) s;
		if (seconds % 60 < 10) gameObject.GetComponent<Text>().text = (seconds / 60).ToString() + ":0" + (seconds % 60).ToString();
		else gameObject.GetComponent<Text>().text = (seconds / 60).ToString() + ":" + (seconds % 60).ToString();
	}
}
