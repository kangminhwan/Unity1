using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cSkin : MonoBehaviour
{
    public List<Material> Materials = new List<Material>();
    public SkinnedMeshRenderer SkinnedMesh;

    int count = 0;


    public void RightClick()
    {
        count++;
        if (count == Materials.Capacity)
            count = 0;
        SkinnedMesh.material = Materials[count];

    }
    public void LeftClick()
    {
        count--;
        if (count <0)
            count = Materials.Capacity;
        SkinnedMesh.material = Materials[count];

    }




}
