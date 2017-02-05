using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdController : MonoBehaviour {

	public bool viewedAd = false;

	void Start () {
	}

	void Update () {
	}

	public void ShowAd()
	{
		if(!viewedAd) gameObject.GetComponent<Ads>().ShowAd();
	}

	public void SetViewed() { this.viewedAd = true; }
}
