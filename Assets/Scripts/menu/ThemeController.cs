using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThemeController : MonoBehaviour {

	public string name;
	public int cost;
	public Sprite straightPipe;
	public Sprite cornerPipe;
	public Color standardcolor;
	public Color mediumcolor;
	public Color bestcolor;
	public Color watercolor;
	public Gradient bg;

	private GameObject nameObject;
	private GameObject standard;
	private GameObject medium;
	private GameObject best;
	private GameObject costText;
	private SaveData sd = new SaveData();

	void Start ()
	{
		FileHandler.Load(ref sd, "save");
		nameObject = transform.GetChild(0).gameObject;
		standard = transform.GetChild(1).gameObject;
		medium   = transform.GetChild(2).gameObject;
		best     = transform.GetChild(3).gameObject;
		costText = transform.GetChild(4).gameObject;

		nameObject .GetComponent<Text>() .text   = name;
		standard   .GetComponent<Image>().sprite = straightPipe;
		medium     .GetComponent<Image>().sprite = straightPipe;
		best       .GetComponent<Image>().sprite = straightPipe;

		standard   .GetComponent<Image>().color  = standardcolor;
		medium     .GetComponent<Image>().color  = mediumcolor;
		best       .GetComponent<Image>().color  = bestcolor;
		if(sd.themes.Contains(transform.GetSiblingIndex()))
		{
			if(sd.themeIndex == transform.GetSiblingIndex())
			{
				costText.GetComponent<Text>().text = "Selected";
			}
			else costText.GetComponent<Text>().text = "Bought";
		}
		else
			costText.GetComponent<Text>().text = cost + "";
	}

	void Update ()
	{
		float speed = GameObject.Find("Main Camera").GetComponent<CameraController>().speed;
		gameObject.GetComponent<Image>().color = bg.Evaluate((Time.frameCount * speed * speed/10) % 1);
	}
}
