using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIControler : MonoBehaviour
{
    public void LoadTestScene(){
        SceneManager.LoadScene("TestScene");
    }
}
