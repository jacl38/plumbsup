using UnityEngine;
using System.Collections;

public class DifficultySelect : MonoBehaviour {

	public UnityEngine.UI.Slider slider;
	public UnityEngine.UI.Text tx;

	void Start () {
		tx.text = "1";
	}
	
	void Update ()
	{
		string val = "" + Mathf.Round((slider.value+1));
		tx.text = val;
	}
}
