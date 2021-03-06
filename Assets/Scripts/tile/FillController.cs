using UnityEngine;
using System.Collections;

public class FillController : MonoBehaviour
{
	bool noLeft;
	bool noRight;
	bool noTop;
	bool noBottom;
	public bool availLeft = false;
	public bool availRight = false;
	public bool availTop = false;
	public bool availBottom = false;
	TileController leftTile;
	TileController rightTile;
	TileController topTile;
	TileController bottomTile;
	TileController thisTile;
	int res;
	int index;

	bool filling = false;
	public int x;
	public int y;
	public float value;
	public bool path;

	void Start()
	{
	}

	void Update()
	{
		res = DifficultySelect.Difficulty;
		index = gameObject.transform.GetSiblingIndex();
		thisTile = gameObject.GetComponent<TileController>();
		x = index % res;
		y = index / res;

		noLeft = x == 0;
		noRight = x == res - 1;
		noTop = y == 0;
		noBottom = y == res - 1;

		if(gameObject.GetComponent<TileController>().FillAmount > 0)
		{
			SmartPopulate.tiles[x, y].path = true;
			this.path = true;
		}

		value = SmartPopulate.tiles[x, y].value;
		if(!noLeft)
		{
			leftTile = gameObject.transform.parent.GetChild(index - 1).GetComponent<TileController>();
			if(leftTile.FillAmount > 0)
			{
				flipOpposites(leftTile, TileController.Direction.RIGHT, TileController.Direction.LEFT);
				if(leftTile.End == TileController.Direction.RIGHT && thisTile.Begin == TileController.Direction.LEFT)
				{
					if(leftTile.FillAmount >= 1)
					{
						StartFill();
					}
				}
			}
/*			else if(leftTile.marked)
			{
				flipOpposites(leftTile, TileController.Direction.RIGHT, TileController.Direction.LEFT);
				if(leftTile.End == TileController.Direction.RIGHT && thisTile.Begin == TileController.Direction.LEFT)
				{
					thisTile.marked = true;
				}
			}*/
		}
		if(!noRight)
		{
			rightTile = gameObject.transform.parent.GetChild(index + 1).GetComponent<TileController>();
			if(rightTile.FillAmount > 0)
			{
				flipOpposites(rightTile, TileController.Direction.LEFT, TileController.Direction.RIGHT);
				if(rightTile.End == TileController.Direction.LEFT && thisTile.Begin == TileController.Direction.RIGHT)
				{
					if(rightTile.FillAmount >= 1)
					{
						StartFill();
					}
				}
			}
/*			else if(rightTile.marked)
			{
				flipOpposites(rightTile, TileController.Direction.LEFT, TileController.Direction.RIGHT);
				if(rightTile.End == TileController.Direction.LEFT && thisTile.Begin == TileController.Direction.RIGHT)
				{
					thisTile.marked = true;
				}
			}*/
		}
		if(!noTop)
		{
			topTile = gameObject.transform.parent.GetChild(index - res).GetComponent<TileController>();
			if(topTile.FillAmount > 0)
			{
				flipOpposites(topTile, TileController.Direction.DOWN, TileController.Direction.UP);
				if(topTile.End == TileController.Direction.DOWN && thisTile.Begin == TileController.Direction.UP)
				{
					if(topTile.FillAmount >= 1)
					{
						StartFill();
					}
				}
			}
/*			else if(topTile.marked)
			{
				flipOpposites(topTile, TileController.Direction.DOWN, TileController.Direction.UP);
				if(topTile.End == TileController.Direction.DOWN && thisTile.Begin == TileController.Direction.UP)
				{
					thisTile.marked = true;
				}
			}*/
		}
		if(!noBottom)
		{
			bottomTile = gameObject.transform.parent.GetChild(index + res).GetComponent<TileController>();
			if(bottomTile.FillAmount > 0)
			{
				flipOpposites(bottomTile, TileController.Direction.UP, TileController.Direction.DOWN);
				if(bottomTile.End == TileController.Direction.UP && thisTile.Begin == TileController.Direction.DOWN)
				{
					if(bottomTile.FillAmount >= 1)
					{
						StartFill();
					}
				}
			}
/*			else if(bottomTile.marked)
			{
				flipOpposites(bottomTile, TileController.Direction.UP, TileController.Direction.DOWN);
				if(bottomTile.End == TileController.Direction.UP && thisTile.Begin == TileController.Direction.DOWN)
				{
					thisTile.marked = true;
				}
			}*/
		}

		if(noTop && index == gameObject.GetComponentInParent<SmartPopulate>().getStart())
		{
			if(thisTile.End == TileController.Direction.UP)
			{
				thisTile.End = thisTile.Begin;
				thisTile.Begin = TileController.Direction.UP;
			}
		}

		thisTile.marked = thisTile.marked || (index == gameObject.transform.parent.GetComponent<SmartPopulate>().getStart());

		if(thisTile.End == TileController.Direction.LEFT)
		{
			if(!noLeft)
				availLeft = thisTile.End == TileController.Direction.LEFT && leftTile.Begin == TileController.Direction.RIGHT;
			else
				availLeft = false;
		} else
			availLeft = false;
		if(thisTile.End == TileController.Direction.RIGHT)
		{
			if(!noRight)
				availRight = thisTile.End == TileController.Direction.RIGHT && rightTile.Begin == TileController.Direction.LEFT;
			else
				availRight = false;
		} else
			availRight = false;
		if(thisTile.End == TileController.Direction.UP)
		{
			if(!noTop)
				availTop = thisTile.End == TileController.Direction.UP && topTile.Begin == TileController.Direction.DOWN;
			else
				availTop = false;
		} else
			availTop = false;
		if(thisTile.End == TileController.Direction.DOWN)
		{
			if(!noBottom)
				availBottom = thisTile.End == TileController.Direction.DOWN && bottomTile.Begin == TileController.Direction.UP;
			else
				availBottom = (x == gameObject.transform.parent.GetComponent<SmartPopulate>().getEnd() && (y == res - 1));
		} else
			availBottom = false;

		if(thisTile.marked && thisTile.End == TileController.Direction.DOWN && (x == gameObject.transform.parent.GetComponent<SmartPopulate>().getEnd() && (y == res - 1)))
		{
			gameObject.transform.parent.GetChild(gameObject.transform.parent.GetComponent<SmartPopulate>().getStart()).GetComponent<FillController>().StartFill();
		}

		if(filling)
		{
			if(thisTile.FillAmount + res * gameObject.GetComponentInParent<SmartPopulate>().FillRate * Time.deltaTime > 1)
			{
				filling = false;
				thisTile.FillAmount = 1;
			}
			else
				gameObject.GetComponent<TileController>().FillAmount += res * gameObject.GetComponentInParent<SmartPopulate>().FillRate * Time.deltaTime;
		}

		if(gameObject.GetComponent<TileController>().FillAmount >= 1.0f)
		{
			if((thisTile.End == TileController.Direction.LEFT  && !availLeft)  ||
			   (thisTile.End == TileController.Direction.RIGHT && !availRight) ||
			   (thisTile.End == TileController.Direction.UP    && !availTop)   ||
			   (thisTile.End == TileController.Direction.DOWN  && !availBottom)) gameObject.GetComponentInParent<SmartPopulate>().endGame();
		}
	}

	public void StartFill()
	{
		this.filling = true;
	}

	void flipOpposites(TileController check, TileController.Direction oppositeOfCheck, TileController.Direction checkDirection)
	{
		if(check.End == oppositeOfCheck && thisTile.End == checkDirection)
		{
			TileController.Direction temp = thisTile.Begin;
			thisTile.Begin = thisTile.End;
			thisTile.End = temp;
		}
	}

	bool opposites(TileController.Direction a, TileController.Direction b)
	{
		if(a == TileController.Direction.LEFT && b == TileController.Direction.RIGHT)
			return true;
		else if(a == TileController.Direction.RIGHT && b == TileController.Direction.LEFT)
			return true;
		else if(a == TileController.Direction.UP && b == TileController.Direction.DOWN)
			return true;
		else if(a == TileController.Direction.DOWN && b == TileController.Direction.UP)
			return true;
		else
			return false;
	}
}
