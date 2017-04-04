using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Timer : MonoBehaviour {

	public int seconds;
	private int originalSeconds;
	float s;
	bool reset = false;

	GameObject panel;

	void Start () {
		panel = gameObject.transform.parent.GetChild(0).GetChild(0).gameObject;
		seconds = 10 + 5 * (DifficultySelect.Difficulty - 3);
		originalSeconds = seconds;
		s = seconds + 1.0f;
	}
	
	void Update () {
		if(s > 0 && !reset)
		s -= Time.deltaTime;
		seconds = (int) s;
		if(seconds % 60 < 10) {
			gameObject.GetComponent<Text>().text = (seconds / 60).ToString() + ":0" + (seconds % 60).ToString();
		} else {
			gameObject.GetComponent<Text>().text = (seconds / 60).ToString() + ":" + (seconds % 60).ToString();
		}

		if(seconds <= 5)
		{
			gameObject.GetComponent<RectTransform>().localScale = 
				new Vector3((0.1f * Mathf.Cos(4 * Time.frameCount * Mathf.Deg2Rad) + 0.95f),
							(0.1f * Mathf.Cos(4 * Time.frameCount * Mathf.Deg2Rad) + 0.95f), 1);
		}

		if(seconds == 0)
		{
			startFlow();
		}

		GameObject lastPipe = panel.transform.GetChild((DifficultySelect.Difficulty * DifficultySelect.Difficulty) - DifficultySelect.Difficulty + panel.GetComponent<SmartPopulate>().getEnd()).gameObject;
		if(lastPipe.GetComponent<TileController>().FillAmount >= 1 && lastPipe.GetComponent<TileController>().End == TileController.Direction.DOWN)
		{
			List<Tile> path = new List<Tile>();
			for(int i = 0; i < panel.transform.childCount; i++)
			{
				int x = i % (int) Mathf.Sqrt(panel.transform.childCount);
				int y = i / (int) Mathf.Sqrt(panel.transform.childCount);
				if(SmartPopulate.tiles[x, y].path)
				{
					path.Add(SmartPopulate.tiles[x, y]);
				}
			}

			foreach(Tile t in path)
			{
				if(t.value > 0.8f)
				{
					ScoresController.goldenCount++;
				}
				else if(t.value > 0)
				{
					ScoresController.silverCount++;
				}
				else if(t.value < 0)
				{
					ScoresController.normalCount++;
				}
			}

			if(panel.transform.parent.gameObject.GetComponent<ScaleWayPoints>() == null)
			{
				panel.transform.parent.gameObject.AddComponent(typeof(ScaleWayPoints));

				panel.transform.parent.gameObject.GetComponent<ScaleWayPoints>().points = new Vector3[3] {
					new Vector3(1.1f, 1.1f, 0.025f),
					new Vector3(0.9f, 0.9f, 0.15f),
					new Vector3(1.0f, 1.0f, 0.15f)
				};
			}

			if(panel.transform.parent.gameObject.GetComponent<RotateWayPoints>() == null)
			{
				panel.transform.parent.gameObject.AddComponent(typeof(RotateWayPoints));

				panel.transform.parent.gameObject.GetComponent<RotateWayPoints>().points = new Vector2[2] {
					new Vector2(0f, 0f),
					new Vector2(360, 0.5f)
				};
			}
			resetPanel();
		}
	}

	public void resetPanel()
	{
		if(reset)
		{
			panel.GetComponent<SmartPopulate>().generate();
			reset = false;
			seconds = originalSeconds;
			s = seconds + 1.0f;
		}
	}

	public void startFlow()
	{
		reset = true;
		panel.GetComponent<SmartPopulate>().disableAll();
		if(panel.transform.GetChild(panel.GetComponent<SmartPopulate>().getStart())
			.GetComponent<TileController>().Begin == TileController.Direction.UP)
		{
			panel.transform.GetChild(panel.GetComponent<SmartPopulate>().getStart())
				.GetComponent<FillController>().StartFill();
		} else
		{
			panel.GetComponent<SmartPopulate>().endGame();
		}
		if(panel.transform.parent.gameObject.GetComponent<ScaleWayPoints>() != null && panel.transform.parent.gameObject.GetComponent<ScaleWayPoints>().ended())
		{
			Destroy(panel.transform.parent.gameObject.GetComponent<ScaleWayPoints>());
			panel.transform.parent.gameObject.transform.localScale = Vector2.one;
		}
		if(panel.transform.parent.gameObject.GetComponent<RotateWayPoints>() != null && panel.transform.parent.gameObject.GetComponent<RotateWayPoints>().ended())
		{
			Destroy(panel.transform.parent.gameObject.GetComponent<RotateWayPoints>());
			panel.transform.parent.gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
		}
		if(gameObject.GetComponent<RotateWayPoints>() != null && gameObject.GetComponent<RotateWayPoints>().ended())
		{
			Destroy(gameObject.GetComponent<RotateWayPoints>());
		}
	}
}
