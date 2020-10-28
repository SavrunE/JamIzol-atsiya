using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class UnitComponent : MonoBehaviour
{
	private bool isSelected;
	private Mover mover;
	private NavMeshAgent agent;

	public bool IsSelected
	{
		get { return isSelected; }
	}

	void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		mover = GetComponent<Mover>();
		UnitSelect.AddUnit(this);
	}
	private void Update()
	{
		if (isSelected)
		{
			DoAction();
		}
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
		mover.MoveDestination(agent);
	}
}