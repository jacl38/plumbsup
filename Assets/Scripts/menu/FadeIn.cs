using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour {

	public Color startColor;
	public float startPercent;
	public float endPercent;
	public float timeScale = 1;
	private float time;

	void Start () {
		startColor = gameObject.GetComponent<Image>().color;
		gameObject.GetComponent<Image>().color = new Color(startColor.r, startColor.g, startColor.b, startPercent);
		time = 0F;
	}
	
	void Update ()
	{
		gameObject.GetComponent<Image>().color = new Color(startColor.r, startColor.g, startColor.b, Mathf.SmoothStep(startPercent, endPercent, timeScale * time));
		time += Time.deltaTime;
	}

	public void Reset()
	{
		gameObject.GetComponent<Image>().color = new Color(startColor.r, startColor.g, startColor.b, startPercent);
		time = 0F;
	}

	public bool ended()
	{
		return time >= timeScale;
	}
}
