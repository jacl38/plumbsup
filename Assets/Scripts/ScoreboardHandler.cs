using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreboardHandler : MonoBehaviour {

	int[] scores;
	Text text;

	void Start ()
	{
		PlayerPrefs.DeleteAll();
		scores = new int[PlayerPrefs.GetInt("size")];
		if(scores.Length > 0)
		{
			for(int i = 0; i < scores.Length; i++)
			{
				scores[i] = PlayerPrefs.GetInt("score_" + i);
			}
		}
		foreach(int i in scores) Debug.Log(scores[i]);
		text = GetComponent<Text>();
		foreach(int i in scores)
		{
			text.text += scores[i] + "\n";
		}
	}

	void Update ()
	{
		
	}
}
