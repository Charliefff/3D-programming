using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public List<Dialog> dialog;
    public GameObject communication;
    private DialogController dialogController;
    private bool isPlayerNear = false;

    // Update is called once per frame
    void Start()
    {
        dialogController = communication.GetComponent<DialogController>();
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
                ShowDialog();
            }
        }        
    }

    void ShowDialog(){
        communication.SetActive(true);
        dialogController.dialogList = dialog;
        dialogController.dialogIndex = 0;
        dialogController.ShowDialog();
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
        }
    }
}
