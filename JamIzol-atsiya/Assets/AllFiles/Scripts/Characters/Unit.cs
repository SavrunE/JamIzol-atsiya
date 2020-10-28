using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Unit : MonoBehaviour
{
    public Mover mover;
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

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
