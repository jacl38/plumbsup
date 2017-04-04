using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoresController : MonoBehaviour {

	public static int normalCount, silverCount, goldenCount;
	public int normals, silvers, goldens;

	void Start () {
		
	}

	void Update () {
		normals = normalCount;
		silvers = silverCount;
		goldens = goldenCount;
	}
}
