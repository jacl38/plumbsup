using UnityEngine;
using System.Collections;

public class ScaleWayPoints : MonoBehaviour {
	
	private Vector2 objectScale;
	public Vector3[] points;
	private int index = 0;
	private Vector2 Current;
	private Vector2 Begin;
	private float xVel = 0.0F;
	private float yVel = 0.0F;
	private float time;
	private float totalTime;

	void Start () {
		objectScale = gameObject.transform.localScale;
		Current = new Vector2(points[0].x, points[0].y);
		Begin = Current;
		gameObject.transform.localScale = new Vector3(points[0].x, points[0].y, 1);
		time = 0;
		totalTime = 0;
	}
	
	public void Update () {
		time += Time.deltaTime;
		totalTime += Time.deltaTime;

		Current.x = Mathf.SmoothDamp(
			Current.x,
			points[index].x,
			ref xVel,
			0.3F,
			Mathf.Infinity,
			Time.deltaTime / points[index].z);

		Current.y = Mathf.SmoothDamp(
			Current.y,
			points[index].y,
			ref yVel,
			0.3F,
			Mathf.Infinity,
			Time.deltaTime / points[index].z);

		gameObject.transform.localScale = new Vector3(Current.x * objectScale.x, Current.y * objectScale.y, 1);
		if (time >= points[index].z && index < points.Length - 1)
		{
			time = 0;
			index++;
		}
	}

	public void Reset()
	{
		Current = Begin;
		time = 0;
	}

	public bool ended()
	{
		float duration = 0;
		foreach(Vector3 point in points)
		{
			duration += point.z;
		}
		return totalTime > duration;
	}

	void OnEnable()
	{
		Reset();
	}
}
