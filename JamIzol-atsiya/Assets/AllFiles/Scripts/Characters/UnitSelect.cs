using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class UnitSelect : MonoBehaviour
{

	[SerializeField] private int maxUnits = 100; // сколько всего может быть юнитов, под контролем игрока
	[SerializeField] private Image mainRect; // чем будем рисовать рамку


	private Rect rect;
	private bool canDraw;
	private Vector2 startPos, endPos;
	private Color original, clear, curColor;
	private Sprite[] unitImage;
	private static UnitComponent[] unit;
	private static List<UnitComponent> unitSelected;
	private static int unitCount;


	public static void DoAction() // запрос на выполнение какого-либо действия, если есть выбранные юниты
	{
		foreach (UnitComponent target in unitSelected)
		{
			if (target) target.DoAction();
		}
	}

	public static void AddUnit(UnitComponent comp) // добавить нового юнита
	{
		for (int i = 0; i < unit.Length; i++)
		{
			if (unit[i] == null)
			{
				unit[i] = comp;
				unitCount++;
				break;
			}
		}
	}

	public static int currentUnitCount // текущее количество юнитов
	{
		get { return unitCount; }
	}

	void Awake()
	{
		unitCount = 0;
		unit = new UnitComponent[maxUnits];
		unitSelected = new List<UnitComponent>();
		original = mainRect.color;
		clear = original;
		clear.a = 0;
		curColor = clear;
		mainRect.color = clear;
	}

	void Draw() // рисуем рамку
	{
		endPos = Input.mousePosition;
		if (startPos == endPos || !canDraw) return;

		curColor = original;

		rect = new Rect(Mathf.Min(endPos.x, startPos.x),
			Screen.height - Mathf.Max(endPos.y, startPos.y),
			Mathf.Max(endPos.x, startPos.x) - Mathf.Min(endPos.x, startPos.x),
			Mathf.Max(endPos.y, startPos.y) - Mathf.Min(endPos.y, startPos.y)
		);

		mainRect.rectTransform.sizeDelta = new Vector2(rect.width, rect.height);

		mainRect.rectTransform.anchoredPosition = new Vector2(rect.x + mainRect.rectTransform.sizeDelta.x / 2,
			Mathf.Max(endPos.y, startPos.y) - mainRect.rectTransform.sizeDelta.y / 2);
	}

	void Deselect() // отмена текущего выбора
	{
		foreach (UnitComponent target in unitSelected)
		{
			Debug.Log("check");
			if (target) target.Deselect();
		}
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0) && !HitUnit())
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
		}

		Draw();

		mainRect.color = Color.Lerp(mainRect.color, curColor, 10 * Time.deltaTime);
	}

	bool HitUnit() // клик по юниту или нет
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