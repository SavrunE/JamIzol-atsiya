using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class UnitSelect : MonoBehaviour
{
	[SerializeField] private int maxUnits = 100;
	[SerializeField] private Image mainRectImage;

	private Rect rect;
	private bool canDraw;
	private Vector2 startPos, endPos;
	private Color original, clear, curColor;
	private Sprite[] unitImage;
	private static UnitComponent[] unit;
	private static List<UnitComponent> unitSelected;
	private static int unitCount;

	public static void DoAction()
	{
		foreach (UnitComponent target in unitSelected)
		{
			if (target) target.DoAction();
		}
	}

	public static void AddUnit(UnitComponent unitComponent)
	{
		for (int i = 0; i < unit.Length; i++)
		{
			if (unit[i] == null)
			{
				unit[i] = unitComponent;
				unitCount++;
				break;
			}
		}
	}

	public static int currentUnitCount
	{
		get { return unitCount; }
	}

	void Awake()
	{
		unitCount = 0;
		unit = new UnitComponent[maxUnits];
		unitSelected = new List<UnitComponent>();
		original = mainRectImage.color;
		clear = original;
		clear.a = 0;
		curColor = clear;
		mainRectImage.color = clear;
	}

	void Draw()
	{
		endPos = Input.mousePosition;
		if (startPos == endPos || !canDraw) return;

		curColor = original;

		rect = new Rect(Mathf.Min(endPos.x, startPos.x),
			Screen.height - Mathf.Max(endPos.y, startPos.y),
			Mathf.Max(endPos.x, startPos.x) - Mathf.Min(endPos.x, startPos.x),
			Mathf.Max(endPos.y, startPos.y) - Mathf.Min(endPos.y, startPos.y)
		);

		mainRectImage.rectTransform.sizeDelta = new Vector2(rect.width, rect.height);

		mainRectImage.rectTransform.anchoredPosition = new Vector2(rect.x + mainRectImage.rectTransform.sizeDelta.x / 2,
			Mathf.Max(endPos.y, startPos.y) - mainRectImage.rectTransform.sizeDelta.y / 2);
	}

	void Deselect()
	{
		foreach (UnitComponent target in unitSelected)
		{
			Debug.Log("check");
			if (target) target.Deselect();
		}
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0) && !ClickOnUnit())
		{
			Deselect();
			rect = new Rect();
			unitSelected = new List<UnitComponent>();
			startPos = Input.mousePosition;
			canDraw = true;
		}

		if (Input.GetMouseButtonUp(0) && canDraw)
		{
			curColor = clear;
			canDraw = false;
			SetSelected();
		}

		Draw();

		mainRectImage.color = Color.Lerp(mainRectImage.color, curColor, 10 * Time.deltaTime);
	}

	void SetSelected() 
	{
		foreach (UnitComponent target in unit)
		{
			if (target)
			{
				Vector2 pos = Camera.main.WorldToScreenPoint(target.transform.position);
				pos = new Vector2(pos.x, Screen.height - pos.y);

				if (rect.Contains(pos))
				{
					target.Select();
					unitSelected.Add(target);
				}
			}
		}
	}

	bool ClickOnUnit() 
	{
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out hit))
		{
			UnitComponent unit = hit.collider.GetComponent<UnitComponent>();
			if (unit)
			{
				Deselect();
				rect = new Rect();
				unitSelected = new List<UnitComponent>();
				unit.Select();
				unitSelected.Add(unit);
				return true;
			}
		}
		return false;
	}
}