using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class cSword : MonoBehaviour
{
    
    public cMB_player player;
    public BoxCollider mcollider;
    public TrailRenderer trail;
    

    void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag("Monster"))
        {
            other.GetComponent<cMonster>().Hit(player.Str);
            
        }
    }
  

}
