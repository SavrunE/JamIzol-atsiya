using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    public static GameObject[] Units;
    public static void AddUnit(Transform transform)
    {
        Instantiate(Units[Random.Range(0, Units.Length)], transform);
    }
}
