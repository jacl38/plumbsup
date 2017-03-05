using UnityEngine;
using System.Collections;

public class StartupBehavior : MonoBehaviour {
	
	GameObject[] panels;
	public GameObject mainPanel;

	void Start() {
		panels = GameObject.FindGameObjectsWithTag("Panel");
		foreach(GameObject p in panels) p.SetActive(false);
		mainPanel.SetActive(true);
	}
}