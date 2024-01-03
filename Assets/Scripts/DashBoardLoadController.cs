using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 
using UnityEngine.UI;

public class DashBoardLoadController : MonoBehaviour
{
    public GameObject[] saveButton = new GameObject[4];
    public GameObject determineWindow;
    void Start()
    {
        UpdateLoad();
    }

    void Update()
    {
        
    }

    public void UpdateLoad(){
        determineWindow.SetActive(false);

        for(int i=0;i<4;i++){
            if(i==0){
                saveButton[i].transform.Find("detail").GetComponent<TextMeshProUGUI>().text = "自動存檔\n";
            }else{
                saveButton[i].transform.Find("detail").GetComponent<TextMeshProUGUI>().text = "存檔"+i+"\n";
            }

            if(Base.saveDataDictionary.ContainsKey("" + i)){                
                saveButton[i].transform.Find("detail").GetComponent<TextMeshProUGUI>().text += "時間: " + Base.saveDataDictionary["" + i].Day + "\t地點: " + Base.saveDataDictionary["" + i].SceneName;
                saveButton[i].GetComponent<Button>().interactable = true;
            }else{
                saveButton[i].GetComponent<Button>().interactable = false;
            }
        }
    }

    public void PressLoadButton(int index){
        determineWindow.SetActive(true);

        determineWindow.transform.Find("trueButton").GetComponent<Button>().onClick.AddListener(() => DetermineWindowClick(index));
        determineWindow.transform.Find("falseButton").GetComponent<Button>().onClick.AddListener(() => determineWindow.SetActive(false));
        
    }

    private void DetermineWindowClick(int index){
        determineWindow.SetActive(false);
        GameObject.FindObjectOfType<DashBoardController>().DashBoard_switch();

        Base.LoadGame(index);
    }
}
