using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    [Header("==== Key settings ====")]
    public string keyUP = "w";
    public string keyDown = "s";
    public string keyLeft = "a";
    public string keyRight = "d";

    public string keyA = "left shift";
    public string keyB;
    public string keyC;
    public string keyD;

    [Header("==== output signals ====")]
    public float Dup;
    public float Dright;
    public float Dmag;
    public Vector3 Dvec;

    public bool run;

    [Header("==== others ====")]
    public bool inputEnable = true;
    
    private float targetDup;
    private float targetDright;
    private float velocityDup;
    private float velocityDright;


    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(inputEnable){                
            targetDup = (Input.GetKey(keyUP)? 1.0f:0) - (Input.GetKey(keyDown)? 1.0f:0);
            targetDright = (Input.GetKey(keyRight)? 1.0f:0) - (Input.GetKey(keyLeft)? 1.0f:0);
        }else{
            targetDup = 0;
            targetDright = 0;
        }

        Dup = Mathf.SmoothDamp(Dup,targetDup,ref velocityDup,0.1f);
        Dright = Mathf.SmoothDamp(Dright,targetDright,ref velocityDright,0.1f);

        Dmag = Mathf.Sqrt((Dup * Dup) +  (Dright * Dright));
        Dvec = Dright * transform.right + Dup * transform.forward;

        run = Input.GetKey(keyA);
    }
}
