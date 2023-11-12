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
    

    // Start is called before the first frame update
    void Start()
    {
        dialogIndex = 0;
        ShowDialog();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            if(dialogList[dialogIndex].type != 0){
                dialogIndex = dialogList[dialogIndex].next_id;
                ShowDialog();
            }else{
                dialogIndex = 0;
                Communication.SetActive(false);
            }
        }
    }

    public void ShowDialog(){
        if(dialogList[dialogIndex].type <= 1){
            UpdateText(dialogList[dialogIndex].name,dialogList[dialogIndex].content);
        }
    }

    public void UpdateText(string name, string text){
        nameText.text = name;
        dialogText.text = text;
    }
}
