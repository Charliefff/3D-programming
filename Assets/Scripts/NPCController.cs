using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public List<Dialog> dialog;
    public GameObject communication;
    private DialogController dialogController;
    private PlayerInput playerInput;
    private bool isPlayerNear = false;

    // Update is called once per frame
    void Start()
    {
        dialogController = communication.GetComponent<DialogController>();
        playerInput = FindObjectOfType<PlayerInput>();
    }
    void Update()
    {
        if(!communication.activeSelf){
            if(!playerInput.inputEnable)
            {
                playerInput.inputEnable = true;
            }
            
            
            if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
            {
                playerInput.inputEnable = false;
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
