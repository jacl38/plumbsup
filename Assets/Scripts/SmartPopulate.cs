using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class SmartPopulate : MonoBehaviour {

	public bool playing;
	[Range(3, 10)]
	public int Degree;
	[Range(0, 1)]
	public float FillRate;

	public int stuckPipes;
	public static Tile[,] tiles;
	Tile current;
	public List<TileController> path = new List<TileController>();

	public Transform Pipe;
	private GameObject scores;

	private GameObject startPoint;
	private GameObject endPoint;
	private GameObject water;
	GridLayoutGroup glg;

	public Color normalColor;
	public Color silverColor;
	public Color goldenColor;
	public Color waterColor;
	public GameObject themeContent;

	int start;
	int end;

	void Start () {
		water = gameObject.transform.parent.parent.parent.parent.GetChild(3).gameObject;
		scores = GameObject.Find("ScoresLabel");
		ScoresController.normalCount = 0;
		ScoresController.silverCount = 0;
		ScoresController.goldenCount = 0;
		normalColor = ThemeHolder.theme.standardcolor;
		silverColor = ThemeHolder.theme.mediumcolor;
		goldenColor = ThemeHolder.theme.bestcolor;
		waterColor = ThemeHolder.theme.watercolor;
		water.GetComponent<Image>().color = waterColor;
		GameObject.Find("StartPoint").GetComponent<Image>().color = waterColor;
		generate();
	}
	
	public int getStart() { return this.start; }
	public int getEnd() { return this.end; }

	void Update () {
		if(!playing) {
			setTiles(false);
		}
		string normalhex = ColorUtility.ToHtmlStringRGB(normalColor);
		string silverhex = ColorUtility.ToHtmlStringRGB(silverColor);
		string goldenhex = ColorUtility.ToHtmlStringRGB(goldenColor);
		scores.GetComponent<Text>().text = "<color=\"#" + normalhex + "\">" + ScoresController.normalCount + "</color>  " + 
										   "<color=\"#" + silverhex + "\">" + ScoresController.silverCount + "</color>  " +
										   "<color=\"#" + goldenhex + "\">" + ScoresController.goldenCount + "</color>\n";
		if(water.GetComponent<ScaleWayPoints>() != null)
		if(water.GetComponent<ScaleWayPoints>().ended())
		{
			endGame();
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
		if(water.GetComponent<ScaleWayPoints>() == null)
		{
			water.AddComponent<ScaleWayPoints>();
			water.GetComponent<ScaleWayPoints>().points = new Vector3[4] {
				new Vector3(1.0f, 0.0f, 0.0f),
				new Vector3(1.0f, 32f, 1.0f),
				new Vector3(1.0f, 30f, 0.75f),
				new Vector3(1.0f, 50f, 1.0f)
			};
		}
		else if(water.GetComponent<ScaleWayPoints>().ended())
		{
			Destroy(water.GetComponent<ScaleWayPoints>());
			gameObject.transform.parent.parent.parent.parent.GetChild(2).gameObject.SetActive(true);
			gameObject.transform.parent.parent.parent.gameObject.SetActive(false);
		}
	}

	public void dijkstraGenerate()
	{
		List<Tile> nodes = new List<Tile>();
		for(int x = 0; x < tiles.GetLength(0); x++)
		{
			for(int y = 0; y < tiles.GetLength(1); y++)
			{
				tiles[x, y] = new Tile(false, x, y);
			}
		}

		tiles[start, 0].cost = 0;
		tiles[start, 0].node = true;

		current = tiles[start, 0];
		try
		{
			while((current.x != end || current.y != tiles.GetLength(1) - 1))
			{
				current.visited = true;
				try
				{
					Tile leftTile = tiles[current.x-1, current.y];
					if(!leftTile.visited && !leftTile.stuck)
					{
						int rand = (int) Random.Range(1, 1000);
						if(leftTile.cost > current.cost + rand || leftTile.cost == -1)
						{
							leftTile.cost = current.cost + rand;
							leftTile.from = current;
							leftTile.fromDirection = TileController.Direction.RIGHT;
						}
					}
				} catch {}
				try
				{
					Tile rightTile = tiles[current.x+1, current.y];
					if(!rightTile.visited && !rightTile.stuck)
					{
						int rand = (int) Random.Range(1, 1000);
						if(rightTile.cost > current.cost + rand || rightTile.cost == -1)
						{
							rightTile.cost = current.cost + rand;
							rightTile.from = current;
							rightTile.fromDirection = TileController.Direction.LEFT;
						}
					}
				} catch {}
				try
				{
					Tile topTile = tiles[current.x, current.y-1];
					if(!topTile.visited && !topTile.stuck)
					{
						int rand = (int) Random.Range(1, 1000);
						if(topTile.cost > current.cost + rand || topTile.cost == -1)
						{
							topTile.cost = current.cost + rand;
							topTile.from = current;
							topTile.fromDirection = TileController.Direction.DOWN;
						}
					}
				} catch {}
				try
				{
					Tile bottomTile = tiles[current.x, current.y+1];
					if(!bottomTile.visited && !bottomTile.stuck)
					{
						int rand = (int) Random.Range(1, 1000);
						if(bottomTile.cost > current.cost + rand || bottomTile.cost == -1)
						{
							bottomTile.cost = current.cost + rand;
							bottomTile.from = current;
							bottomTile.fromDirection = TileController.Direction.UP;
						}
					}
				} catch {}
				int min = int.MaxValue;
				Tile currentMin = null;
				for(int x = 0; x < tiles.GetLength(1); x++)
				{
					for(int y = 0; y < tiles.GetLength(0); y++)
					{
						Tile t = tiles[x, y];
						if(!t.visited && t.cost < min && t.cost != -1)
						{
							currentMin = t;
							min = t.cost;
						}
					}
				}
				current = currentMin;
			}
		}
		catch { dijkstraGenerate();	}
		Tile pathCurrent = tiles[end, Degree-1];
		while(pathCurrent != tiles[start, 0])
		{
			pathCurrent.node = true;
			nodes.Add(pathCurrent);

			if(pathCurrent.from.x > pathCurrent.x) { pathCurrent.from.toDirection = TileController.Direction.LEFT;  }
			if(pathCurrent.from.x < pathCurrent.x) { pathCurrent.from.toDirection = TileController.Direction.RIGHT; }
			if(pathCurrent.from.y > pathCurrent.y) { pathCurrent.from.toDirection = TileController.Direction.UP;    }
			if(pathCurrent.from.y < pathCurrent.y) { pathCurrent.from.toDirection = TileController.Direction.DOWN;  }

			pathCurrent = pathCurrent.from;
		}
		nodes.Add(tiles[start, 0]);

		int stuck = stuckPipes;
		while(stuck > 0)
		{
			float value = Random.value;
			int index = Mathf.FloorToInt(Random.Range(0, nodes.Count));
			if(!nodes[index].stuck)
			{
				tiles[nodes[index].x, nodes[index].y].stuck = true;
				tiles[nodes[index].x, nodes[index].y].value = value;
				stuck--;
			}
		}

		shufflePipes();
			
		for(int i = 0; i < Degree * Degree; i++)
		{
			int x = i % Degree;
			int y = i / Degree;
			Transform t = Instantiate(Pipe, transform.position, transform.rotation) as Transform;
			GameObject p = t.gameObject;
			p.transform.SetParent(gameObject.transform);
			p.transform.localScale = Vector3.one;
			TileController tile = p.GetComponent<TileController>();
			tile.Degree = Degree;
			if(tiles[x, y].node)
			{
				if(tiles[x, y].value > 0.8)
				{
					tile.PipeColor = goldenColor;
				}
				else if(tiles[x, y].value > 0)
				{
					tile.PipeColor = silverColor;
				}
				else
				{
					tile.PipeColor = normalColor;
				}
				tile.Begin = tiles[x, y].fromDirection;
				tile.End = tiles[x, y].toDirection;
				if(x == end && y == Degree-1)
				{
					tile.End = TileController.Direction.DOWN;
				}
			}
			else
			{
				tile.PipeColor = normalColor;
				tile.Begin = (TileController.Direction) Mathf.FloorToInt(Random.Range(0, 4));
				tile.End   = (TileController.Direction) Mathf.FloorToInt(Random.Range(0, 4));
			}
		}

/*		for(int x = 0; x < tiles.GetLength(0); x++)
		{
			for(int y = 0; y < tiles.GetLength(1); y++)
			{
				Transform t = Instantiate(Pipe, transform.position, transform.rotation) as Transform;
				GameObject p = t.gameObject;
				p.transform.SetParent(gameObject.transform);
				p.transform.localScale = Vector3.one;
				TileController tile = p.GetComponent<TileController>();
				tile.Degree = Degree;
				if(tiles[y, x].node)
				{
					if(tiles[x, y].value > 0.8)
						tile.PipeColor = goldenColor;
					else if(tiles[x, y].value > 0)
						tile.PipeColor = silverColor;
					else
						tile.PipeColor = normalColor;
					tile.Begin = tiles[y, x].fromDirection;
					tile.End   = tiles[y, x].toDirection;
				}
				else
				{
					tile.PipeColor = normalColor;
					tile.Begin = (TileController.Direction) Mathf.FloorToInt(Random.Range(0, 4));
					tile.End   = (TileController.Direction) Mathf.FloorToInt(Random.Range(0, 4));
				}
			}
		}*/
	}

	public void generate()
	{
		playing = true;
		if(!(DifficultySelect.Difficulty >= DifficultySelect.MinDifficulty && DifficultySelect.Difficulty <= DifficultySelect.MaxDifficulty)) {
			DifficultySelect.Difficulty = 3;
		}
		Degree = DifficultySelect.Difficulty;
		stuckPipes = Degree / 2;
		start  = Mathf.FloorToInt(Random.Range(0, Degree));
		end    = Mathf.FloorToInt(Random.Range(0, Degree));

		startPoint = gameObject.transform.parent.GetChild(1).gameObject;
		endPoint   = gameObject.transform.parent.GetChild(2).gameObject;

		startPoint .transform.localPosition = new Vector3(-480 + (960 / Degree) / 2 + (start * (960/Degree)), 490, 0);
		endPoint   .transform.localPosition = new Vector3(-480 + (960 / Degree) / 2 + (end * (960/Degree)),  -490, 0);

		startPoint .GetComponent<RectTransform>().sizeDelta = new Vector3(960 / Degree, 20, 0);
		endPoint   .GetComponent<RectTransform>().sizeDelta = new Vector3(960 / Degree, 20, 0);

		glg = gameObject.GetComponent<GridLayoutGroup>();

		glg.cellSize = new Vector2(960 / Degree, 960 / Degree);

		if(!(DifficultySelect.Difficulty >= DifficultySelect.MinDifficulty && DifficultySelect.Difficulty <= DifficultySelect.MaxDifficulty)) {
			DifficultySelect.Difficulty = 3;
		}

		for(int i = 0; i < gameObject.transform.childCount; i++)
		{
			Destroy(gameObject.transform.GetChild(i).gameObject);
		}

		tiles = new Tile[Degree, Degree];
		dijkstraGenerate();
	}

	public void shufflePipes()
	{
		for(int x = 0; x < Degree; x++)
		{
			for(int y = 0; y < Degree; y++)
			{
				if(tiles[x, y].node && !tiles[x, y].stuck)
				{
					int x2 = Mathf.FloorToInt(Random.Range(0, Degree));
					int y2 = Mathf.FloorToInt(Random.Range(0, Degree));
					while(tiles[x, y].stuck || tiles[x2, y2].stuck)
					{
						x2 = Mathf.FloorToInt(Random.Range(0, Degree));
						y2 = Mathf.FloorToInt(Random.Range(0, Degree));
					}
					Tile flip = tiles[x, y];
					tiles[x, y] = tiles[x2, y2];
					tiles[x2, y2] = flip;
				}
			}
		}
	}
}
