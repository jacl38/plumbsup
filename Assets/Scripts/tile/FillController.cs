using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class FillController : MonoBehaviour {

	public GameObject DU;
	public GameObject ML;
	public GameObject MR;

	[Range(0.0f, 1.0f)]
	public float amount;

	public enum PipeType {
		STRAIGHT,
		CORNER
	}

	private PipeType type;
	private float fullSize;

	void Start () {
		DU = gameObject.transform.GetChild(0).gameObject;
		ML = gameObject.transform.GetChild(1).gameObject;
		MR = gameObject.transform.GetChild(2).gameObject;
	}
	
	void Update ()
	{
		gameObject.GetComponent<RectTransform>().sizeDelta.Set(fullSize, fullSize);
		if (type == PipeType.STRAIGHT)
			straightFill();
		else if (type == PipeType.CORNER)
			rightCornerFill();
	}

	void straightFill()
	{
		ML.GetComponent<RectTransform>().sizeDelta = new Vector3(0, 0);
		ML.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);

		MR.GetComponent<RectTransform>().sizeDelta = new Vector3(0, 0);
		MR.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);

		DU.GetComponent<RectTransform>().sizeDelta = new Vector3(fullSize / 8, amount * fullSize);
		DU.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, (fullSize * amount) / 2 - (fullSize / 2), 1);
	}

	void rightCornerFill()
	{
		ML.GetComponent<RectTransform>().sizeDelta = new Vector3(0, 0);
		ML.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
		if (amount <= 0.5625)
		{
			MR.GetComponent<RectTransform>().sizeDelta = new Vector3(0, 0);
			MR.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);

			DU.GetComponent<RectTransform>().sizeDelta = new Vector3(fullSize / 8, amount * fullSize);
			DU.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, (fullSize * amount) / 2 - (fullSize / 2), 1);
		}
		else
		{
			DU.GetComponent<RectTransform>().sizeDelta = new Vector3(fullSize / 8, (9F/16F) * fullSize);
			DU.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, (fullSize * (9F/16F)) / 2 - (fullSize / 2), 1);

			MR.GetComponent<RectTransform>().sizeDelta = new Vector3((amount-.5625F) * fullSize, fullSize/8);
			MR.GetComponent<RectTransform>().anchoredPosition = new Vector3(fullSize * ((8F*amount)/16F) - (7F*fullSize)/32F, 0, 0);
		}
	}

	public void setType(PipeType type)
	{
		this.type = type;
	}

	public void setSize(float size)
	{
		this.fullSize = size;
	}
}
