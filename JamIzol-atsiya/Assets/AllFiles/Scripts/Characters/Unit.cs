using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


//хуйня 
[RequireComponent(typeof(NavMeshAgent))]
public class Unit : MonoBehaviour
{
    private Mover mover = null;

    private NavMeshAgent agent;
    public NavMeshAgent Agent
    {
        get
        {
            if (agent == null)
            {
                agent = GetComponent<NavMeshAgent>();
            }
            return agent;
        }
        set
        {
            agent = value;
        }
    }

    private Vector3 targetPosition;
    void Start()
    {
        UnitManager.Instance.AddUnit(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (mover)
        {
           // Agent.SetDestination(mover.DestinationEvent());
        }
    }
}
