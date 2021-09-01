using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cBoss : MonoBehaviour
{
    public BoxCollider LHandCol;
    public BoxCollider RHandCol;
    public cMeteo meteo;
    public GameObject Player;
    public Camera cam;
    //FSM
    public enum State
    {
        Idle,
        Smash,
        PowerSlam,
        Roar,
        Shield,
        Stomp,
        Dead
    }
    public State state = State.Idle;
    //Particle
    public ParticleSystem hitparticle;
    public ParticleSystem Beamparticle;
    public ParticleSystem WaveParticle;



    //Anim
    public Animator Anim;

    public Slider slider;


    //Status
    public float MaxHP = 10;
    public float Hp = 10;
    public float Str = 5;
    public float Deffence = 5;


    public void Start()
    {
        Anim = GetComponent<Animator>();
        meteo = GetComponent<cMeteo>();

    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Anim.SetTrigger("PowerSlap");
            StartCoroutine(PowerSlapWait());

        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            Anim.SetTrigger("Roar");
            StartCoroutine(DropWait());

        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            Anim.SetTrigger("Shield");
          

        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            Anim.SetTrigger("Stomp");

        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            Anim.SetTrigger("Smash");
            LHandCol.enabled = true;
            RHandCol.enabled = true;
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
        //    Anim.SetTrigger("End");
        }
    }
    void WaveStart()
    {

    }


    void Beam()
    {
        Beamparticle.Play();
        StartCoroutine(BeamEnd());
    }

    IEnumerator BeamEnd()
    {

        yield return new WaitForSeconds(5);
        Beamparticle.Stop();
        Anim.SetTrigger("End");
    }
    IEnumerator PowerSlapWait()
    {
        RHandCol.enabled = true;
        yield return new WaitForSeconds(5);
        RHandCol.enabled = false;
    }
    IEnumerator DropWait()
    {
        yield return new WaitForSeconds(1.0f);
        meteo.DropStart(Player.transform);
        cam.GetComponent<cCameraScript>().VibrateForTime(5);
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

            return;
        }
        else
        {
            hitparticle.Play();
        }

    }
    public void Hit(float force, Vector3 hitpos)
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

            return;
        }
        else
        {
            hitparticle.transform.position = hitpos;
            hitparticle.Play();
        }

    }
    void Stomp()
    {
        WaveParticle.Play();
    }


    void ChangeState(State s)
    {
        state = s;
        int temp = (int)state;
        Anim.SetInteger("state", temp);
        //Debug.Log(state.ToString());
    }

}
