using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{

    public GameObject Player;

    Vector3 distance;
    
    // Start is called before the first frame update
    void Start()
    {
        distance = Player.transform.position - this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Player.transform.position - distance;
    }
}
