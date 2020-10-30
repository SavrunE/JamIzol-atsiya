using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateDefendCube : MonoBehaviour
{
    private UnitComponent unit;
    private Transform LookOnClick;

    public GameObject m_Prefab;
    public KeyCode m_KeyCode = KeyCode.S;
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
        if (Input.GetKeyDown(m_KeyCode) && m_Prefab != null)
        {
            Ray ray = unit.mainCamera.ScreenPointToRay(Input.mousePosition);

            RaycastHit RaycastHit;
            if (Physics.Raycast(ray, out RaycastHit))
            {
                Vector3 point = RaycastHit.point;

                Instantiate(m_Prefab, transform.position, transform.rotation);
            }
        }
    }
}

