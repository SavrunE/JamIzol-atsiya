using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SpawnSpecifications : MonoBehaviour
{
    private SpawnData data;
    public SpawnData Data { get { return data; } }
    Rigidbody2D body;
    public static Action<GameObject> OnSelect;
    private float distance;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }
  
    public void Init(SpawnData data)
    {
        this.data = data;
    }
}