using UnityEngine;
using System.Collections;

public class UnitComponent : MonoBehaviour
{
	
	[SerializeField] private UnitType type; // выбрать тип юнита

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
		iconName = type.ToString();
		id = iconName.GetHashCode();
		UnitSelect.AddUnit(this);
	}

	void OnDestroy()
	{
		// когда юнит уничтожен, сообщаем какой именно и если он выбран, то будет обновлена панель иконок
		UnitSelect.Internal.UnitDestroyed(id, isSelected);
	}

	public void Deselect() // вызов, если выделение юнита снято
	{
		isSelected = false;
	}

	public void Select() // вызов, если юнит был выбран
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