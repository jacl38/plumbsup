using UnityEngine;
using System.Collections;

public class PositionWayPoints : MonoBehaviour {
	
	public Vector3[] points;
	private int index = 0;
	private Vector2 Current;
	private Vector2 Begin;
	private float xVel = 0.0F;
	private float yVel = 0.0F;
	private float time;
	private float totalTime;
	public bool resetOnEnable = true;

	void Start () {
		gameObject.GetComponent<RectTransform>().localPosition = new Vector2(points[0].x, points[0].y);
		Current = new Vector2(points[0].x, points[0].y);
		Begin = Current;
		time = 0;
		totalTime = 0;
	}

	void Update()
	{
		time += Time.deltaTime;
		totalTime += Time.deltaTime;
		Current.x = Mathf.SmoothDamp(Current.x, points[index].x, ref xVel, 0.3F, Mathf.Infinity, Time.deltaTime / points[index].z);
		Current.y = Mathf.SmoothDamp(Current.y, points[index].y, ref yVel, 0.3F, Mathf.Infinity, Time.deltaTime / points[index].z);
		gameObject.GetComponent<RectTransform>().localPosition = Current;
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

	public void SetTo(int index)
	{
		Current = new Vector2(points[index].x, points[index].y);
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
		if(resetOnEnable)
		{
			Reset();
		}
	}
}
