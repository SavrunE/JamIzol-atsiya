using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    private List<Unit> Units;

    public static UnitManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance == this)
        {
            Destroy(gameObject);
        }
        Units = new List<Unit>();
    }
    public void AddUnit(Unit unit)
    {
        Units.Add(unit);
    }
    public void RemoveUnit(Unit unit)
    {
        Units.Remove(unit);
    }
    public Unit[] GetAllUnits()
    {
        return Units.ToArray();
    }
}
