﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileVelosity : MonoBehaviour
{
    public float ProjectileSpeed = 3f;
    public float TimeToDestroy = 10f;
    public float Damage = 40f;

    public Vector3 Target;
    private Rigidbody rigidBody;

    public static System.Action OnUnitDead;
    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.velocity = Vector3.zero;
        rigidBody.AddForce(Target.normalized * ProjectileSpeed, ForceMode.Impulse);
        StartCoroutine(DestroyForTime());

    }
    void Update()
    {
        
    }
    private IEnumerator DestroyForTime()
    {
        yield return new WaitForSeconds(TimeToDestroy);
        Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            collision.gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Character" && other.tag == "Enemie")
        {
            UnitComponent unit = other.GetComponent<UnitComponent>();
            if (unit)
            {
                unit.CurrentHP -= Damage;
                //Debug.Log(Damage);
                if (unit.CurrentHP< 0)
                {
                    unit.gameObject.SetActive(false);
                    OnUnitDead?.Invoke();
                }
            }
        }
    }
}
