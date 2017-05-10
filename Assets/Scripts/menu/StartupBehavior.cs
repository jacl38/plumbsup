using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StartupBehavior : MonoBehaviour {
	
	GameObject[] panels;
	public GameObject mainPanel;
	public ThemeController defaultTheme;
	List<ThemeController> avail = new List<ThemeController>();
	public GameObject themeContent;
	SaveData sd = new SaveData();

	void Start() {
		FileHandler.Load(ref sd, "save");
		for(int i = 0; i < themeContent.transform.childCount; i++)
		{
			avail.Add(themeContent.transform.GetChild(i).GetComponent<ThemeController>());
			if(avail[i].cost == 0 && !sd.themes.Contains(i))
			{
				sd.addTheme(i);
			}
		}
		themeContent.GetComponent<ThemeManager>().setTheme(avail[sd.themeIndex]);
		ThemeHolder.theme = avail[sd.themeIndex];
		FileHandler.Save(sd, "save");

		panels = GameObject.FindGameObjectsWithTag("Panel");
		foreach(GameObject p in panels) p.SetActive(false);
		mainPanel.SetActive(true);
	}
}