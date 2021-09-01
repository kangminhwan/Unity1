using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AI;


public sealed class cMB_player : MonoBehaviour
{
    public Rigidbody rigid;
    public cJoyStick joystick;
    public Animator Anim;
    public bool attacking;
    public cSword sword;
    public GameObject motion;
    public Camera cam;

    //Status
    public float MaxHP = 10;
    public float Hp = 10;
    public float Str = 6;
    public float Deffence = 5;

    public bool b_Move = false;

    //Click
    RaycastHit hit;
    public GameObject targetImage;

    GameObject targetObj = null;
    bool isMoving = false;
    public NavMeshAgent agent;
    string attackName;
    Vector3 bpos;


    //boss
    public Transform bosspoint;


    void Start()
    {
        attacking = false;
        bpos = transform.position;
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Camera.main.transform.position += transform.position - bpos;
        bpos = transform.position;

        Anim.SetFloat("Speed", joystick.MoveDistance);
        LockOn();

        if (Input.GetKey(KeyCode.H))
            transform.position = bosspoint.position;

    }
    private void FixedUpdate()
    {
        FreezeVelocity();
    }
    public void FreezeVelocity()
    {
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
    }

    public void SetMove(bool temp)
    {
        b_Move = temp;
        motion.GetComponent<MotionTrail>().spawn = temp;

    }

    public void Slash()
    {
        attackStart("Slash");

    }
    public void Skill1()
    {
        attackStart("Skill1");
 
    }
    public void Skill2()
    {
        attackStart("Skill2");

    }
    public void Skill3()
    {
        attackStart("Skill3");      
    }
    void attackStart(string Skillname)
    {
        if (targetObj != null)
        {
            if (Vector3.Distance(targetObj.transform.position, transform.position) > 2f)
            {
                Anim.SetFloat("Speed", 50f);
                isMoving = true;
                agent.SetDestination(targetObj.transform.position);
                attackName = Skillname;
                return;
            }
        }
      
       
        isMoving = false;
        Anim.SetFloat("Speed", 0f);
        Anim.SetTrigger(Skillname);
        
        //motion.SetActive(true);
        motion.GetComponent<MotionTrail>().spawn = true;
        attacking = true;
        sword.mcollider.enabled = true;
        sword.trail.enabled = true;
    }
    public void attackEnd()
    {
        //motion.SetActive(false);
        motion.GetComponent<MotionTrail>().spawn = false;
        attacking = false;
        sword.mcollider.enabled = false;
        sword.trail.enabled = false;
    }


    public void LockOn()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit)&&!EventSystem.current.IsPointerOverGameObject())
            {
                if (hit.transform.tag.Equals("Monster"))//Targeting
                {
                    targetObj = hit.transform.gameObject;
                }
                else//NoneTarget
                {
                    agent.ResetPath();
                   
                    targetImage.transform.position = new Vector3(0, 0, 0);
                    targetObj = null;
                    isMoving = false;
                }
            }
        }

        if (targetObj != null)
        {
            targetImage.transform.position = targetObj.transform.position + (cam.transform.position - targetObj.transform.position).normalized;
            targetImage.transform.LookAt(cam.transform.position);

            if (isMoving)
            {
                Anim.SetFloat("Speed", 100f);
                
                agent.SetDestination(targetObj.transform.position);
                float dis = Vector3.Distance(targetObj.transform.position, transform.position);
                if (dis <= 2f)
                {
                    isMoving = false;
                    agent.ResetPath();
                    attackStart(attackName);
                }
            }
        }
    }
    public void Hit(float damage)
    {
        Hp -= damage;
        Debug.Log(Hp);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boss"))
        {
            Debug.Log("ON");


        }
    }
}