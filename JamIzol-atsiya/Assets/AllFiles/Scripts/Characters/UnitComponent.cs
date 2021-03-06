﻿using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class UnitComponent : MonoBehaviour
{
    [Range(1f, 100f)]
    public float AttackDamage = 10;
    public float MoveSpeed
    {
        get
        {
            if (agent)
            {
                return agent.speed;
            }
            return 0f;
        }
        set
        {
            agent.speed = value;
        }
    }

    public GameObject SelectDisplay;
    [HideInInspector]
    public Camera mainCamera;

    public float MaxHP = 100f;
    public float CurrentHP;

    private bool isSelected;
    private bool isBusy;
    private Mover mover;
    private NavMeshAgent agent;
    private GenerateDefendCube generater;
    public NavMeshAgent Agent { get { return agent; } set { } }
    [SerializeField] private float radius = 15f;


    public delegate void Click();
    public Click OnRightClick;

    public bool CheckBusy { get { return isBusy; } set { } }

    public bool IsSelected
    {
        get { return isSelected; }
    }

    void Start()
    {
        CurrentHP = MaxHP;
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        agent = GetComponent<NavMeshAgent>();
        mover = GetComponent<Mover>();
        generater = GetComponent<GenerateDefendCube>();
        UnitSelect.AddUnit(this);
        agent.speed += 1;

        StartCoroutine(RestoreHP());
    }
    private IEnumerator RestoreHP()
    {
        while(true)
            {
            float CheckHP = CurrentHP + 10f;
            if (CheckHP < MaxHP)
            {
                CurrentHP = CheckHP;
            }
            else
            {
                CurrentHP = CheckHP;
            }

            yield return new WaitForSeconds(5f);
        }
    }
    private void Update()
    {
        FogOfWar.Instance.Disperse(transform.position, radius);

        if (isSelected)
        {
            if (Input.GetMouseButtonUp(1))
            {
                Moving();
                OnRightClick?.Invoke();
            }
        }
    }
 
    private void Dead()
    {

    }

    public void Deselect()
    {
        isSelected = false;
        if (SelectDisplay)
        {
            SelectDisplay.SetActive(false);
        }
    }

    public void Select()
    {
        isSelected = true;
        if (SelectDisplay)
        {
            SelectDisplay.SetActive(true);
        }
    }
    public void IsBusy()
    {
        isBusy = true;
    }
    public void NotBusy()
    {
        isBusy = false;
    }
    public void Moving()
    {
        mover.MoveDestination(agent, mainCamera);
    }

    public void StopMoving()
    {
        mover.StopMoving(agent);
    }
}