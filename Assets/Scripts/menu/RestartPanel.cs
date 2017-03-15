using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartPanel : MonoBehaviour {

	public GameObject mainPanel;
	public GameObject endPanel;

	void Start () {
		
	}

	void Update () {
		
	}

	public void TryAgain()
	{
		gameObject.transform.parent.GetChild(0).GetChild(0).GetChild(1).GetComponent<Timer>().resetPanel();
		mainPanel.SetActive(true);
		endPanel.SetActive(false);
	}

	public void Menu()
	{
		SceneManager.LoadScene("MenuScreen");
	}
}
