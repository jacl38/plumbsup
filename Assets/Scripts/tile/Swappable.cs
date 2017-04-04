using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Swappable : MonoBehaviour {

	bool selected = false;
	public bool allowed = true;

	private AudioSource SelectSource;
	private AudioSource DeselectSource;
	public Transform Pipe;
	private Vector3 angle;
	GameObject[] pipeObjects;

	void Start () {
		pipeObjects = new GameObject[4];
		pipeObjects[0] = gameObject.GetComponent<TileController>().getPipe();
		pipeObjects[1] = gameObject.GetComponent<TileController>().getML();
		pipeObjects[2] = gameObject.GetComponent<TileController>().getMR();
		pipeObjects[3] = gameObject.GetComponent<TileController>().getDU();
		SelectSource = GameObject.FindGameObjectWithTag("SelectSource").GetComponent<AudioSource>();
		DeselectSource = GameObject.FindGameObjectWithTag("DeselectSource").GetComponent<AudioSource>();
	}
	
	void Update () {
		if(!allowed)
			Deselect();
		if(gameObject.GetComponent<ScaleWayPoints>() != null)
		{
			if(gameObject.GetComponent<ScaleWayPoints>().ended())
				DestroyImmediate(gameObject.GetComponent<ScaleWayPoints>());
		}
		angle = new Vector3(
			gameObject.GetComponent<TileController>().GetAngle().x,
			gameObject.GetComponent<TileController>().GetAngle().y,
			gameObject.GetComponent<TileController>().GetAngle().z
		);

		if(selected && allowed)
		{
			pipeObjects[0].GetComponent<RectTransform>().localEulerAngles = 
				angle +
				new Vector3(0, 0, 3f * Mathf.Cos(Time.frameCount / 6f));
			pipeObjects[0].GetComponent<RectTransform>().localScale = 
				new Vector3((0.1f * Mathf.Cos(Time.frameCount / 16f) + 0.95f),
				(0.1f * Mathf.Cos(Time.frameCount / 16f) + 0.95f), 1);
		}
		else
		{
			gameObject.GetComponent<TileController>().getPipe().GetComponent<RectTransform>().localScale = Vector3.one;
			gameObject.GetComponent<TileController>().getPipe().GetComponent<RectTransform>().localEulerAngles = Vector3.zero;
		}
	}

	public void Select()
	{
		int index = transform.GetSiblingIndex();
		int size = DifficultySelect.Difficulty;
		int x = index % size;
		int y = Mathf.FloorToInt(index/size);
		if(!SmartPopulate.tiles[x, y].stuck && allowed && !(gameObject.GetComponent<TileController>().FillAmount > 0))
		{
			this.selected = true;
			gameObject.GetComponent<Image>().color = new Color(0, 0, 0, 0.3f);
			SelectSource.Play();
		}
	}

	public void Deselect()
	{
		this.selected = false;
		gameObject.GetComponent<Image>().color = new Color(0, 0, 0, 0.0f);
 		DeselectSource.Play();
	}

	public void OnClick()
	{
		DestroyImmediate(gameObject.GetComponent<ScaleWayPoints>());
		bool hasSelected = false;
		var parent = gameObject.transform.parent;
		for(int i = 0; i < parent.childCount; i++)
		{
			if (parent.GetChild(i).gameObject.GetComponent<Swappable>().selected)
			{
				hasSelected = true;
				break;
			}
		}

		int index = transform.GetSiblingIndex();
		int size = DifficultySelect.Difficulty;
		int x = index % size;
		int y = Mathf.FloorToInt(index/size);

		if (!hasSelected || SmartPopulate.tiles[x, y].stuck)
		{
			Select();
		}
		else
		{
			for (int i = 0; i < parent.childCount; i++)
			{
				if (parent.GetChild(i).gameObject.GetComponent<Swappable>().selected && i != gameObject.transform.GetSiblingIndex())
				{
					var ThisPipe = parent.GetChild(gameObject.transform.GetSiblingIndex()).GetComponent<TileController>(); //AKA "gameObject"
					var OtherPipe = parent.GetChild(i).gameObject.GetComponent<TileController>(); //Tile to be swapped with
					TileController.Direction thisTempBegin = ThisPipe.Begin;
					TileController.Direction thisTempEnd = ThisPipe.End;
					Color thisPipeColor = ThisPipe.PipeColor;
					Color thisFillColor = ThisPipe.FillColor;
					TileController.Direction otherTempBegin = OtherPipe.Begin;
					TileController.Direction otherTempEnd = OtherPipe.End;
					Color otherPipeColor = OtherPipe.PipeColor;
					Color otherFillColor = OtherPipe.FillColor;
					
					OtherPipe.Begin = thisTempBegin;
					OtherPipe.End = thisTempEnd;
					OtherPipe.PipeColor = thisPipeColor;
					OtherPipe.FillColor = thisFillColor;
					ThisPipe.Begin = otherTempBegin;
					ThisPipe.End = otherTempEnd;
					ThisPipe.PipeColor = otherPipeColor;
					ThisPipe.FillColor = otherFillColor;

					gameObject.transform.parent.GetChild(i).gameObject.GetComponent<Swappable>().Deselect();

					ThisPipe.gameObject.AddComponent(typeof(ScaleWayPoints));

					ThisPipe.gameObject.GetComponent<ScaleWayPoints>().points = new Vector3[3]
					{
//						new Vector3(0.1f, 0.1f, 0.01f),
//						new Vector3(1.0f, 1.0f, 0.25f)
						new Vector3(1.1f, 1.1f, 0.025f),
						new Vector3(0.9f, 0.9f, 0.15f),
						new Vector3(1.0f, 1.0f, 0.15f)
					};
					
					OtherPipe.gameObject.AddComponent(typeof(ScaleWayPoints));

					OtherPipe.gameObject.GetComponent<ScaleWayPoints>().points = new Vector3[3]
					{

//						new Vector3(0.1f, 0.1f, 0.01f),
//						new Vector3(1.0f, 1.0f, 0.25f)
						new Vector3(1.1f, 1.1f, 0.025f),
						new Vector3(0.9f, 0.9f, 0.15f),
						new Vector3(1.0f, 1.0f, 0.15f)
					};

					break;
				}
			}
			Deselect();
		}
	}
}
