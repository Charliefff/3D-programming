using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 
public class DialogController : MonoBehaviour
{
    public List<Dialog> dialogList;
    public int dialogIndex;
    public GameObject Communication;
    public TextMeshProUGUI nameText; 
    public TextMeshProUGUI dialogText;
    public GameObject[] option = new GameObject[3];
    public TextMeshProUGUI[] optionText = new TextMeshProUGUI[3];
    private LoadingController loadingController;
    private bool selectOption;
    private List<int> optionList = new List<int>();
    

    // Start is called before the first frame update
    void Start()
    {
        // dialogIndex = 0;
        selectOption = false;
        CloseOption();
        loadingController = GameObject.FindObjectOfType<LoadingController>();
    }

    // Update is called once per frame
    void Update()
    {
        print(dialogIndex);
        if (Input.anyKeyDown && !selectOption)
        {
            NextDialog();
        }
    }    

    public void ShowDialog(){
        DialogEffect();
        
        if(dialogList[dialogIndex].type <= 1){
            UpdateText(dialogList[dialogIndex].name,dialogList[dialogIndex].content);
            dialogIndex = dialogList[dialogIndex].next_id;            
        }

        if(dialogList[dialogIndex].type == 2){
            SetOption();
        }
    }

    public void DialogEffect(){
        if(dialogList[dialogIndex].effect == "consumable"){
            if(Base.bagConsumable.ContainsKey(dialogList[dialogIndex].target)){
                Base.bagConsumable[dialogList[dialogIndex].target] += 1;
            }else{
                Base.bagConsumable[dialogList[dialogIndex].target] = 1;
            }
        }
    }

    public void UpdateText(string name, string text){
        nameText.text = name;
        dialogText.text = text;
    }

    public void NextDialog(){
        if(dialogList[dialogIndex].type == 1){
            ShowDialog();
        }else if(dialogList[dialogIndex].type == 2){
            SetOption();
        }else{
            dialogIndex = 0;
            Communication.SetActive(false);
        }
    }

    public void SetOption(){
        selectOption = true;
        for(int i=0; i<3;i++){
            option[i].SetActive(true);
            optionText[i].text = dialogList[dialogIndex].content;
            optionList.Add(dialogIndex);
            dialogIndex ++;
            if(dialogList[dialogIndex].type != 2) break;
        }
        Cursor.lockState = CursorLockMode.None; 
    }

    public void SelectOption(){
        optionList.Clear();
        selectOption = false;
        CloseOption();
        NextDialog();        
        Cursor.lockState = CursorLockMode.Locked;      
    }

    public void SelectOption1(){
        if(dialogList[optionList[0]].effect == "portal"){            
            StartCoroutine(TransmitActor(optionList[0]));
        }else{
            dialogIndex = dialogList[optionList[0]].next_id;
            SelectOption();   
        }
    }

    public void SelectOption2(){
        if(dialogList[optionList[1]].effect == "portal"){            
            StartCoroutine(TransmitActor(optionList[1]));
        }else{
            dialogIndex = dialogList[optionList[1]].next_id;
            SelectOption();   
        }
    }

    public void SelectOption3(){
        if(dialogList[optionList[2]].effect == "portal"){            
            StartCoroutine(TransmitActor(optionList[2]));
        }else{
            dialogIndex = dialogList[optionList[2]].next_id;
            SelectOption();   
        }    
    }

    public void CloseOption(){
        for(int i=0;i<3;i++){
            option[i].SetActive(false);
        }
    }

    IEnumerator TransmitActor(int optionIndex)
    { 
        loadingController.LoadingFadeIn();
        yield return new WaitForSeconds(1);  
        GameObject.FindObjectOfType<ActorController>().Transmit(int.Parse(dialogList[optionIndex].target));
        loadingController.LoadingFadeOut();

        dialogIndex = dialogList[optionList[0]].next_id;
        SelectOption(); 
    }
}
