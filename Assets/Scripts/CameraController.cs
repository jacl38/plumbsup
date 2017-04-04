using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public Camera cam;
	Color c = new Color(0, 0, 0);
	public Gradient bgColor;
	public float speed = 1;
	public float offset;

	void Start () {
		cam = gameObject.GetComponent<Camera>();
		offset = 0;
	}
	
	void Update () {
		//c.r = (float)((0.5 * (Mathf.Cos(Time.time / 4))) + 0.5);
		//c.g = (float)((0.5 * (Mathf.Cos(Time.time / 4 - ((2 * Mathf.PI) / 3)))) + 0.5);
		//c.b = (float)((0.5 * (Mathf.Cos(Time.time / 4 - ((4 * Mathf.PI) / 3)))) + 0.5);
		c = bgColor.Evaluate(offset + (Time.time * speed) % 1);
		cam.backgroundColor = c;
	}
}
