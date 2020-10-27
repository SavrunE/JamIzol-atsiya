using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class Mover : MonoBehaviour
{
    public bool canMove;
    public RaycastHit raycastHit;

    private NavMeshAgent agent;
    private Camera mainCamera;
    void Start()
    {
        canMove = true;

        agent = GetComponent<NavMeshAgent>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(1))
        {
            canMove = true;

            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out raycastHit))
            {
                agent.SetDestination(raycastHit.point);
                
            }
        }
    }
}
