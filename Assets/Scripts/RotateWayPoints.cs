using UnityEngine;
using System.Collections;

public class RotateWayPoints : MonoBehaviour {

	public Vector2[] points;
	private int index = 0;
	private float Current;
	private float vel = 0.0F;
	private float time;
	private float totalTime;

	void Start () {
		Current = points[0].x;
		gameObject.transform.eulerAngles = new Vector3(0, 0, points[0].x);
		time = 0;
		totalTime = 0;
	}
	
	void Update () {
		time += Time.deltaTime;
		totalTime += Time.deltaTime;
		Current = Mathf.SmoothDamp(Current, points[index].x, ref vel, 0.3F, Mathf.Infinity, Time.deltaTime / points[index].y);
		gameObject.transform.eulerAngles = new Vector3(0, 0, Current);
		if (time >= points[index].y && index < points.Length - 1)
		{
			time = 0;
			index++;
		}
	}

	public bool ended()
	{
		float duration = 0;
		foreach(Vector2 point in points)
		{
			duration += point.y;
		}
		return totalTime > duration;
	}
}
