using UnityEngine;
using System.Collections;

public class MouseScrollWheel : MonoBehaviour
{
    private Transform target;

    [SerializeField]
    private float scrollSpeed = 10.0f;
    [SerializeField]
    private float zoomMin = 1.0f;
    [SerializeField]
    private float zoomMax = 20.0f;
    [SerializeField]
    [Range(0.1f,1f)]
    private float response = 0.5f;
    
    private float zoomEndPositionY;
    private float point;
    private void Start()
    {
        target = GetComponent<Transform>();
    }
    void LateUpdate()
    {
        if (target)
        {
            float moveScale = Input.GetAxis("Mouse ScrollWheel") * (-1);
            float sqrMove = moveScale * moveScale;

            if (moveScale != 0 && sqrMove < 1)
            {
                zoomEndPositionY = target.position.y + moveScale * scrollSpeed;
                point = CheckZoom(zoomEndPositionY, zoomMin, zoomMax);
                Vector3 endPosition = new Vector3(target.position.x, point, target.position.z);

                target.position = Vector3.Lerp(transform.position, endPosition, response);
            }
        }
    }
    private static float CheckZoom(float zoomEndPosition, float min, float max)
    {
        if (zoomEndPosition < min)
            zoomEndPosition = min;
        if (zoomEndPosition > max)
            zoomEndPosition = max;
        return zoomEndPosition;
    }
}