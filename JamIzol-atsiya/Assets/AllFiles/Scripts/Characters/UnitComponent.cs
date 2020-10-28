using UnityEngine;
using System.Collections;

public class UnitComponent : MonoBehaviour
{
	private int id;
	private string iconName;
	private bool isSelected;

	public bool IsSelected
	{
		get { return isSelected; }
	}

	public string IconName
	{
		get { return iconName; }
	}

	public int Id
	{
		get { return id; }
	}

	void Start()
	{
		UnitSelect.AddUnit(this);
	}

	void OnDestroy()
	{
	}

	public void Deselect() 
	{
		isSelected = false;
	}

	public void Select() 
	{
		isSelected = true;
	}

	public void DoAction()
	{
		// дополнительная опция, вызывается специальной командой: UnitSelect.DoAction()
		// т.е. все юниты, которые в данный момент выбраны, выполнят данную функцию
		// можно использовать, например, для отправки юнитов в указанную точку
	}
}