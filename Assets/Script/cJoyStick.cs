using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class cJoyStick : MonoBehaviour
{
    public Image Joy;
    private Vector3 Pos;
    float StickDIstance = 0; //조이스틱이 갈수 있는 범위


    Vector3 bpos;
    public GameObject Player;

    Vector3 dir;
    public float MoveDistance;


    public bool MoveChek = false;


    void Start()
    {
        bpos = Player.transform.position;
        Pos = Joy.transform.position;
        StickDIstance = Joy.rectTransform.sizeDelta.x / 1.2f;
        //sizeDelta는 앵커사이에 따른 사각형의 크기
    }
    void Update()
    {

        if (MoveChek)
        {
            //Vector3 PlayerMove = new Vector3(dir.x, 0, dir.y);
            //Player.transform.Translate(PlayerMove * MoveDistance / 100 * Time.deltaTime);


            if (Player.GetComponent<cMB_player>().attacking)
                return;
            // Player.transform.Translate(moving * MoveDistance / 50 * Time.deltaTime);
            Player.transform.Translate(Vector3.forward * MoveDistance / 50 * Time.deltaTime);

           
        }

        //   Debug.Log(MoveDistance);
    }


    public void Drag_PC()
    {

        MoveChek = true;
        Player.GetComponent<cMB_player>().SetMove(MoveChek);

        if (Joy == null)
            return;
        Vector3 MousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);

        dir = (MousePos - Pos).normalized;
        //조이스틱이 움직인 방향

        MoveDistance = Vector3.Distance(Pos, MousePos);
        //조이스틱이 얼마나 움직였는지 체크
        if (MoveDistance > StickDIstance)
        {
            Joy.rectTransform.position = Pos + (dir * StickDIstance);
            MoveDistance = 200;
            //반경을 넘어갈 경우 방향으로 정해진 길이만 가도록 설정
        }
        else
        {
            Joy.transform.position = MousePos;
        }
        if (Player.GetComponent<cMB_player>().attacking)
            return;
        Player.transform.eulerAngles = new Vector3(0, Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y, 0);


        //eulerAngles : 각도 단위로 오일러 각도로 회전
        //Mathf.Atan2 : 각도를 탄젠트 y/x인 라디안으로 반환합니다.
        //Mathf.Rad2Deg : 라디안에서 하위 수준으로의 변환 상수
    }

    public void EndDrag()
    {
        MoveChek = false;
        Player.GetComponent<cMB_player>().SetMove(MoveChek);
        if (Joy != null)
        {
            Joy.rectTransform.position = Pos;
            MoveDistance = 0;
            //터치가 끝나면 조이스틱 원위치
        }
    }
}