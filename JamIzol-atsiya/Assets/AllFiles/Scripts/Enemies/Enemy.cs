using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject target;
    public GameObject Projectile;
    public float AttackTime = 1f;
    void Start()
    {
        StartCoroutine(Attack());
    }

    void Update()
    {
        Transform enemy = target.transform;
        transform.LookAt(enemy);
    }
    private IEnumerator Attack()
    {
        while (true)
        {
            Instantiate(Projectile, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(AttackTime);
        }
    }
}
