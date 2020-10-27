using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.FlagsAttribute]
public enum CubeMaterial
{
    Metal_Brown,
    Metal_Blue,

    Brickwork_Brown,
    Brickwork_Blue,

    Stone_Brown,
    Stone_Blue,

    Grass_Brown,
    Grass_Blue
}
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
    private MaterialRender materialRender;
    private void Awake()
    {
        MaxHP = (CubePower + 1) * HardValue;
        ValidHP = MaxHP;
        meshRenderer = GetComponent<MeshRenderer>();
        materialRender = GameObject.FindGameObjectWithTag("GameController").GetComponent<MaterialRender>();

        meshRenderer.material = materialRender.AllCubesMaterial[CubePower];
    }
}
