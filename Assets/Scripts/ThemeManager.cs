using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class ThemeHolder
{
	public static ThemeController theme;
}

public class ThemeManager : MonoBehaviour
{
	public int themeIndex;
	public Color standardcolor;
	public Color mediumcolor;
	public Color bestcolor;
	public Color watercolor;
	public Gradient backgroundGradient;
	public Sprite straightPipe;
	public Sprite cornerPipe;

	public void setTheme(ThemeController newTheme)
	{
		standardcolor      = newTheme.standardcolor;
		mediumcolor        = newTheme.mediumcolor;
		bestcolor          = newTheme.bestcolor;
		watercolor         = newTheme.watercolor;
		backgroundGradient = newTheme.bg;
		GameObject.Find("Main Camera").GetComponent<CameraController>().setGradient(newTheme.bg);
		straightPipe       = newTheme.straightPipe;
		cornerPipe         = newTheme.cornerPipe;
	}
}
