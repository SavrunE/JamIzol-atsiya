using UnityEngine;

public class LightPingPong : MonoBehaviour
{
    private Light myLight;

    void Start()
    {
        myLight = GetComponent<Light>();
    }

    void Update()
    {
        myLight.intensity = Mathf.PingPong(2 *Time.time, 2);
    }
}