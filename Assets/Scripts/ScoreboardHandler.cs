using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreboardHandler : MonoBehaviour {

	SaveData sd = new SaveData();
	List<Score> scores = new List<Score>();
	public Transform scorePrefab;
	private GameObject noScoresText;
	private GameObject content;
	private GameObject clearButton;

	void Start ()
	{
		FileHandler.Load(ref sd, "save");
		sd.SortScores();
		scores = sd.getScores();
		foreach(Score s in scores)
		{
			Transform t = Instantiate(scorePrefab, transform.position, transform.rotation) as Transform;
			GameObject tex = t.gameObject;
			t.transform.SetParent(gameObject.transform);
			t.transform.localScale = Vector3.one;
			Text text = tex.GetComponent<Text>();
			text.text = s.getDifficulty() + "x" + s.getDifficulty() + " " + s.getScore();
		}
		noScoresText = GameObject.Find("NoScoresText");
		clearButton = GameObject.Find("ClearButton");
		if(scores.Count == 0)
		{
			clearButton.gameObject.SetActive(false);
			noScoresText.gameObject.SetActive(true);
		}
		else
		{
			noScoresText.gameObject.SetActive(false);
			clearButton.gameObject.SetActive(true);
		}
		content = GameObject.Find("Content");
	}

	void Update()
	{
		//check if it has scalewaypoints
			//check if it's finished
				//destroy it
		//otherwise, add it from its current scale to 0 or current scale to 1
		if(content.transform.localPosition.y <= 0)
		{
			clearButton.SetActive(true);
		} else
			clearButton.SetActive(false);
	}

	public void confirmClear()
	{
		sd = new SaveData();
		FileHandler.Save(sd, "save");
		GameObject.Find("ConfirmClearPanel").SetActive(false);
		int j = transform.childCount;
		for(int i = 0; i < j; i++)
		{
			DestroyImmediate(transform.GetChild(0).gameObject);
		}
		GameObject.Find("ClearButton").SetActive(false);
		noScoresText.gameObject.SetActive(true);
	}
}
