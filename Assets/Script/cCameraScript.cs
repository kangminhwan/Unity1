using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public sealed class cCameraScript : MonoBehaviour
{
    public GameObject player;
    Vector3 pos;

    public cJoyStick joystick;
    Vector2 mousepos = new Vector2(0, 0);

    public float zoomSpeed = 10.0f;
    public float RotaionSpeed = 3.0f;
    Vector3 test;

    //PostProcess
    //private PostProcessVolume volume;
    public GameObject Bloom;
    public GameObject Blur;
    //Click
    RaycastHit hit;
    Vector3 originPos;

    //CameraShake
    public float ShakeAmount;
    float ShakeTime;
    Vector3 initialPosition;

    void Start()
    {
        test = player.transform.position;
        ShakeAmount = 0.5f;
       // volume.GetComponentInChildren<PostProcessVolume>();
    }

    // Update is called once per frame
    void Update()
    {


        CameraRotation();
        Zoom();
        Shake();
        ONVolume();

    }
    void ONVolume()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            if(Blur.active)
                Blur.SetActive(false);
            else
            {
                Blur.SetActive(true);
                
            }
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (Bloom.active)
                Bloom.SetActive(false);
            else
            {
                Bloom.SetActive(true);

            }
        }
    }

    void Shake()
    {
        if (ShakeTime > 0)
        {
            this.transform.position = Random.insideUnitSphere* ShakeAmount + initialPosition;
            
           // transform.position =new Vector3(Random.value, Random.value, Random.value)* ShakeAmount + initialPosition;
            ShakeTime -= Time.deltaTime;
        }
       
    }
    public void VibrateForTime(float time)
    {
        ShakeTime = time;
        initialPosition = this.transform.position;
    }

    private void Zoom()
    {
        float distance = Input.GetAxis("Mouse ScrollWheel") * -1 * zoomSpeed;
        if (distance != 0)
        {
            Camera.main.fieldOfView += distance;
        }
    }
    void CameraRotation()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousepos = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {

            }
        }

        if (Input.GetMouseButton(0))
        {
            if (!joystick.MoveChek)
            {

                if (Mathf.Abs(mousepos.x - Input.mousePosition.x) > Mathf.Abs(mousepos.y - Input.mousePosition.y))
                    transform.RotateAround(player.transform.position, Vector3.up, RotaionSpeed * Time.deltaTime * (mousepos.x - Input.mousePosition.x));
                else
                {
                    transform.RotateAround(player.transform.position, transform.right, RotaionSpeed * Time.deltaTime * (mousepos.y - Input.mousePosition.y));

                }
            }



            mousepos = Input.mousePosition;
        }


    }

}