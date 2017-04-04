using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToOtherPanel : MonoBehaviour {

	public GameObject lastPanel;

	void Start () {
		
	}

	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			lastPanel.SetActive(true);
			gameObject.SetActive(false);
		}
	}
}
