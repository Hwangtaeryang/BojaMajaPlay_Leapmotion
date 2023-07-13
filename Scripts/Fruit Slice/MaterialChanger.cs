using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MaterialChanger : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    public Material[] materials;


    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        ChangeWallMaterial();
    }

    public void ChangeWallMaterial()
    {
        if (materials.Length > 0)
            meshRenderer.sharedMaterial = materials[Random.Range(0, materials.Length)];
    }
}
