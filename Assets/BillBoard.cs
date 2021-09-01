using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class BillBoard : MonoBehaviour
{
     
    void Update()
    {
       
        transform.LookAt(Camera.main.transform.position);
    }
}
