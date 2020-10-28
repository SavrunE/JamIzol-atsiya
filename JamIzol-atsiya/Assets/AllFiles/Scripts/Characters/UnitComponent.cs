using UnityEngine;
using System.Collections;

public class UnitComponent : MonoBehaviour
{
	
	[SerializeField] private UnitType _type; // выбрать тип юнита

	private int _id;
	private string _iconName;
	private bool _isSelected;

	public bool isSelected
	{
		get { return _isSelected; }
	}

	public string iconName
	{
		get { return _iconName; }
	}

	public int id
	{
		get { return _id; }
	}

	void Start()
	{
		_iconName = _type.ToString();
		_id = _iconName.GetHashCode();
		UnitSelect.AddUnit(this);
	}

	void OnDestroy()
	{
		// когда юнит уничтожен, сообщаем какой именно и если он выбран, то будет обновлена панель иконок
		UnitSelect.Internal.UnitDestroyed(_id, _isSelected);
	}

	public void Deselect() // вызов, если выделение юнита снято
	{
		_isSelected = false;
	}

	public void Select() // вызов, если юнит был выбран
	{
		_isSelected = true;
	}

	public void DoAction()
	{
		// дополнительная опция, вызывается специальной командой: UnitSelect.DoAction()
		// т.е. все юниты, которые в данный момент выбраны, выполнят данную функцию
		// можно использовать, например, для отправки юнитов в указанную точку
	}
}