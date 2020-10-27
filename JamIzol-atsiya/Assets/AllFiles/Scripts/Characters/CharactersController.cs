using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersController : MonoBehaviour
{
    [Range (1, 100)]
    public int AttackDamage = 10;
    private Mover mover;
    private Collider target;
    private Collider checkTarget;
    private CubeController targetWall;
    void Start()
    {
        mover = GetComponent<Mover>();
    }
    private void Update()
    {
        target = mover.raycastHit.collider;

        if (target != checkTarget)
        {
            checkTarget = target;
            targetWall = GetCubeController(target);
            StopCoroutine(AttackWall());
        }

        if (target != null 
            && target.tag == "Wall"
            && Vector3.Distance(target.transform.position, transform.position) < 2f)
        {
            StartCoroutine(AttackWall());
            
        }
    }
    public IEnumerator AttackWall()
    {
        bool canMove = mover.canMove;
        canMove = false;

        
        float maxHP = targetWall.MaxHP;
        Color validColor = targetWall.ValidColor;

        targetWall.ValidHP -= AttackDamage * Time.deltaTime;
        Debug.Log(targetWall.ValidHP);
        if (targetWall.ValidHP <= 0)
        {
            target.gameObject.SetActive(false);
            canMove = true;
            yield return new WaitForFixedUpdate();
        }
        else
        {
            float redCollar = (maxHP - targetWall.ValidHP) / maxHP;

            targetWall.gameObject.GetComponent<MeshRenderer>().material.color = Color.Lerp(targetWall.gameObject.GetComponent<MeshRenderer>().material.color, Color.red, redCollar * Time.deltaTime);
            //targetColor = Color.Lerp(targetColor, red, targetWall.ValidHP);
        }
    }
    private CubeController GetCubeController(Collider collider)
    {
        CubeController result;
        result = collider.gameObject.GetComponent<CubeController>();
        return result;
    }
}
