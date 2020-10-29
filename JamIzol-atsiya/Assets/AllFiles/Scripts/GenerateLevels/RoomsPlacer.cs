using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomsPlacer : MonoBehaviour
{
    public static RoomsPlacer Instance;
    public Room[] RoomPrefab;
    public Room StartingRoom;

    private Room[,] spawnedRooms;

    public int LockationLength = 11;
    [SerializeField] private int spawnCount = 12;
    public delegate void Click();
    public Click OnUnitOverLevel;

    private int x = 0, y = 0;

    private int centerMap;

    private Vector2Int CurrentVector;
    private HashSet<Vector2Int> vacantPlaces;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance = this)
        {
            Debug.Log(gameObject + " was destroyed");
            Destroy(gameObject);
        }
    }
    private IEnumerator Start()
    {
        spawnedRooms = new Room[LockationLength, LockationLength];
        centerMap = LockationLength / 2;
        spawnedRooms[centerMap, centerMap] = StartingRoom;

        for (int i = 0; i < spawnCount; i++)
        {
            PlaceOneRoom();
            yield return new WaitForEndOfFrame();
        }
    }
    private void PlaceOneRoom()
    {
        HashSet<Vector2Int> vacantPlaces = new HashSet<Vector2Int>();
        for (int x = 0; x < spawnedRooms.GetLength(0); x++)
        {
            for (int y = 0; y < spawnedRooms.GetLength(1); y++)
            {
                if (spawnedRooms[x, y] == null) continue;

                int maxX = spawnedRooms.GetLength(0) - 1;
                int maxY = spawnedRooms.GetLength(1) - 1;


                if (x > 0 && spawnedRooms[x - 1, y] == null) vacantPlaces.Add(new Vector2Int(x - 1, y));
                if (y > 0 && spawnedRooms[x, y - 1] == null) vacantPlaces.Add(new Vector2Int(x, y - 1));
                if (x < maxX && spawnedRooms[x + 1, y] == null) vacantPlaces.Add(new Vector2Int(x + 1, y));
                if (y < maxY && spawnedRooms[x, y + 1] == null) vacantPlaces.Add(new Vector2Int(x, y + 1));
            }
        }

        Room newRoom = Instantiate(RoomPrefab[UnityEngine.Random.Range(0, RoomPrefab.Length)]);
        Vector2Int position = vacantPlaces.ElementAt(UnityEngine.Random.Range(0, vacantPlaces.Count));
        newRoom.transform.position = new Vector3(position.x - centerMap, 0, position.y - centerMap) * LockationLength;

        spawnedRooms[position.x, position.y] = newRoom;
    }
    private void Update()
    {
        OnUnitOverLevel?.Invoke();
        //if (unit.overLevel)
        //{
        //    unit.overLevel = false;
        //}
    }
}
