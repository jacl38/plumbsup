using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanel : MonoBehaviour
{
	ScrollRect scroll;
	GameObject BuyButton;
	private float vel = 0.0F;
	public int index = 0;

	List<int> bought = new List<int>();
	List<ThemeController> avail = new List<ThemeController>();
	public SaveData sd = new SaveData();

	void Start ()
	{
		scroll = transform.parent.parent.gameObject.GetComponent<ScrollRect>();
		BuyButton = GameObject.Find("Buy Button").gameObject;

		FileHandler.Load(ref sd, "save");
		if(sd.getThemes() != null) bought = sd.getThemes();
		for(int i = 0; i < transform.childCount; i++)
		{
			if(sd.themes.Contains(i))
			{
				sd.addTheme(i);
				transform.GetChild(i).GetChild(4).GetComponent<Text>().text = "Bought";
			}
			avail.Add(transform.GetChild(i).GetComponent<ThemeController>());
		}
		GameObject.Find("CoinCount").GetComponent<Text>().text = sd.getCoins() + "";
		setCoinAfterText(GameObject.Find("CoinCount").gameObject, GameObject.Find("Coin").gameObject);
	}

	void Update ()
	{
		index = -Mathf.RoundToInt(transform.localPosition.x / 960);
		float destination = index * -960;
		if(Mathf.Abs(scroll.velocity.x) < 200 && /*Input.touches.Length == 0*/!Input.GetMouseButton(0))
		{
			float newx = Mathf.SmoothDamp(transform.localPosition.x, destination, ref vel, 0.1F, Mathf.Infinity, Time.deltaTime);
			scroll.horizontalNormalizedPosition = newx / (-(avail.Count-1) * 960);
		}
		if(bought.Contains(index))
		{
			BuyButton.transform.GetChild(0).GetComponent<Text>().text = "Select";
		}
		else
		{
			BuyButton.transform.GetChild(0).GetComponent<Text>().text = "Buy";
		}
	}

	public void Buy()
	{
		//Already owns the theme
		if(bought.Contains(index))
		{
			ThemeHolder.theme = avail[index];
			//Set the old theme to not selected, but "bought"
			transform.GetChild(sd.themeIndex).GetChild(4).GetComponent<Text>().text = "Bought";
			//set the theme
			GameObject.Find("ThemeContent").GetComponent<ThemeManager>().setTheme(avail[index]);
			//set theme manager's internal index to the current shop index
			GameObject.Find("ThemeContent").GetComponent<ThemeManager>().themeIndex = index;
			//Set the new theme text to selected
			transform.GetChild(index).GetChild(4).GetComponent<Text>().text = "Selected";
			//set the save file internal index
			sd.themeIndex = index;
			//saves it
			FileHandler.Save(sd, "save");
		}
		//Otherwise, if the user has enough for the theme
		else if(sd.getCoins() >= avail[index].cost)
		{
			//if the save doesn't have any themes, initialize the themes list so it doesn't break anything...
			if(sd.getThemes() == null)
			{
				sd.themes = new List<int>();
			}
			//add the theme to the save
			sd.addTheme(index);
			//add the theme to the bought list
			bought.Add(index);
			//take away the coins
			sd.addCoins(-avail[index].cost);
			//save the data
			FileHandler.Save(sd, "save");
			//update the coin counter
			GameObject.Find("CoinCount").GetComponent<Text>().text = sd.getCoins() + "";
			setCoinAfterText(GameObject.Find("CoinCount").gameObject, GameObject.Find("Coin").gameObject);
			//idk if this is needed w/e
			avail[index].gameObject.transform.GetChild(4).GetComponent<Text>().text = "Bought";
		}
		//if the user doesn't have the theme and they have less than the amount of coins needed to buy it
		else
		{
			//wiggle
			GameObject coinsPanel = GameObject.Find("CoinsPanel");
			if(coinsPanel.GetComponent<RotateWayPoints>() != null)
			{
				coinsPanel.GetComponent<RotateWayPoints>().Reset();
			}
			else
			{
				coinsPanel.AddComponent<RotateWayPoints>();
				coinsPanel.GetComponent<RotateWayPoints>().points = new Vector2[] {
					new Vector2(-10f, 0.1f),
					new Vector2(10f, 0.1f),
					new Vector2(0f, 0.1f)
				};
			}
		}
	}

	public static void setCoinAfterText(GameObject text, GameObject CoinImage)
	{
		CoinImage.transform.localPosition = new Vector2(text.GetComponent<Text>().text.Length*10 + CoinImage.GetComponent<RectTransform>().sizeDelta.x/2, 0);
		text.transform.localPosition = new Vector2(-CoinImage.GetComponent<RectTransform>().sizeDelta.x/2, 0);
	}
}
