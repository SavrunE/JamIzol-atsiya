using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    NavMeshAgent agent;
    Camera mainCamera;
    public bool canMove;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        mainCamera = Camera.main;
        canMove = true;
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(1))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                agent.destination = hit.point;
            }
        }
    }
}
