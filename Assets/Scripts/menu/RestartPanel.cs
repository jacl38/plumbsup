using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartPanel : MonoBehaviour {

	public GameObject mainPanel;
	public GameObject endPanel;
	public GameObject Coin;
	public SaveData sd;
	GameObject water;
	private int totalScore;

	void Start () {
		water = gameObject.transform.parent.GetChild(3).gameObject;
		totalScore = ScoresController.normalCount + (ScoresController.silverCount * 10) + (ScoresController.goldenCount * 50);
		sd = new SaveData();
		FileHandler.Load(ref sd, "save");
		sd.addScore(DifficultySelect.Difficulty, totalScore);
		sd.addCoins(Mathf.RoundToInt(totalScore / 10f));
		FileHandler.Save(sd, "save");
		GameObject.Find("CoinCount").GetComponent<Text>().text = "" + sd.getCoins();
		string circle = "Bought";
		GameObject.Find("Pipes").GetComponent<Text>().text = "<color=\"#" + ColorUtility.ToHtmlStringRGB(ThemeHolder.theme.standardcolor) + "\">" + circle + "</color>\n" + 
															 "<color=\"#" + ColorUtility.ToHtmlStringRGB(ThemeHolder.theme.mediumcolor  ) + "\">" + circle + "</color>\n" + 
															 "<color=\"#" + ColorUtility.ToHtmlStringRGB(ThemeHolder.theme.bestcolor    ) + "\">" + circle + "</color>\n";
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
				"= " + totalScore + "\n/ 10 = " + Mathf.RoundToInt(totalScore/10f);
		}
		setCoinAfterText(GameObject.Find("CoinCount"), Coin);
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

	public static void setCoinAfterText(GameObject text, GameObject CoinImage)
	{
		Font f = text.GetComponent<Text>().font;
		CharacterInfo cin = new CharacterInfo();
		float sum = 0;
		for(int i = 0; i < text.GetComponent<Text>().text.Length; i++)
		{
			f.GetCharacterInfo(text.GetComponent<Text>().text[i], out cin, text.GetComponent<Text>().fontSize);
			sum += cin.advance/2;
		}
		CoinImage.transform.localPosition = new Vector2(sum + CoinImage.GetComponent<RectTransform>().sizeDelta.x/2, 0);
		text.transform.localPosition = new Vector2(-CoinImage.GetComponent<RectTransform>().sizeDelta.x/2, 0);
	}
}
