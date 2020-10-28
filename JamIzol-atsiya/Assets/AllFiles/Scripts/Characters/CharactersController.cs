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
    private CubeController targetWall;
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
                AttackWall();
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

        targetIsEnemy = CheckEnemy();

        //Проверим, не на тот-же коллайдер мы нажали
        if (checkTarget != target)
        {
            checkTarget = target;

            canAttack = false;
            if (targetIsEnemy)
            {
                targetWall = GetCubeController(target, targetColor);
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
    private void AttackWall()
    {
        float maxHP = targetWall.MaxHP;

        targetWall.ValidHP -= AttackDamage * Time.deltaTime;
        if (targetWall.ValidHP > 0)
        {
            mover.canMove = false;

            float redColor = (maxHP - targetWall.ValidHP) / maxHP;

            targetWall.gameObject.GetComponent<MeshRenderer>().material.color = Color.Lerp(targetColor, Color.red, redColor * Time.deltaTime);
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
    private CubeController GetCubeController(Collider collider, Color targetColor)
    {
        CubeController result;
        result = collider.gameObject.GetComponent<CubeController>();
        targetColor = collider.gameObject.GetComponent<MeshRenderer>().material.color;

        return result;
    }
}
