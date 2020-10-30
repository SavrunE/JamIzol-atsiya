using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Rigidbody Projectile;
    private GameObject[] targets;

    private GameObject nearEnemy;
    private Transform enemy;

    private NavMeshAgent agent;

    private int maxUnits = 3;
    private int targetsCount = 0;
    public float AttackTime = 1f;

    private float nearEnemyDistance;

    public float RetreatDistance = 3f;
    public float StoppingDistance = 2f;

    private float speed = 10f;

    public float MaxHP = 255;
    public float CurrentHP;

    void Start()
    {
        CurrentHP = MaxHP;
        agent = GetComponent<NavMeshAgent>();
        CheckTargets();

        StartCoroutine(Attack());

        StartCoroutine(MovingController());

        ProjectileVelosity.OnUnitDead += () => StopCoroutine(MovingController());
        ProjectileVelosity.OnUnitDead += CheckTargets;
        ProjectileVelosity.OnUnitDead += () => StartCoroutine(MovingController());
    }
    private void CheckTargets()
    {
        targetsCount = 0;
        targets = new GameObject[maxUnits];
        foreach (GameObject target in GameObject.FindGameObjectsWithTag("Character"))
        {
            targets[targetsCount] = target;

            //Debug.Log(targets[targetsCount]);
            targetsCount++;
        }
        CheckNearEnemy();
        if (!nearEnemy)
        {
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
       // if (nearEnemy)
           // transform.LookAt(nearEnemy.transform);
    }

    private IEnumerator MovingController()
    {
        while (true)
        {
            CheckNearEnemy();

            if (nearEnemyDistance > StoppingDistance)
            {
                Move();
            }
            else if (nearEnemyDistance < StoppingDistance && nearEnemyDistance > RetreatDistance)
            {
                DontMove();
            }

            yield return new WaitForSeconds(3f);
        }
    }
    private void Move()
    {
        if (nearEnemy) {
            agent.SetDestination(nearEnemy.transform.position);
        }
    }
    private void DontMove()
    {
        agent.SetDestination(transform.position);
    }

    private IEnumerator Attack()
    {
        while (true)
        {
            CheckNearEnemy();
            if (nearEnemy)
            {
                Rigidbody projectile = Instantiate(Projectile, transform.position, Quaternion.identity) ;
                projectile.velocity = transform.TransformDirection(transform.right * speed);
                ProjectileVelosity component = projectile.GetComponent<ProjectileVelosity>();
                component.Target = nearEnemy.gameObject.transform.position;
                projectile.transform.right = transform.right;

            }
            yield return new WaitForSeconds(AttackTime);
        }
    }
    private void CheckNearEnemy()
    {
        nearEnemyDistance = 999f;
        foreach (GameObject target in targets)
        {
            if (target)
            {
                float distance = Vector3.Distance(transform.position, target.transform.position);
                if (distance < nearEnemyDistance)
                {
                    //Debug.Log(nearEnemyDistance);
                    nearEnemy = target;
                    nearEnemyDistance = distance;
                }
            }
        }
        nearEnemyDistance = Vector3.Distance(transform.position, nearEnemy.transform.position);
    }
}
