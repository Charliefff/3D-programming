using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class UIControler : MonoBehaviour
{
    public GameObject[] DashBoard = new GameObject[10];
    private int Dashboard_index;
    private EventSystem eventSystem;

    void Start()
    {
        Dashboard_index = 0;
        eventSystem = EventSystem.current;
    }

    public void Dashboard_reset(){
        for (int i = 1; i < DashBoard.Length; i++)
        {
            if (DashBoard[i] != null)
            {
                DashBoard[i].SetActive(false);
            }
        }
        if (DashBoard[0] != null)
        {
            DashBoard[0].SetActive(true);
        }
        Dashboard_index = 0;

        eventSystem.SetSelectedGameObject(null);
    }

    public void Dashboard_state(){
        DashBoard[Dashboard_index].SetActive(false);        
        DashBoard[0].SetActive(true);
        Dashboard_index = 0;     
    }
    public void Dashboard_item(){
        DashBoard[Dashboard_index].SetActive(false);        
        DashBoard[1].SetActive(true);
        Dashboard_index = 1;     
    }
    public void Dashboard_equpiment(){
        DashBoard[Dashboard_index].SetActive(false);        
        DashBoard[2].SetActive(true);
        Dashboard_index = 2;    
    }
    public void Dashboard_ability(){
        DashBoard[Dashboard_index].SetActive(false);        
        DashBoard[3].SetActive(true);
        Dashboard_index = 3;    
    }
    public void Dashboard_save(){
        DashBoard[Dashboard_index].SetActive(false);        
        DashBoard[4].SetActive(true);
        Dashboard_index = 4;    
    }
    public void Dashboard_load(){
        DashBoard[Dashboard_index].SetActive(false);        
        DashBoard[5].SetActive(true);
        Dashboard_index = 5;    
    }
    public void Dashboard_system(){
        DashBoard[Dashboard_index].SetActive(false);        
        DashBoard[6].SetActive(true);
        Dashboard_index = 6;    
    }
    public void Dashboard_back(){
        SceneManager.LoadScene("TitleSceneTest");
    }
    public void LoadTestScene(){
        SceneManager.LoadScene("TestScene");
    }

    
}
