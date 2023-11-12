using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Testnemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //print(Base.player[1].MaxBlood);
    }

    void OnTriggerEnter(Collider otherObject)
    {
        if(otherObject.name == "PlayerHandle")
        {
            Base.sceneName = SceneManager.GetActiveScene().name;
            Base.enemyID = 1;
            Base.playerVec = GameObject.Find("PlayerHandle").GetComponent<Transform>().position;
            
            Cursor.lockState = CursorLockMode.None; 
            SceneManager.LoadScene("BattleScene");
        }
    }

}
