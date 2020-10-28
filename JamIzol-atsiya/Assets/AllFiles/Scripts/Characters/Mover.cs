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

    private Camera mainCamera;

    public delegate void Click();
    public Click OnRightClick;

    public Vector3 Destination;
    void Start()
    {
        CanMove = true;
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    public void MoveDestination(NavMeshAgent agent)
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
