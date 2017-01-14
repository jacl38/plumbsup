using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RandomPopulate : MonoBehaviour {

	public GameObject panel;
	public int degree;
	public Transform Pipe;

	private GameObject startPoint;
	private GameObject endPoint;

	GridLayoutGroup glg;

	void Start () {
		startPoint = gameObject.transform.parent.GetChild(1).gameObject;
		endPoint = gameObject.transform.parent.GetChild(2).gameObject;
		startPoint.transform.localPosition = new Vector3(-480 + (960 / degree) / 2, 490, 0);
		endPoint.transform.localPosition = new Vector3(480 - (960 / degree) / 2, -490, 0);
		startPoint.GetComponent<RectTransform>().sizeDelta = new Vector3(960 / degree, 20, 0);
		endPoint.GetComponent<RectTransform>().sizeDelta = new Vector3(960 / degree, 20, 0);

		glg = gameObject.GetComponent<GridLayoutGroup>();

		glg.cellSize = new Vector2(960 / degree, 960 / degree);

		for(int i = 0; i < degree; i++)
		{
			for(int j = 0; j < degree; j++)
			{
				Transform t = Instantiate(Pipe, transform.position, transform.rotation) as Transform;
				GameObject p = t.gameObject;
				p.transform.SetParent(panel.transform);
				p.transform.localScale = Vector3.one;
				TileController tile = p.GetComponent<TileController>();
				tile.Degree = degree;
				int rand = (int) Mathf.Floor(Random.Range(0, 4));
				tile.Begin = (TileController.Direction) rand;
				rand = (int) Mathf.Floor(Random.Range(0, 4));
				tile.End = (TileController.Direction) rand;
			}
		}
	}
	
	void Update () {
		
	}
}
