using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public Camera cam;
	Color c = new Color(0, 0, 0);
	public Gradient bgColor;
	public float speed = 1;
	public GameObject themeContent;

	void Start () {
		cam = gameObject.GetComponent<Camera>();
		bgColor = ThemeHolder.theme.bg;
	}
	
	void Update () {
		//c.r = (float)((0.5 * (Mathf.Cos(Time.time / 4))) + 0.5);
		//c.g = (float)((0.5 * (Mathf.Cos(Time.time / 4 - ((2 * Mathf.PI) / 3)))) + 0.5);
		//c.b = (float)((0.5 * (Mathf.Cos(Time.time / 4 - ((4 * Mathf.PI) / 3)))) + 0.5);
		c = bgColor.Evaluate((Time.frameCount * speed * speed/10) % 1);
		cam.backgroundColor = c;
	}

	public void setGradient(Gradient g)
	{
		bgColor = g;
	}
}
