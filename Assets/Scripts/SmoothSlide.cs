using UnityEngine;
using System.Collections;

public class SmoothSlide : MonoBehaviour {

	public Vector2 Begin;
	public Vector2 End;
	public float seconds;

	private Vector2 Current;
	private float xVel = 0.0F;
	private float yVel = 0.0F;

	void Start () {
		Current = Begin;
	}
	
	void Update () {
		Slide();
	}

	void Slide()
	{
		Current.x = Mathf.SmoothDamp(Current.x, End.x, ref xVel, 0.3F, Mathf.Infinity, Time.deltaTime/seconds);
		Current.y = Mathf.SmoothDamp(Current.y, End.y, ref yVel, 0.3F, Mathf.Infinity, Time.deltaTime/seconds);
		gameObject.GetComponent<RectTransform>().localPosition = Current;
	}
}
