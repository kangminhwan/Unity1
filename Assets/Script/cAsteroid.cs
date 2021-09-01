using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cAsteroid : MonoBehaviour
{
    public GameObject targetImg;
    public Transform trans;

    public void Awake()
    {
        trans = this.transform;
    }
   
    public void DropStart(Vector3 pos)
    {


       //trans.localScale = new Vector3(3, 3, 3);
       // this.transform.position = new Vector3(pos.x, pos.y + 20.0f, pos.z);
        trans.position = new Vector3(pos.x, pos.y + 20.0f, pos.z);
        //Img.transform.position = pos;

    }


    //void OnTriggerEnter(Collider other)
    //{

    //    Img.transform.localScale = Vector3.zero;
    //    gameObject.SetActive(false);
    //    if (other.CompareTag("Player"))
    //    {
    //        other.GetComponent<cMB_player>().Hit(10);

    //    }
    //}
}
