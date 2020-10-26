using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    public bool canMove;

    private NavMeshAgent agent;
    private Camera mainCamera;
    void Start()
    {
        canMove = true;

        agent = GetComponent<NavMeshAgent>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(1))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            }
        }
    }
}
