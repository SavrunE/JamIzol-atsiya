using UnityEngine;

[DisallowMultipleComponent, RequireComponent(typeof(Camera))]
public sealed class CameraControl : MonoBehaviour
{
    private Camera camera;

    private void Start()
    {
        camera = GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(2))
        {
            camera.transform.position -= new Vector3(Input.GetAxis("MouseX"), 0f, Input.GetAxis("MouseZ"));
        }
    }
}