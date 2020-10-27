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
    public CubeMaterial Material;
    private MeshRenderer meshRenderer;
    private MaterialRender materialRender;
    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        materialRender = GameObject.FindGameObjectWithTag("GameController").GetComponent<MaterialRender>();
        meshRenderer.material = materialRender.AllCubesMaterial[CubePower];
    }
}
