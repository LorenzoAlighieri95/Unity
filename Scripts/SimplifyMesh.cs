using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplifyMesh : MonoBehaviour
{

    void Awake()
    {
        var originalMesh = GetComponent<MeshFilter>().sharedMesh;
        float quality = 0.2f;
        var meshSimplifier = new UnityMeshSimplifier.MeshSimplifier();
        meshSimplifier.Initialize(originalMesh);
        meshSimplifier.SimplifyMesh(quality);
        var destMesh = meshSimplifier.ToMesh();
        GetComponent<MeshFilter>().sharedMesh = destMesh;
    }
}
