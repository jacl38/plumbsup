using UnityEngine;
using System.Collections;

[RequireComponent (typeof (UnityEngine.UI.Text))]
public class ButtonText : MonoBehaviour {

	public string[] labels;
	int index = 0;

	void Start () {
	
	}
	
	void Update () {
		
	}

	public void IncrementLabels()
	{
		if (index < labels.Length - 1) index++;
		else if (index == labels.Length - 1) index = 0;
		gameObject.GetComponent<UnityEngine.UI.Text>().text = labels[index];
	}
}
