using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour {
	
	public float startPercent;
	public float endPercent;
	public float timeScale = 1;
	private float time;

	void Start () {
		gameObject.GetComponent<Image>().color = new Color(1.0F, 1.0F, 1.0F, startPercent);
		time = 0F;
	}
	
	void Update ()
	{
		gameObject.GetComponent<Image>().color = new Color(1.0F, 1.0F, 1.0F, Mathf.SmoothStep(startPercent, endPercent, timeScale * time));
		time += Time.deltaTime;
	}
}
