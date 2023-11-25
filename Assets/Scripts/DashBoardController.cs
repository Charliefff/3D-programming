using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashBoardController : MonoBehaviour
{
    public GameObject DashBoard;
    private UIControler uiController;
    public static bool dashBoardEnable = true;
    // Start is called before the first frame update
    void Start()
    {
        uiController = GetComponent<UIControler>();
        DashBoard.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && dashBoardEnable)
        {            
            if (Time.timeScale == 1.0f)
            {
                Time.timeScale = 0.0f;
                Cursor.lockState = CursorLockMode.None; 
            }
            else
            {
                Time.timeScale = 1.0f;
                Cursor.lockState = CursorLockMode.Locked;
                uiController.Dashboard_reset();
            }

            DashBoard.SetActive(!DashBoard.activeSelf);
        }
    }
}
