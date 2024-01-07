using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCController : MonoBehaviour
{
    public List<Dialog> dialog;
    public GameObject communication;
    public bool onetime = false;
    private DialogController dialogController;
    private bool isPlayerNear = false;
    private Image eKeyImage;

    // Update is called once per frame
    void Start()
    {
        dialogController = communication.GetComponent<DialogController>();
        eKeyImage = GameObject.Find("EKey").GetComponent<Image>();
    }
    void Update()
    {
        if(!communication.activeSelf){
            if(!PlayerInput.inputEnable || !PlayerInput.mouseEnable || !DashBoardController.dashBoardEnable)
            {
                PlayerInput.inputEnable = true;
                PlayerInput.mouseEnable = true;
                DashBoardController.dashBoardEnable = true;
            }
            
            
            if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
            {
                PlayerInput.inputEnable = false;
                PlayerInput.mouseEnable = false;
                DashBoardController.dashBoardEnable = false;
                if (eKeyImage != null)
                {
                    eKeyImage.color = new Color(eKeyImage.color.r, eKeyImage.color.g, eKeyImage.color.b, 0f);
                }
                ShowDialog();
                if(onetime){
                    Destroy(this.gameObject);
                }
            }
        }        
    }

    public void ShowDialog(){
        communication.SetActive(true);
        dialogController.dialogList = dialog;
        dialogController.dialogIndex = 0;
        dialogController.ShowDialog();
    }

    void OnTriggerStay(Collider other){
        if (other.name == "PlayerHandle"){
            if (eKeyImage != null && PlayerInput.inputEnable)
            {
                eKeyImage.color = new Color(eKeyImage.color.r, eKeyImage.color.g, eKeyImage.color.b, 1f);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "PlayerHandle")
        {
            isPlayerNear = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.name == "PlayerHandle")
        {
            isPlayerNear = false;

            if (eKeyImage != null && PlayerInput.inputEnable)
            {
                eKeyImage.color = new Color(eKeyImage.color.r, eKeyImage.color.g, eKeyImage.color.b, 0f);
            }
        }
    }
}
