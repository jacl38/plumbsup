using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartPanel : MonoBehaviour {

	public GameObject mainPanel;
	public GameObject endPanel;
	GameObject water;
	private int totalScore;

	void Start () {
		water = gameObject.transform.parent.GetChild(3).gameObject;
		totalScore = ScoresController.normalCount + (ScoresController.silverCount * 10) + (ScoresController.goldenCount * 50);
		SaveData sd = new SaveData();
		FileHandler.Load(ref sd, "save");
		sd.addScore(DifficultySelect.Difficulty, totalScore);
		FileHandler.Save(sd, "save");
	}

	void Update () {
		if(water.GetComponent<FadeIn>() == null)
		{
			water.AddComponent<FadeIn>();
			water.GetComponent<FadeIn>().startPercent = 1.0f;
			water.GetComponent<FadeIn>().endPercent = 0.0f;
			water.GetComponent<FadeIn>().timeScale = 2f;
			GameObject.Find("Amounts").GetComponent<Text>().text = "x" + ScoresController.normalCount + "\n" +
				"x" + ScoresController.silverCount + "\n" +
				"x" + ScoresController.goldenCount + "\n" +
				"= " + totalScore;
		}
	}

	public void TryAgain()
	{
		if(water.GetComponent<FadeIn>() != null)
		{
			water.GetComponent<Image>().color = water.GetComponent<FadeIn>().startColor;
			Destroy(water.GetComponent<FadeIn>());
			water.transform.localScale = new Vector3(1, 1, 1);
		}
		gameObject.transform.parent.GetChild(0).GetChild(0).GetChild(1).GetComponent<Timer>().resetPanel();
		endPanel.SetActive(false);
		mainPanel.SetActive(true);
		ScoresController.goldenCount = 0;
		ScoresController.normalCount = 0;
		ScoresController.silverCount = 0;
	}

	public void Menu()
	{
		SceneManager.LoadScene("MenuScreen");
	}
}
