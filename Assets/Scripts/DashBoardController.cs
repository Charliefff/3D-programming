using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class DashBoardController : MonoBehaviour
{
    public GameObject DashBoard;
    private DashBoardNormalController dashBoardNormalController;
    private DashBoardItemController dashBoardItemController;
    private DashBoardEquipmentController dashBoardEquipmentController;
    private DashBoardSkillController dashBoardSkillController;
    private DashBoardSaveController dashBoardSaveController;
    private DashBoardLoadController dashBoardLoadController;
    public static bool dashBoardEnable = true;
    public GameObject[] DashBoardPage = new GameObject[7];
    private int Dashboard_index;
    private EventSystem eventSystem;

    
    // Start is called before the first frame update
    void Start()
    {
        dashBoardNormalController = DashBoard.GetComponent<DashBoardNormalController>();
        dashBoardItemController = DashBoard.GetComponent<DashBoardItemController>();
        dashBoardEquipmentController = DashBoard.GetComponent<DashBoardEquipmentController>();
        dashBoardSkillController = DashBoard.GetComponent<DashBoardSkillController>();
        dashBoardSaveController = DashBoard.GetComponent<DashBoardSaveController>();
        dashBoardLoadController = DashBoard.GetComponent<DashBoardLoadController>();

        
        DashBoard.SetActive(false);
        Dashboard_index = 0;
        eventSystem = EventSystem.current;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && dashBoardEnable)
        { 
            DashBoard_switch();
        }
    }

    public void DashBoard_switch(){
        DashBoard.SetActive(!DashBoard.activeSelf);

        if (Time.timeScale == 1.0f)
        {
            Time.timeScale = 0.0f;
            Cursor.lockState = CursorLockMode.None; 
            Dashboard_reset();
        }
        else
        {
            Time.timeScale = 1.0f;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    public void Dashboard_reset(){
        for (int i = 1; i < DashBoardPage.Length; i++)
        {
            DashBoardPage[i].SetActive(false);
        }
        DashBoardPage[0].SetActive(true);
        Dashboard_index = 0;

        eventSystem.SetSelectedGameObject(null);

        dashBoardNormalController.UpdateNormal();
        dashBoardItemController.UpdateItem();
        dashBoardEquipmentController.UpdateEquipment();  
    }

    public void Dashboard_state(){
        DashBoardPage[Dashboard_index].SetActive(false);        
        DashBoardPage[0].SetActive(true);
        Dashboard_index = 0;    
        dashBoardNormalController.UpdateNormal();
    }
    public void Dashboard_item(){
        DashBoardPage[Dashboard_index].SetActive(false);        
        DashBoardPage[1].SetActive(true);
        Dashboard_index = 1;     
        dashBoardItemController.UpdateItem();
    }
    public void Dashboard_equpiment(){
        DashBoardPage[Dashboard_index].SetActive(false);        
        DashBoardPage[2].SetActive(true);
        Dashboard_index = 2;  
        dashBoardEquipmentController.UpdateEquipment();  
    }
    public void Dashboard_ability(){
        DashBoardPage[Dashboard_index].SetActive(false);        
        DashBoardPage[3].SetActive(true);
        Dashboard_index = 3;
        dashBoardSkillController.UpdateSkill();    
    }
    public void Dashboard_save(){
        DashBoardPage[Dashboard_index].SetActive(false);        
        DashBoardPage[4].SetActive(true);
        Dashboard_index = 4; 
        dashBoardSaveController.UpdateSave();   
    }
    public void Dashboard_load(){
        DashBoardPage[Dashboard_index].SetActive(false);        
        DashBoardPage[5].SetActive(true);
        Dashboard_index = 5;
        dashBoardLoadController.UpdateLoad();    
    }
    public void Dashboard_system(){
        DashBoardPage[Dashboard_index].SetActive(false);        
        DashBoardPage[6].SetActive(true);
        Dashboard_index = 6;    
    }
    public void Dashboard_back(){
        SceneManager.LoadScene("TitleSceneTest");
    }
}
