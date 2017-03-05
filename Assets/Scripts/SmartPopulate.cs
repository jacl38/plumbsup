using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SmartPopulate : MonoBehaviour {

	public bool playing;
	[Range(3, 10)]
	public int Degree;
	public Transform Pipe;

	private GameObject startPoint;
	private GameObject endPoint;
	GridLayoutGroup glg;

	int start;
	int end;

	void Start () {
		playing = true;

		if(!(DifficultySelect.Difficulty >= DifficultySelect.MinDifficulty && DifficultySelect.Difficulty <= DifficultySelect.MaxDifficulty)) {
			DifficultySelect.Difficulty = 3;
		}
		Degree = DifficultySelect.Difficulty;
		start  = (int) Mathf.Floor(Random.Range(0, Degree));
		end    = (int) Mathf.Floor(Random.Range(0, Degree));

		startPoint = gameObject.transform.parent.GetChild(1).gameObject;
		endPoint   = gameObject.transform.parent.GetChild(2).gameObject;

		startPoint .transform.localPosition = new Vector3(-480 + (960 / Degree) / 2 + (start * (960/Degree)), 490, 0);
		endPoint   .transform.localPosition = new Vector3(-480 + (960 / Degree) / 2 + (end * (960/Degree)), -490, 0);

		startPoint .GetComponent<RectTransform>().sizeDelta = new Vector3(960 / Degree, 20, 0);
		endPoint   .GetComponent<RectTransform>().sizeDelta = new Vector3(960 / Degree, 20, 0);

		glg = gameObject.GetComponent<GridLayoutGroup>();

		glg.cellSize = new Vector2(960 / Degree, 960 / Degree);

		for(int i = 0; i < gameObject.transform.childCount; i++)
		{
			Destroy(gameObject.transform.GetChild(i).gameObject);
			Debug.Log("Killed all children");
		}

		for (int i = 0; i < Degree; i++)
		{
			for (int j = 0; j < Degree; j++)
			{
				Transform t = Instantiate(Pipe, transform.position, transform.rotation) as Transform;
				GameObject p = t.gameObject;
				p.transform.SetParent(gameObject.transform);
				p.transform.localScale = Vector3.one;
				TileController tile = p.GetComponent<TileController>();
				tile.PipeColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
				tile.Degree = Degree;
				int rand = (int) Mathf.Floor(Random.Range(0, 4));
				tile.Begin = (TileController.Direction) rand;
				rand = (int) Mathf.Floor(Random.Range(0, 4));
				tile.End = (TileController.Direction) rand;
			}
		}
	}
	
	public int getStart() { return this.start; }
	public int getEnd() { return this.end; }

	void Update () {
		if(!playing) {
			setTiles(false);
		}
	}

	public void setTiles(bool enabled) {
		for(int i = 0; i < gameObject.transform.childCount; i++)
		{
			gameObject.transform.GetChild(i).GetComponent<Button>().enabled = enabled;
		}
	}
}
