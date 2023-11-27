using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingController : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadingFadeIn(){
        animator.SetBool("fadeIn",true);
    }

    public void LoadingFadeOut(){
        animator.SetBool("fadeIn",false);
    }

    public void SwitchScene(string sceneName, bool mouseLock = true){
        StartCoroutine(SwitchSceneIEnumerator(sceneName, mouseLock));
    }

    IEnumerator SwitchSceneIEnumerator(string sceneName, bool mouseLock = true)
    { 
        LoadingFadeIn();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneName);

        if(mouseLock){
            Cursor.lockState = CursorLockMode.Locked;
        }else{
            Cursor.lockState = CursorLockMode.None; 
        }

        LoadingFadeOut();
    }
}
