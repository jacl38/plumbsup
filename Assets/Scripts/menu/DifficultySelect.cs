using UnityEngine;
using System.Collections;

public class DifficultySelect : MonoBehaviour {

	public static int MinDifficulty = 5;
	public static int MaxDifficulty = 10;
	public static int Difficulty;
	public UnityEngine.UI.Slider slider;
	public UnityEngine.UI.Text tx;

	void Start () {
		slider.minValue = MinDifficulty - 1;
		slider.maxValue = MaxDifficulty - 1;
		tx.text = "" + MinDifficulty;
		Difficulty = 1;
	}
	
	void Update ()
	{
		Difficulty = (int) Mathf.Round(slider.value+1);
		string val = "" + Difficulty;
		tx.text = val;
	}
}
