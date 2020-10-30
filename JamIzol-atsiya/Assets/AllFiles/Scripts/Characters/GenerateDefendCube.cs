using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateDefendCube : MonoBehaviour
{
    private UnitComponent unit;
    private UnitSelect unitSelect;
    private Transform LookOnClick;
    private Vector3 rayPoint;

    private float distanceToSpawn;
    private GameObject defendedCube;
    private KeyCode keyCodeSpace;

    private float waitToPlace = 0.5f;
    private float checkWaitToPlace;
    private bool canPlaceCube = true;

    public Action OnSpawnCube;

    void Start()
    {

        unit = GetComponent<UnitComponent>();
        unitSelect = GetComponent<UnitSelect>();
        LookOnClick = GetComponentInChildren<Transform>().Find("LookOnClick");
        if (!LookOnClick)
        {
            Debug.Log("GenerateDefendCube need object LookOnClick on " + unit);
        }
        distanceToSpawn = GameController.Instance.WallDistancePlace;
        defendedCube = GameController.Instance.WallPrefab;
        keyCodeSpace = GameController.Instance.KeyCodePlaceWall;
    }

    void Update()
    {
        if (Input.GetKeyDown(keyCodeSpace) && unit.IsSelected && canPlaceCube)
        {
            canPlaceCube = false;
            Ray ray = unit.mainCamera.ScreenPointToRay(Input.mousePosition);

            RaycastHit RaycastHit;
            if (Physics.Raycast(ray, out RaycastHit))
            {
                rayPoint = RaycastHit.point;
                //Debug.Log(Vector3.Distance(transform.position, rayPoint));


                unit.Moving();
                StartCoroutine(CheckTimeToNextCubePlace());
                StartCoroutine(PlaceDefendedCube());

            }
        }
    }
    private IEnumerator CheckTimeToNextCubePlace()
    {
        yield return new WaitForSeconds(waitToPlace);
        canPlaceCube = true;
    }
    private IEnumerator PlaceDefendedCube()
    {
        unit.OnRightClick += StopPlaceDefendedCube;
        while (Vector3.Distance(transform.position, rayPoint) > distanceToSpawn)
        {
            yield return null;
        }
        unit.StopMoving();

        LookOnClick.LookAt(rayPoint);
        Instantiate(defendedCube, rayPoint, LookOnClick.rotation);

        unit.OnRightClick -= StopPlaceDefendedCube;
    }
    private void StopPlaceDefendedCube()
    {
        StopCoroutine(PlaceDefendedCube());
    }
}

