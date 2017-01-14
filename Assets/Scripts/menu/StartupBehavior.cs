using UnityEngine;
using System.Collections;

public class StartupBehavior : MonoBehaviour {

	public GameObject mainPanel;
	public GameObject helpPanel;
	public GameObject levelsPanel;

	void Start () {
		mainPanel.SetActive(true);
		helpPanel.SetActive(false);
		levelsPanel.SetActive(false);
	}
	
	void Update () {
	
	}
}
