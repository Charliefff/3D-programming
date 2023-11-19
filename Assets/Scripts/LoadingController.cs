using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
