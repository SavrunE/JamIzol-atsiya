using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using UnityEngine.Events;

public class Mover : MonoBehaviour
{
    public bool CanMove;
    public bool isActive;

    public RaycastHit RaycastHit;

    public delegate void Click();
    public Click OnRightClick;

    public Vector3 Destination;
    void Start()
    {
        CanMove = true;
    }

    public void MoveDestination(NavMeshAgent agent, Camera mainCamera)
    {
        if (Input.GetMouseButtonUp(1))
        {
            CanMove = true;

            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit))
            {
                Vector3 point = RaycastHit.point;
                agent.SetDestination(point);

                OnRightClick?.Invoke();
            }
        }
    }
}
