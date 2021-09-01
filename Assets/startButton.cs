using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class startButton : MonoBehaviour
{
    public RectTransform transform;
    private float t = 0;
    public float speed=2;
    public float scale = 0.5f;
    public void Update()
    {
        t += Time.deltaTime*speed;
        float sin = Mathf.Sin(t)*scale;

        transform.localScale = new Vector3(sin+1.7f, sin+ 1.5f, 0);

    }
}
