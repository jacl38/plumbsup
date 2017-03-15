using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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
		if(s > 0)
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
				new Vector3((0.1f * Mathf.Cos(0.25f * Time.frameCount / Mathf.PI) + 0.95f),
					(0.1f * Mathf.Cos(0.25f * Time.frameCount / Mathf.PI) + 0.95f), 1);
		}

		if(seconds == 0)
		{
			reset = true;
			panel.GetComponent<SmartPopulate>().disableAll();
			if(panel.transform.GetChild(panel.GetComponent<SmartPopulate>().getStart())
				.GetComponent<TileController>().Begin == TileController.Direction.UP)
			{
				panel.transform.GetChild(panel.GetComponent<SmartPopulate>().getStart())
					.GetComponent<FillController>().StartFill();
			} else
				panel.GetComponent<SmartPopulate>().endGame();
			if(panel.transform.parent.gameObject.GetComponent<ScaleWayPoints>() != null && panel.transform.parent.gameObject.GetComponent<ScaleWayPoints>().ended())
			{
				Destroy(panel.transform.parent.gameObject.GetComponent<ScaleWayPoints>());
			}
			if(panel.transform.parent.gameObject.GetComponent<RotateWayPoints>() != null && panel.transform.parent.gameObject.GetComponent<RotateWayPoints>().ended())
			{
				Destroy(panel.transform.parent.gameObject.GetComponent<RotateWayPoints>());
			}
			if(panel.transform.GetChild((DifficultySelect.Difficulty*DifficultySelect.Difficulty) - DifficultySelect.Difficulty + panel.GetComponent<SmartPopulate>().getEnd())
				.GetComponent<TileController>().FillAmount >= 1)
			{
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
	}

	public void resetPanel()
	{
		if(reset)
		{
			panel.GetComponent<SmartPopulate>().generateTiles();
			reset = false;
			seconds = originalSeconds;
			s = seconds + 0.5f;
		}
	}
}
