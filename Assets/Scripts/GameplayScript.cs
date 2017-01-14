using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[ExecuteInEditMode]
public class GameplayScript : MonoBehaviour
{
	public Vector2 size;
	public bool fix;
	private Vector2 parentDimensions;

	GridLayoutGroup glg;

	void Start()
	{
		parentDimensions = gameObject.GetComponentInParent<RectTransform>().sizeDelta;
		size.x = Mathf.Round(size.x);
		size.y = Mathf.Round(size.y);
		if (size.x < 1) size.x = 1;
		if(size.y < 1) size.y = 1;
		glg = gameObject.GetComponent<GridLayoutGroup>();
	}

	void Update()
	{
		if (fix) fixCellSize();
	}

	public void fixCellSize()
	{
		if (!isFixed())
		{
			glg.cellSize = new Vector2(parentDimensions.x / size.x, parentDimensions.y / size.y);
		//	foreach (Transform child in gameObject.transform)
		//	{
		//		GameObject.DestroyImmediate(child.gameObject);
		//	}
		}
	}

	public bool isFixed()
	{
		return (glg.cellSize == new Vector2(parentDimensions.x / size.x, parentDimensions.y / size.y)) && gameObject.transform.childCount == size.x * size.y;
	}
}