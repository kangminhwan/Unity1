using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cDropTarget : MonoBehaviour
{

    public Transform trans;
    private void Awake()
    {
        trans = this.transform;
        transform.localScale = Vector3.zero;
        trans.eulerAngles = new Vector3(90, 0, 0);
    }
  
    // Update is called once per frame
    void Update()
    {
       transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(2, 2, 2), Time.deltaTime);
    }
    public void DropStart(Vector3 pos)
    {
        trans.position = new Vector3(pos.x, pos.y, pos.z);
        StartCoroutine(DropWait());

    }
    IEnumerator DropWait()
    {
        yield return new WaitForSeconds(3f);
        trans.localScale = Vector3.zero;
        this.gameObject.SetActive(false);

    }
}


