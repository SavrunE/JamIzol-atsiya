using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Attacker : MonoBehaviour
{
    [Range(1, 100)]
    public int AttackDamage = 10;
    private Mover mover;

    private Collider target;
    private Collider checkTarget;

    private CubeController targetEnemy;
    private UnitComponent unit;
    private Color targetColor;

    private bool isAttack = false;
    private bool isEnemy = false;

    private Action OnDestroyWall;

    MeshRenderer mesh;

    void Start()
    {
        unit = GetComponent<UnitComponent>();
        mover = GetComponent<Mover>();

        OnDestroyWall += DestroyWallEvent;
        mover.OnRightClick += RightClick;
    }
    private void Update()
    {
        if (isEnemy)
        {
            if (isAttack)
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
        unit.NotBusy();
        target = mover.RaycastHit.collider;
        isEnemy = CheckEnemy();

        //Проверим, не на тот-же коллайдер мы нажали
        if (checkTarget != target)
        {
            checkTarget = target;
            isAttack = false;
            if (isEnemy)
            {
                unit.IsBusy();
                targetEnemy = target.gameObject.GetComponent<CubeController>();
                mesh = target.gameObject.GetComponent<MeshRenderer>();
                targetColor = MaterialRender.materialRender.GetMaterial(targetEnemy.CubePower).color;
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
            mover.CanMove = false;
            isAttack = true;
        }
    }
    private void AttackEnemy()
    {
        
        float maxHP = targetEnemy.MaxHP;
        float validHP = targetEnemy.ValidHP;

        targetEnemy.ValidHP -= AttackDamage * Time.deltaTime;
        if (validHP > 0)
        {
            Color redPower = new Color((maxHP / validHP * (targetColor.r/Color.red.r)), targetColor.g, targetColor.b);
            mesh.material.color = Color.Lerp(targetColor, redPower, 1f);
            unit.IsBusy();
        }
        else
        {
            mover.CanMove = true;
            isAttack = false;

            target.gameObject.SetActive(false);

            unit.NotBusy();
        }
    }

    private void DestroyWallEvent()
    {

    }
}
