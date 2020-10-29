using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class UnitComponent : MonoBehaviour
{
    public GameObject SelectDisplay;
    private bool isSelected;
    private bool isBusy;
    private Camera mainCamera;
    private Mover mover;
    private NavMeshAgent agent;
    [SerializeField] private float radius = 15f;

public bool CheckBusy { get { return isBusy; } set { } }

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
        FogOfWar.Instance.Disperse(transform.position, radius);

        if (isSelected)
        {
            mover.MoveDestination(agent, mainCamera);
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

    }
}