using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class UnitComponent : MonoBehaviour
{
    [Range(1f, 100f)]
    public float AttackDamage = 10;
    public float MoveSpeed
    {
        get
        {
            if (agent)
            {
                return agent.speed;
            }
            return 0f;
        }
        set
        {
            agent.speed = value;
        }
    }

    public GameObject SelectDisplay;
    public Camera mainCamera;

    private bool isSelected;
    private bool isBusy;
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
        agent.speed += 1;
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