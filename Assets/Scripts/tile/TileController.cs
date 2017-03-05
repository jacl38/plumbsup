using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[ExecuteInEditMode]
public class TileController : MonoBehaviour
{
	public enum Direction
	{
		UP,
		DOWN,
		LEFT,
		RIGHT
	};

	public enum PipeType
	{
		STRAIGHT,
		CORNER
	};

	private GameObject DU;
	public GameObject getDU() { return DU; }
	private GameObject ML;
	public GameObject getML() { return ML; }
	private GameObject MR;
	public GameObject getMR() { return MR; }
	private GameObject Pipe;
	public GameObject getPipe() { return Pipe; }

	public Sprite Straight;
	public Sprite Corner;

	[Range(0.0f, 1.0f)]
	public float FillAmount;

	public float FullSize;
	public int Degree;

	public Direction Begin;
	public Direction End;

	public Color PipeColor;
	public Color FillColor;

	private Quaternion angle;
	public void SetAngle(Quaternion angle){ this.angle = angle; }
	public Quaternion GetAngle() { return this.angle; }

	public PipeType getType()
	{
		if((Begin == Direction.DOWN && End == Direction.UP) || (Begin == Direction.LEFT && End == Direction.RIGHT))
		{
			return PipeType.STRAIGHT;
		}
		else
		{
			return PipeType.CORNER;
		}
	}

	void Start()
	{
		DoFill();
		DU = gameObject.transform.GetChild(0).gameObject;
		ML = gameObject.transform.GetChild(1).gameObject;
		MR = gameObject.transform.GetChild(2).gameObject;
		Pipe = gameObject.transform.GetChild(3).gameObject;
	}

	void Update()
	{
		DoFill();
	}

	void DoFill()
	{
		DU = gameObject.transform.GetChild(0).gameObject;
		ML = gameObject.transform.GetChild(1).gameObject;
		MR = gameObject.transform.GetChild(2).gameObject;
		Pipe = gameObject.transform.GetChild(3).gameObject;
		gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(FullSize/Degree, FullSize/Degree);
		Pipe.GetComponent<RectTransform>().sizeDelta = new Vector2(FullSize / Degree, FullSize / Degree);

		var pipeColors = Pipe.GetComponent<Button>().colors;
		pipeColors.normalColor = PipeColor;
		Pipe.GetComponent<Button>().colors = pipeColors;
		Pipe.GetComponent<Image>().color = PipeColor;
		DU.GetComponent<Image>().color = FillColor;
		ML.GetComponent<Image>().color = FillColor;
		MR.GetComponent<Image>().color = FillColor;

		if (Begin == End)
		{
			if (Begin == Direction.LEFT) End = Direction.RIGHT;
			else if (Begin == Direction.RIGHT) End = Direction.LEFT;
			else if (Begin == Direction.DOWN) End = Direction.UP;
			else if (Begin == Direction.UP) End = Direction.DOWN;
		}

		if((Begin == Direction.DOWN && End == Direction.UP) || 
			(Begin == Direction.UP && End == Direction.DOWN) || 
			(Begin == Direction.LEFT && End == Direction.RIGHT) || 
			(Begin == Direction.RIGHT && End == Direction.LEFT)) {
			if (Begin == Direction.DOWN && End == Direction.UP) gameObject.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 0);
			if (Begin == Direction.RIGHT && End == Direction.LEFT) gameObject.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 90);
			if (Begin == Direction.UP && End == Direction.DOWN) gameObject.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 180);
			if (Begin == Direction.LEFT && End == Direction.RIGHT) gameObject.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 270);
			Pipe.GetComponent<Image>().sprite = Straight;

			MR.GetComponent<RectTransform>().sizeDelta = Vector3.zero;
			ML.GetComponent<RectTransform>().sizeDelta = Vector3.zero;

			DU.GetComponent<RectTransform>().sizeDelta = new Vector3(FullSize / Degree / 8, FillAmount * FullSize / Degree);
			DU.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, (FullSize / Degree * FillAmount) / 2 - (FullSize / Degree / 2), 1);
			//fill straight
		}
		else
		{
			Pipe.GetComponent<Image>().sprite = Corner;
			if((Begin == Direction.DOWN && End == Direction.LEFT) || 
				(Begin == Direction.RIGHT && End == Direction.DOWN) ||
				(Begin == Direction.UP && End == Direction.RIGHT) ||
				(Begin == Direction.LEFT && End == Direction.UP)) {
				gameObject.transform.GetComponent<RectTransform>().localScale = new Vector3(-1, 1, 1);
				if (Begin == Direction.DOWN && End == Direction.LEFT)  { this.angle = Quaternion.Euler(0, 0, 0);   gameObject.transform.rotation = angle; }
				if (Begin == Direction.RIGHT && End == Direction.DOWN) { this.angle = Quaternion.Euler(0, 0, 90);  gameObject.transform.rotation = angle; }
				if (Begin == Direction.UP && End == Direction.RIGHT)   { this.angle = Quaternion.Euler(0, 0, 180); gameObject.transform.rotation = angle; }
				if (Begin == Direction.LEFT && End == Direction.UP)    { this.angle = Quaternion.Euler(0, 0, 270); gameObject.transform.rotation = angle; }
			}
			else
			{
				gameObject.transform.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
				if (Begin == Direction.DOWN && End == Direction.RIGHT)
				{ this.angle = Quaternion.Euler(0, 0, 0);   gameObject.GetComponent<RectTransform>().rotation = angle; }
				if (Begin == Direction.RIGHT && End == Direction.UP)
				{ this.angle = Quaternion.Euler(0, 0, 90);  gameObject.GetComponent<RectTransform>().rotation = angle; }
				if (Begin == Direction.UP && End == Direction.LEFT)
				{ this.angle = Quaternion.Euler(0, 0, 180); gameObject.GetComponent<RectTransform>().rotation = angle; }
				if (Begin == Direction.LEFT && End == Direction.DOWN)
				{ this.angle = Quaternion.Euler(0, 0, 270); gameObject.GetComponent<RectTransform>().rotation = angle; }
			}
			ML.GetComponent<RectTransform>().sizeDelta = new Vector3(0, 0);
			ML.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
			if (FillAmount <= 0.5625)
			{
				MR.GetComponent<RectTransform>().sizeDelta = new Vector3(0, 0);
				MR.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);

				DU.GetComponent<RectTransform>().sizeDelta = new Vector3(FullSize / Degree / 8, FillAmount * FullSize / Degree);
				DU.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, (FullSize / Degree * FillAmount) / 2 - (FullSize / Degree / 2), 1);
			}
			else
			{
				DU.GetComponent<RectTransform>().sizeDelta = new Vector3(FullSize / Degree / 8, (9F / 16F) * FullSize / Degree);
				DU.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, (FullSize / Degree * (9F / 16F)) / 2 - (FullSize / Degree / 2), 1);

				MR.GetComponent<RectTransform>().sizeDelta = new Vector3((FillAmount - .5625F) * FullSize / Degree, FullSize / Degree / 8);
				MR.GetComponent<RectTransform>().anchoredPosition = new Vector3(FullSize / Degree * ((8F * FillAmount) / 16F) - (7F * FullSize / Degree) / 32F, 0, 0);
			}
			//fill corner
		}
	}
}