using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class cMeteo : MonoBehaviour
{
    public GameObject Asteroid;
    public GameObject Target;

    public List<GameObject> AsteroidPool = new List<GameObject>();
    public List<GameObject> TargetPool = new List<GameObject>();
    public GameObject Player;
    public Animator anim;


    int DropMax = 5;
    public void Start()
    {
        CreatePool();
        
    }
    int dropcount = 0;
    public void DropStart(Transform pos)
    {
       
        AsteroidPool[dropcount].gameObject.SetActive(true);
        AsteroidPool[dropcount].gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        AsteroidPool[dropcount].GetComponent<cAsteroid>().DropStart(pos.position);

        TargetPool[dropcount].gameObject.SetActive(true);
        TargetPool[dropcount].GetComponent<cDropTarget>().DropStart(pos.position);
        StartCoroutine(DropWait(pos));

    }
    IEnumerator DropWait(Transform pos)
    {
        yield return new WaitForSeconds(1f);
        dropcount++;
        if (dropcount.Equals(DropMax))
        {
            dropcount = 0;
            anim.SetTrigger("End");
        }
        else
        {
            DropStart(pos);
        }
        
    }
    public void CreatePool()
    {
        GameObject MemoryPools = new GameObject("Memorys");
       
        //부모 오브젝트생성
        for (int i = 0; i < DropMax; i++)
        {
            GameObject obj = Instantiate(Asteroid, MemoryPools.transform);
            //유성 생성
            obj.name = "Asteroid_" + i.ToString("00");
            //이름 변경
            obj.SetActive(false);
            //비활성화
            AsteroidPool.Add(obj);
            //리스트에 추가
        }

        for (int i = 0; i < DropMax; i++)
        {

            GameObject obj = Instantiate(Target, new Vector3(0, 0, 0), Quaternion.identity);
            //타겟 생성
            obj.name = "Target_" + i.ToString("00");
            //이름 변경
            obj.SetActive(false);
            //비활성화
            TargetPool.Add(obj);
            //리스트에 추가
        }
    }


}
