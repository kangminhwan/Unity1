using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class cTurtle : cMonster
{


    void Start()
    {


        agent = GetComponent<NavMeshAgent>();
        spawnPos = transform.position;
        patrolpoint = transform.position;

        rigid = GetComponent<Rigidbody>();

        //particle = GetComponent<ParticleSystem>();

    }

    void Update()
    {
        FSM();
        //if(Input.GetKeyDown(KeyCode.A))
        //     mat.shader = Shader.Find("Legacy Shaders/Transparent/Diffuse");
    }
    private void FixedUpdate()
    {
        FreezeVelocity();
    }

}
