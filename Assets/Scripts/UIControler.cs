using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class UIControler : MonoBehaviour
{
    public GameObject[] imgBaseArea = new GameObject[4];
    public GameObject[] buttonListBaseArea = new GameObject[4];
    public bool baseAreaSwitch = false;
    void Start()
    {
        if(baseAreaSwitch){
            foreach(var temp in imgBaseArea){
            temp.SetActive(false);
            }
            foreach(var temp in buttonListBaseArea){
                temp.SetActive(false);
            }

            imgBaseArea[0].SetActive(true);
            buttonListBaseArea[0].SetActive(true);  
        }        
    }

    public void LoadTestScene(){
        GameObject.Find("Loading").GetComponent<LoadingController>().SwitchScene("GrassLandScene");
    }

    public void LoadScene(string scene){
        bool mouselock = true;
        if(scene == "TitleScene" || scene == "BaseArea" ){
            mouselock = false;
        }
        GameObject.Find("Loading").GetComponent<LoadingController>().SwitchScene(scene,mouselock);
    }

    public void BaseAreaSlectButton(int imgID){ 
        if(imgID >= 0){
            StartCoroutine(SwitchBaseArea(imgID));
        }        
    }

    IEnumerator SwitchBaseArea(int imgID)
    { 
        GameObject.Find("Loading").GetComponent<LoadingController>().LoadingFadeIn();
        yield return new WaitForSeconds(1);

        foreach(var temp in imgBaseArea){
           temp.SetActive(false);
        }
        foreach(var temp in buttonListBaseArea){
            temp.SetActive(false);
        }
        imgBaseArea[imgID].SetActive(true);
        buttonListBaseArea[imgID].SetActive(true);       

        GameObject.Find("Loading").GetComponent<LoadingController>().LoadingFadeOut();
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting"); // 在Unity編輯器中輸出日誌，因為Application.Quit不會在編輯器中運作
    }
}
