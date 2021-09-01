using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cSceneManger : MonoBehaviour
{
 

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
           
            SceneManager.LoadScene(2, LoadSceneMode.Single);
        }
    }
    public void Scene1()
    {
        
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}
