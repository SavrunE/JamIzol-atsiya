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

    private Action OnDestroyWall;
    private Action OnAttackWall;
    
    void Start()
    {
        mover = GetComponent<Mover>();

        target = mover.raycastHit.collider;
        checkTarget = target;

        OnDestroyWall += DestroyWallEvent;
        mover.OnRightClick += RightClick;
    }
    private void Update()
    {
        OnAttackWall?.Invoke();
    }
    private void RightClick()
    {
        Debug.Log(target);
        Debug.Log(checkTarget);
        target = mover.raycastHit.collider;

        //Проверим, не на тот-же куб мы нажали
        if(checkTarget != target)
        {
            checkTarget = target;

            Debug.Log(target);
            Debug.Log(checkTarget);
            if (target != null && target.tag != "Ground" && target.tag == "Wall")
            {
                targetWall = GetCubeController(target);
                StartCoroutine(CheckDistanceToWall());
            }
        }
    }
    private IEnumerator CheckDistanceToWall()
    {
        if (Vector3.Distance(target.transform.position, transform.position) < 2f)
        {
            OnAttackWall += AttackWall;
        }
        else
        {
            yield return new WaitForEndOfFrame();
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
            OnAttackWall -= AttackWall;
        }
    }
   
    private void DestroyWallEvent()
    {

    }
    private CubeController GetCubeController(Collider collider)
    {
        CubeController result;
        result = collider.gameObject.GetComponent<CubeController>();
        targetColor = collider.gameObject.GetComponent<MeshRenderer>().material.color;

        return result;
    }
}
