using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    public GameObject enemyParent;
    public GameObject enemy;
    public Animator enemyAni;
    public int enemyID;
    public bool wander = true;
    public string battleScene;


    private float counter = 0f;
    private float waitTime = 0f;
    private float moveDistance;
    private bool moveFlag;
    private LoadingController loadingController;

    private float timeSwitch;

    // Start is called before the first frame update
    void Start()
    {
        loadingController = GameObject.FindObjectOfType<LoadingController>();

        waitTime = Random.Range(1f, 5f);
        counter = 0f; 
        moveFlag = false;

        timeSwitch = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(wander){
             counter += Time.deltaTime;
            enemyParent.transform.position = enemy.transform.position; 
            enemy.transform.position = enemyParent.transform.position; 

            if (!moveFlag && counter >= waitTime)
            {
                float turnAngle = Random.Range(-360, 360);
                enemyParent.transform.Rotate(0, turnAngle, 0, Space.Self);

                moveDistance = Random.Range(0.5f, 4);
                moveFlag = true;
                enemyAni.SetBool("walk", true);
                counter = 0f; 
            }
            
            if (moveFlag)
            {
                if (counter <= moveDistance)
                {
                    enemyParent.transform.Translate(Vector3.forward * Time.deltaTime);
                }
                else
                {
                    moveFlag = false; 
                    waitTime = Random.Range(1f, 5f);
                    counter = 0f;
                    enemyAni.SetBool("walk", false);
                }
            }
        }
       
    }

    void OnTriggerEnter(Collider otherObject)
    {    
        if(otherObject.name == "PlayerHandle")
        {
            if(Time.time - timeSwitch < 0.5){
                Destroy(this.gameObject);
            }else{
                Base.sceneName = SceneManager.GetActiveScene().name;
                Base.enemyID = enemyID;
                Base.playerVec = GameObject.Find("PlayerHandle").GetComponent<Transform>().position;
                
                loadingController.SwitchScene(battleScene, false);
            }            
        }
    }
}
