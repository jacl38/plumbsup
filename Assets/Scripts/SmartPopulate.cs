using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SmartPopulate : MonoBehaviour {

	public bool playing;
	[Range(3, 10)]
	public int Degree;
	[Range(0, 1)]
	public float FillRate;

	private enum Types
	{
		rightDown,
		leftDown,
		rightUp,
		leftUp,
		vertical,
		horizontal
	};

	public Transform Pipe;

	private GameObject startPoint;
	private GameObject endPoint;
	GridLayoutGroup glg;

	int start;
	int end;

	void Start () {
		generateTiles();
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

	public void disableAll()
	{
		foreach(Transform tile in gameObject.transform)
		{
			tile.GetComponent<Swappable>().allowed = false;
			DestroyImmediate(tile.GetComponent<ScaleWayPoints>());
		}
	}

	public void endGame()
	{
		gameObject.transform.parent.parent.parent.transform.gameObject.SetActive(false);
		gameObject.transform.parent.parent.parent.parent.GetChild(2).gameObject.SetActive(true);
	}

	public void generateTiles()
	{
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
		}

		int rightDownsNeeded  = 0;
		int leftDownsNeeded   = 0;
		int rightUpsNeeded    = 0;
		int leftUpsNeeded     = 0;
		int verticalsNeeded   = 0;
		int horizontalsNeeded = 0;

		if(start < end)
		{
			horizontalsNeeded = end - start - 1;
			rightUpsNeeded = 1;
			leftDownsNeeded = 1;
		}
		if(start > end)
		{
			horizontalsNeeded = start - end - 1;
			leftUpsNeeded = 1;
			rightDownsNeeded = 1;
		}

		for(int i = 0; i < Degree * Degree; i++)
		{
			Transform t = Instantiate(Pipe, transform.position, transform.rotation) as Transform;
			GameObject p = t.gameObject;
			p.transform.SetParent(gameObject.transform);
			p.transform.localScale = Vector3.one;
			TileController tile = p.GetComponent<TileController>();
			tile.Degree = Degree;
			ArrayList needed = new ArrayList();
			for(int rd = 0; rd < rightDownsNeeded; rd++)
			{
				needed.Add(Types.rightDown);
			}
			for(int ld = 0; ld < leftDownsNeeded; ld++)
			{
				needed.Add(Types.leftDown);
			}
			for(int ru = 0; ru < rightUpsNeeded; ru++)
			{
				needed.Add(Types.rightUp);
			}
			for(int lu = 0; lu < leftUpsNeeded; lu++)
			{
				needed.Add(Types.leftUp);
			}
			for(int v = 0; v < verticalsNeeded; v++)
			{
				needed.Add(Types.vertical);
			}
			for(int h = 0; h < horizontalsNeeded; h++)
			{
				needed.Add(Types.horizontal);
			}
		}

/*		for (int i = 0; i < Degree; i++)
		{
			for (int j = 0; j < Degree; j++)
			{
				Transform t = Instantiate(Pipe, transform.position, transform.rotation) as Transform;
				GameObject p = t.gameObject;
				p.transform.SetParent(gameObject.transform);
				p.transform.localScale = Vector3.one;
				TileController tile = p.GetComponent<TileController>();
				tile.Degree = Degree;
				int rand = (int) Mathf.Floor(Random.Range(0, 4));
				tile.Begin = (TileController.Direction) rand;
				rand = (int) Mathf.Floor(Random.Range(0, 4));
				tile.End = (TileController.Direction) rand;
			}
		}*/
	}
}
