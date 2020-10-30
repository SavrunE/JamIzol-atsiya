using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateDefendCube : MonoBehaviour
{
    private UnitComponent unit;
    private Transform LookOnClick;
    private Vector3 rayPoint;

    public float DistanceToSpawn;
    public GameObject DefendedCube;
    public Action OnSpawnCube;
    public KeyCode KeyCodeSpace = KeyCode.Space;

    void Start()
    {
        unit = GetComponent<UnitComponent>();
        LookOnClick = GetComponentInChildren<Transform>().Find("LookOnClick");
        if (!LookOnClick)
        {
            Debug.Log("GenerateDefendCube need object LookOnClick on " + unit);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCodeSpace) && DefendedCube != null)
        {
            Ray ray = unit.mainCamera.ScreenPointToRay(Input.mousePosition);

            RaycastHit RaycastHit;
            if (Physics.Raycast(ray, out RaycastHit))
            {
                rayPoint = RaycastHit.point;
                Debug.Log(Vector3.Distance(transform.position, rayPoint));

                unit.Moving();
                StartCoroutine(PlaceDefendedCube());
                

            }
        }
    }
    private IEnumerator PlaceDefendedCube()
    {
        unit.OnRightClick += StopPlaceDefendedCube;
        while (Vector3.Distance(transform.position, rayPoint) > DistanceToSpawn)
        {
            Debug.Log("1");
            yield return null;
        }
        Debug.Log("2");
        unit.StopMoving();

        LookOnClick.LookAt(rayPoint);
        Instantiate(DefendedCube, rayPoint, LookOnClick.rotation);

        unit.OnRightClick -= StopPlaceDefendedCube;
    }
    private void StopPlaceDefendedCube()
    {
        StopCoroutine(PlaceDefendedCube());
    }
}

