﻿using UnityEngine;
using System.Collections;

public class ScaleWayPoints : MonoBehaviour {
	
	private Vector2 objectScale;
	public Vector3[] points;
	private int index = 0;
	private Vector2 Current;
	private float xVel = 0.0F;
	private float yVel = 0.0F;
	private float time;

	void Start () {
		objectScale = gameObject.transform.localScale;
		Current = new Vector2(points[0].x, points[0].y);
		gameObject.transform.localScale = new Vector3(points[0].x, points[0].y, 1);
		time = 0;
	}
	
	public void Update () {
		time += Time.deltaTime;

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
}
