using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileVelosity : MonoBehaviour
{
    public float ProjectileSpeed = 3f;
    private Rigidbody rigidBody;
    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.AddForce(Vector3.forward * ProjectileSpeed, ForceMode.Impulse);
    }
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
      
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall")
        {
            //velocity = -velocity;
        }
    }
    
}
