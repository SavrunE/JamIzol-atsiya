using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialRender : MonoBehaviour
{
    public static MaterialRender materialRender = null;

    public Material[] AllCubesMaterial;

    private void Awake()
    {
        if (materialRender == null)
        {
            materialRender = this;
        }
        else if (materialRender == this)
        {
            Destroy(gameObject);
        }
    }
    public Material GetMaterial(int i)
    {
        return AllCubesMaterial[i];
    }
}