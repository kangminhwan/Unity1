using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class cHadAttack : MonoBehaviour
{
    float attack = 1;
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.CompareTag("Player"))
        {
            other.GetComponent<cMB_player>().Hit(attack);
            this.transform.GetComponent<BoxCollider>().enabled = false;

        }
    }
}
