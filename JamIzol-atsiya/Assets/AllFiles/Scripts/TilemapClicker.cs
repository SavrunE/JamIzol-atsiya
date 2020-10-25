using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapClicker : MonoBehaviour
{
    public Transform PointerFlag;

    public TileBase TileToSet;
    private Tilemap map;
    private Camera mainCamera;
    void Start()
    {
        map = GetComponent<Tilemap>();
        mainCamera = Camera.main;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 clickWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            Vector3Int clickCellPosition = map.WorldToCell(clickWorldPosition);


            PointerFlag.position = map.CellToWorld(clickCellPosition);
        }
    }
}
