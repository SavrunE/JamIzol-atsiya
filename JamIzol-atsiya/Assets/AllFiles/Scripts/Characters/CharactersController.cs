using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharactersController : MonoBehaviour
{
    [Range(1, 100)]
    public int AttackDamage = 10;
    private Mover mover;
    private Collider target;
    private Collider checkTarget;
    private CubeController targetEnemy;
    private Color targetColor;

    private bool canAttack;
    private bool targetIsEnemy;

    private Action OnDestroyWall;
    private Action OnAttackWall;

    void Start()
    {
        canAttack = false;
        mover = GetComponent<Mover>();

        target = mover.raycastHit.collider;
        checkTarget = target;

        OnDestroyWall += DestroyWallEvent;

        mover.OnRightClick += RightClick;
    }
    private void Update()
    {
        if (targetIsEnemy)
        {
            if (canAttack)
            {
                AttackEnemy();
            }
            else
            {
                CheckDistanceToWall();
            }
        }
    }
    private void RightClick()
    {
        target = mover.raycastHit.collider;
        
        //Проверим, не на тот-же коллайдер мы нажали
        if (checkTarget != target)
        {
            checkTarget = target;
            canAttack = false;

            targetIsEnemy = CheckEnemy();

            if (targetIsEnemy)
            {
                targetEnemy = target.gameObject.GetComponent<CubeController>();
                targetColor = target.gameObject.GetComponent<MeshRenderer>().material.color;
            }
        }
    }
    private bool CheckEnemy()
    {
        if (target != null)
            return target.tag == "Wall";
        else
            return false;
    }
    private void CheckDistanceToWall()
    {
        if (Vector3.Distance(target.transform.position, transform.position) < 2f)
        {
            canAttack = true;
        }
    }
    private void AttackEnemy()
    {
        float maxHP = targetEnemy.MaxHP;
        float validHP = targetEnemy.ValidHP;

        targetEnemy.ValidHP -= AttackDamage * Time.deltaTime;
        if (validHP > 0)
        {
            mover.canMove = false;

            float redColor = (maxHP - validHP) / maxHP;

            Color redPower = new Color((maxHP / validHP * (targetColor.r/Color.red.r)), targetColor.g, targetColor.b);
            target.gameObject.GetComponent<MeshRenderer>().material.color = Color.Lerp(targetColor, redPower, 1);
        }
        else
        {
            target.gameObject.SetActive(false);
            mover.canMove = true;
            canAttack = false;
        }
    }

    private void DestroyWallEvent()
    {

    }
}
