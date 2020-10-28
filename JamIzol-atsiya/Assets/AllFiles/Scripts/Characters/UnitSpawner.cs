using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    public GameObject[] Units;
    public void AddUnit(Transform transform)
    {
        Instantiate(Units[Random.Range(0, Units.Length)], transform);
    }
}
