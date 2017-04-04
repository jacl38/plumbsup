using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tile {

	public Tile from;
	public TileController.Direction fromDirection;
	public TileController.Direction toDirection;
	public bool visited = false;
	public bool node = false;
	public bool path = false;
	public bool stuck;
	public float value;
	public int cost = -1;
	public int x, y;

	public Tile(bool stuck, int x, int y)
	{
		this.value = -1;
		this.stuck = stuck;
		this.x = x;
		this.y = y;
	}
}