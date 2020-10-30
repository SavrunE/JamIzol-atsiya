using UnityEngine;

public class SpawnPrefabOnKeyDown : MonoBehaviour
{
    public GameObject m_Prefab;
    public KeyCode m_KeyCode = KeyCode.S;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
    void Update()
    {
        if (Input.GetKeyDown(m_KeyCode) && m_Prefab != null)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            RaycastHit RaycastHit;
            if (Physics.Raycast(ray, out RaycastHit))
            {
                Vector3 point = RaycastHit.point;

                Instantiate(m_Prefab, transform.position, transform.rotation);
            }
        }
    }
}
