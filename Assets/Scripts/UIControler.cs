using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class UIControler : MonoBehaviour
{
    void Start()
    {

    }
    public void LoadTestScene(){
        SceneManager.LoadScene("TestScene");
    }

    
}
