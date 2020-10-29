using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class UnitComponent : MonoBehaviour
{
	public GameObject SelectDisplay;
	private bool isSelected;
	private bool isBusy;
	private Camera mainCamera;
	public bool CheckBusy {  get {return isBusy; } set { } }
	private Mover mover;
	private NavMeshAgent agent;

	public bool IsSelected
	{
		get { return isSelected; }
	}

	void Start()
	{
		mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
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
		if (SelectDisplay)
		{
			SelectDisplay.SetActive(false);
		}
	}

	public void Select() 
	{
		isSelected = true;
		if (SelectDisplay)
		{
			SelectDisplay.SetActive(true);
		}
	}
	public void IsBusy()
	{
		isBusy = true;
	}
	public void NotBusy()
	{
		isBusy = false;
	}

	public void DoAction()
	{
		mover.MoveDestination(agent, mainCamera);
	}
}