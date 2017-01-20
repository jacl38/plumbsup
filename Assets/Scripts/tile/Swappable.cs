using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Swappable : MonoBehaviour {

	bool selected = false;

	public Transform Pipe;

	void Start () {
	}
	
	void Update () {
		if(selected)
		{
			gameObject.GetComponent<TileController>().getPipe().GetComponent<RectTransform>().localEulerAngles = 
				new Vector3(0, 0, 3f * Mathf.Cos(Time.frameCount/4f));
			gameObject.GetComponent<TileController>().getPipe().GetComponent<RectTransform>().localScale = 
				new Vector3((0.1f * Mathf.Cos(Time.frameCount / 10f) + 0.95f), (0.1f * Mathf.Cos(Time.frameCount / 10f) + 0.95f), 1);
		}
		else
		{
			gameObject.GetComponent<TileController>().getPipe().GetComponent<RectTransform>().localScale = Vector3.one;
			gameObject.GetComponent<TileController>().getPipe().GetComponent<RectTransform>().localEulerAngles = Vector3.zero;
		}
	}

	public void Select()
	{
		this.selected = true;
		gameObject.GetComponent<Image>().color = new Color(0, 0, 0, 0.3f);
	}

	public void Deselect()
	{
		this.selected = false;
		gameObject.GetComponent<Image>().color = new Color(0, 0, 0, 0.0f);
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

		if (!hasSelected)
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
					TileController.Direction otherTempBegin = OtherPipe.Begin;
					TileController.Direction otherTempEnd = OtherPipe.End;

					OtherPipe.Begin = thisTempBegin;
					OtherPipe.End = thisTempEnd;
					ThisPipe.Begin = otherTempBegin;
					ThisPipe.End = otherTempEnd;

					gameObject.transform.parent.GetChild(i).gameObject.GetComponent<Swappable>().Deselect();

					ThisPipe.gameObject.AddComponent(typeof(ScaleWayPoints));

					ThisPipe.gameObject.GetComponent<ScaleWayPoints>().points = new Vector3[2]
					{
						new Vector3(0.1f, 0.1f, 0.01f),
						new Vector3(1.0f, 1.0f, 0.1f)
					};
					
					OtherPipe.gameObject.AddComponent(typeof(ScaleWayPoints));

					OtherPipe.gameObject.GetComponent<ScaleWayPoints>().points = new Vector3[2]
					{
						new Vector3(0.1f, 0.1f, 0.01f),
						new Vector3(1.0f, 1.0f, 0.1f)
					};

					break;
				}
			}
			Deselect();
		}
	}
}
