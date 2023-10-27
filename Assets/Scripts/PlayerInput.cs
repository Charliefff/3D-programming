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

    public string keyJUp = "i";
    public string keyJDown = "k";
    public string keyJLeft = "j";
    public string keyJRight = "l";


    [Header("==== output signals ====")]
    public float Dup;
    public float Dright;
    public float Dmag;
    public Vector3 Dvec;
    public float Jup;
    public float Jright;

    public bool run;

    [Header("==== others ====")]
    public bool inputEnable = true;
    
    private float targetDup;
    private float targetDright;
    private float velocityDup;
    private float velocityDright;
    private float DupProjection;
    private float DrightProjection;


    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Jup = (Input.GetKey(keyJUp)? 1.0f:0) - (Input.GetKey(keyJDown)?1.0f:0);
        Jright = (Input.GetKey(keyJRight)? 1.0f:0) - (Input.GetKey(keyJLeft)?1.0f:0);

        if(inputEnable){                
            targetDup = (Input.GetKey(keyUP)? 1.0f:0) - (Input.GetKey(keyDown)? 1.0f:0);
            targetDright = (Input.GetKey(keyRight)? 1.0f:0) - (Input.GetKey(keyLeft)? 1.0f:0);
        }else{
            targetDup = 0;
            targetDright = 0;
        }

        Dup = Mathf.SmoothDamp(Dup,targetDup,ref velocityDup,0.1f);
        Dright = Mathf.SmoothDamp(Dright,targetDright,ref velocityDright,0.1f);

        DupProjection = Dup * Mathf.Sqrt(1-(Dright*Dright) * 0.5f);
        DrightProjection = Dright * Mathf.Sqrt(1-(Dup*Dup) * 0.5f);

        Dmag = Mathf.Sqrt((DupProjection * DupProjection) +  (DrightProjection * DrightProjection));
        Dvec = DrightProjection * transform.right + DupProjection * transform.forward;

        run = Input.GetKey(keyA);
    }
}
