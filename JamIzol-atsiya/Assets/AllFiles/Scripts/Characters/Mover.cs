using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using UnityEngine.Events;

public class Mover : MonoBehaviour
{
    public bool canMove;
    public RaycastHit raycastHit;

    private NavMeshAgent agent;
    private Camera mainCamera;

    public Action<Vector3> DestinationEvent;

    //public DestinationEvent DestinationEvent;

    public delegate void Click();
    public Click OnRightClick;
    void Start()
    {
        //OnRightClick =  () => { Debug.Log("Hi"); };
        //OnRightClick += delegate () { Debug.Log("q"); };
        //OnRightClick();

        canMove = true;

        agent = GetComponent<NavMeshAgent>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(1))
        {
            OnRightClick?.Invoke();
            canMove = true;

            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out raycastHit))
            {
                Vector3 point = raycastHit.point;
                agent.SetDestination(point);
                DestinationEvent?.Invoke(point);
            }
        }
    }
    private void MyTestDelegateFunction()
    {
        Debug.Log("Test");
    }
}
//[System.Serializable]
//public class DestinationEvent : UnityEvent<Vector3> { }
//public class DestinationEventInt : UnityEvent<Int> { }
