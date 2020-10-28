using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class CubeController : MonoBehaviour
{
    [Range(0,7)]
    public int CubePower ;
    //мб успею сложность докрутить
    public float HardValue = 10f;

    [HideInInspector]
    public float MaxHP;
    [HideInInspector]
    public float ValidHP;
    [HideInInspector]
    public Color ValidColor;

    private MeshRenderer meshRenderer;

    private Material material; 
    private void Start()
    {
        MaxHP = (CubePower + 1) * HardValue;
        ValidHP = MaxHP;
        meshRenderer = GetComponent<MeshRenderer>();

        material = MaterialRender.materialRender.GetMaterial(1);
        meshRenderer.material = material;

        
    }
}
