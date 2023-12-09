using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadingController : MonoBehaviour
{
    public Animator animator;
    private bool mouseLock;
    private bool transmitPlayer;
    private bool shouldTriggerOnSceneLoaded = false;
    private string goalScene;

    void Start()
    {
        goalScene = "";
    }

    void Update()
    {
        
    }

    void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    public void LoadingFadeIn(){
        animator.SetBool("fadeIn",true);
    }

    public void LoadingFadeOut(){
        animator.SetBool("fadeIn",false);
    }

    public void SwitchScene(string sceneName, bool mouseLock = true, bool transmitPlayer = false){
        goalScene = sceneName;
        this.mouseLock = mouseLock;
        this.transmitPlayer = transmitPlayer;
        shouldTriggerOnSceneLoaded = true;
        StartCoroutine(SwitchSceneIEnumerator(sceneName));
    }  

    IEnumerator SwitchSceneIEnumerator(string sceneName, bool mouseLock = true ,bool transmitPlayer = false)
    { 
        LoadingFadeIn();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneName);        
    }

    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        if(shouldTriggerOnSceneLoaded){
            if(transmitPlayer){
                GameObject.FindObjectOfType<ActorController>().Transmit(-1);
            }

            if(mouseLock){
                Cursor.lockState = CursorLockMode.Locked;
            }else{
                Cursor.lockState = CursorLockMode.None; 
            }

            LoadingFadeOut();
            shouldTriggerOnSceneLoaded = false;

            if(goalScene == "TitleScene"){
                Destroy(this.gameObject.transform.parent.gameObject);
            }
        }
    }
}
