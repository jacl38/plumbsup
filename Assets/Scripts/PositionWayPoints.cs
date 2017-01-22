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

	void Start () {
		Current = new Vector2(points[0].x, points[0].y);
		Begin = Current;
		gameObject.GetComponent<RectTransform>().localPosition = new Vector2(points[0].x, points[0].y);
		time = 0;
	}

	void Update()
	{
		time += Time.deltaTime;
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
}
