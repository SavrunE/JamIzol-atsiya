using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersController : MonoBehaviour
{
    [Range(1, 100)]
    public int AttackDamage = 10;
    private Mover mover;
    private Collider target;
    private Collider checkTarget;
    private CubeController targetWall;
    private Color targetColor;
    private bool IsDigging;
    void Start()
    {
        IsDigging = false;
        mover = GetComponent<Mover>();
        target = mover.raycastHit.collider;
        checkTarget = target;
    }
    private void Update()
    {
        target = mover.raycastHit.collider;
        if(checkTarget != target)
        {
            IsDigging = false; //сделать свзяь эвентом с  mover.canMove
        }

        if (target != null && target.tag != "Ground")
        {
            targetWall = GetCubeController(target);
            StartCoroutine(CheckDistanceToWall());
        }
    }
    private IEnumerator CheckDistanceToWall()
    {
        //StopCoroutine(AttackWall());
        if (target.tag == "Wall" && Vector3.Distance(target.transform.position, transform.position) < 2f)
        {
            StartCoroutine(AttackWall());
            if (!IsDigging)
            {
                mover.canMove = true;
                StopCoroutine(AttackWall());
                yield break;
            }
        }
    }
    private IEnumerator AttackWall()
    {
        mover.canMove = false;
        IsDigging = true;

        float maxHP = targetWall.MaxHP;

        targetWall.ValidHP -= AttackDamage * Time.deltaTime;
        Debug.Log(targetWall.ValidHP);
        if (targetWall.ValidHP <= 0)
        {
            target.gameObject.SetActive(false);
            mover.canMove = true;
            IsDigging = false;
        }
        else
        {
            float redColar = (maxHP - targetWall.ValidHP) / maxHP;

            targetWall.gameObject.GetComponent<MeshRenderer>().material.color = Color.Lerp(targetColor, Color.red, redColar * Time.deltaTime);
        }
        yield break;
    }
    private CubeController GetCubeController(Collider collider)
    {
        CubeController result;
        result = collider.gameObject.GetComponent<CubeController>();
        targetColor = collider.gameObject.GetComponent<MeshRenderer>().material.color;

        return result;
    }
}
