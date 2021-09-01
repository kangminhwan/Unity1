using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class cMonster : MonoBehaviour
{
    //TIme
    public cTimeManager timeManager;
    //Effect
    public GameObject matobj;
    public ParticleSystem particle;
    //AI-Nav
    public NavMeshAgent agent;

    //Anim
    public Animator Anim;

    public Slider slider;

    public Rigidbody rigid;
    //FSM
    public enum State
    {
        Idle,
        Find,
        Attack,
        Hit,
        Stun,
        Dead,
        Move
    }
    public State state = State.Move;
    public bool attacking = false;
    public bool Hitted = false;

    //Status
    public float MaxHP = 10;
    public float Hp = 10;
    public float Str = 5;
    public float Deffence = 5;
   // public TextMesh text;

    //Interaction
    public GameObject Player;
    private float detectdis = 10;
    private float range = 1.5f;

    //Spawn
    public Vector3 spawnPos;

    //patrol 
    public Vector3 patrolpoint;



    public void FSM()
    {

        //거리
        float Dis = Vector3.Distance(Player.transform.position, this.transform.position);

        switch (state)
        {

            case State.Idle:
                //->Find
                if (Dis < detectdis && Vector3.Dot(transform.forward, (Player.transform.position - this.transform.position).normalized) > 0.5f)
                {
                    ChangeState(State.Find);
                    Anim.SetInteger("state", (int)state);
                    Anim.SetBool("walking", true);
                    agent.SetDestination(transform.position);
                }
                // transform.forward = Player.transform.position - transform.position;

                break;
            case State.Move:
                //->Find
                if (Dis < detectdis && Vector3.Dot(transform.forward, (Player.transform.position - this.transform.position).normalized) > 0.5f)
                {
                    ChangeState(State.Find);
                    Anim.SetBool("walking", true);
                }

                //Patrol   
                if (1 < Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(patrolpoint.x, 0, patrolpoint.z)))
                {
                    agent.SetDestination(patrolpoint);
                }
                else //도착 햇을때
                {

                    patrolpoint = spawnPos + new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
                    // Testobj.transform.position = patrolpoint;

                    transform.forward = patrolpoint - transform.position;
                    ChangeState(State.Idle);
                    StartCoroutine(MoveWait());
                }
                break;
            case State.Find:

                //Outof DectectDistance
                if (Dis > detectdis)
                {

                    ChangeState(State.Move);
                }
                //Following
                else if (range < Dis)
                {
                    agent.SetDestination(Player.transform.position);

                }
                //InRange
                else if (range > Dis)
                {
                    Anim.SetBool("walking", false);
                    ChangeState(State.Attack);
                }
                break;

            case State.Attack:

                if (attacking)
                {

                }
                else
                {
                    if (range + 1 < Dis)
                    {
                        ChangeState(State.Find);
                        Anim.SetBool("walking", true);
                    }
                    else
                    {

                        //  transform.eulerAngles = (Player.transform.position - transform.position);
                        // transform.forward = Player.transform.position - transform.position;

                        attacking = true;
                        Anim.SetTrigger("Attack");
                        ChangeState(State.Idle);
                        StartCoroutine(AttackWait());

                    }

                }


                break;

            case State.Hit:

                break;

            case State.Dead:
               
                break;
        }
    }


    IEnumerator AttackWait()
    {
        yield return new WaitForSeconds(2f);
        ChangeState(State.Attack);
    }
    IEnumerator MoveWait()
    {
        yield return new WaitForSeconds(4f);
        ChangeState(State.Move);
    }
    IEnumerator Hitwait(State temp)
    {
        yield return new WaitForSeconds(2.0f);
        ChangeState(temp);
    }
    IEnumerator DeadWait()
    {
        yield return new WaitForSeconds(3.0f);
        this.transform.localScale = new Vector3(0, 0, 0);
    }

    void AttackEnd()
    {
        attacking = false;
    }
    void ChangeState(State s)
    {
        state = s;
        int temp = (int)state;
        Anim.SetInteger("state", temp);
        //Debug.Log(state.ToString());
    }


    public void Hit(float force)
    {
        if (state.Equals(State.Dead))
            return;


        float damage = force - Deffence;
        if (damage > 0)
        {
            Hp -= force - Deffence;
           // text.text = "HP : " + Hp;
            slider.value = Hp / MaxHP;
        }



        if (Hp <= 0)
        {
            ChangeState(State.Dead);
            Anim.SetTrigger("Die");
            StartCoroutine(DeadWait());
            return;
        }

        if (Hitted.Equals(false))
        {
          
            timeManager.DoSlowMotion();
            particle.Play();
            State temp = state;
            state = State.Hit;
            ChangeState(State.Hit);
            Hitted = true;
            Anim.SetTrigger("Hit");
            StartCoroutine(Hitwait(temp));

        }

    }


    public void HitEnd()
    {
        Hitted = false;
       
    }

    public void FreezeVelocity()
    {
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
    }

}
