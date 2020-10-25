using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharactersMover : MonoBehaviour
{
    NavMeshAgent agent;
    Camera mainCamera;
    void Start()
    {
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
                agent.destination = hit.point;
            }
        }
    }
}
