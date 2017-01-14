using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[ExecuteInEditMode]
public class Tile : MonoBehaviour {

	public float FullSize;
	public float Degree;
	public Sprite straight;
	public Sprite corner;
	public Color pipeColor;
	public Color fillColor;
	private GameObject pipe;
	private GameObject[] fills = new GameObject[3];

	public enum Direction
	{
		UP,
		DOWN,
		LEFT,
		RIGHT
	};

	public Direction start;
	public Direction end;

	void Start()
	{
		pipe = gameObject.transform.GetChild(3).gameObject;
		for(int i = 0; i < fills.Length; i++)
		{
			fills[i] = gameObject.transform.GetChild(i).gameObject;
			fills[i] = gameObject.transform.GetChild(i).gameObject;
			fills[i] = gameObject.transform.GetChild(i).gameObject;
		}
	}

	void Update() {
		updateColor();
		gameObject.GetComponent<FillController>().setSize(FullSize/Degree);
		gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(FullSize/Degree, FullSize/Degree);
		gameObject.transform.GetChild(3).GetComponent<RectTransform>().sizeDelta = new Vector2(FullSize/Degree, FullSize/Degree);

		if(start == end)
		{
			if (start == Direction.LEFT) end = Direction.RIGHT;
			else if (start == Direction.RIGHT) end = Direction.LEFT;
			else if (start == Direction.DOWN) end = Direction.UP;
			else if (start == Direction.UP) end = Direction.DOWN;
		}

		if((start == Direction.DOWN && end == Direction.UP) || (start == Direction.UP && end == Direction.DOWN))
		{
			pipe.GetComponent<Image>().sprite = straight;
			gameObject.GetComponent<FillController>().setType(FillController.PipeType.STRAIGHT);
		}
		else
		{
			pipe.GetComponent<Image>().sprite = corner;
			gameObject.GetComponent<FillController>().setType(FillController.PipeType.CORNER);
		}

//		if (leading == Direction.UP) { gameObject.GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, 90); }
//		else if (leading == Direction.DOWN) { gameObject.GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, 270); }
//		else if (leading == Direction.LEFT) { gameObject.GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, 180); }
//		else if (leading == Direction.RIGHT) { gameObject.GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, 0); }
	}

	public void updateDirection()
	{

	}

	public void updateColor()
	{
		//pipe.GetComponent<Image>().color = pipeColor;
		for(int i = 0; i < fills.Length; i++)
		{
			fills[i].GetComponent<Image>().color = fillColor;
		}
	}

	public Direction getStart()
	{
		return this.start;
	}

	public Direction getEnd()
	{
		return this.end;
	}
}
