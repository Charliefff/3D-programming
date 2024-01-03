using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : MonoBehaviour
{
    public GameObject model;
    public GameObject[] modelList = new GameObject[4];

    public PlayerInput pi;
    public float walkSpeed = 1.4f;
    public float runMultiplier = 2.7f;

    public GameObject playerHandle;

    [SerializeField]
    private Animator anim;
    private Rigidbody rigid;
    private Vector3 movingVec;
    

    private List<Vector3> portalPositions = new List<Vector3>
    {
        new Vector3(31.28487f, 6.5f, 131.1901f), 
        new Vector3(46.74676f, 3.5f, 79.3427f), 
        new Vector3(21.55777f, 5.5f, 75.34791f)  
    };
    void Awake()
    {
        pi = GetComponent<PlayerInput> ();
        anim = model.GetComponent<Animator>();
        rigid = GetComponent<Rigidbody> ();
        model = modelList[0];
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("forward",pi.Dmag * Mathf.Lerp(anim.GetFloat("forward"),((pi.run)? 2.0f : 1.0f),0.5f));

        if(pi.Dmag > 0.1f){
            model.transform.forward = Vector3.Slerp(model.transform.forward,pi.Dvec,0.3f);
        }
        movingVec = pi.Dmag * model.transform.forward * walkSpeed * ((pi.run)? runMultiplier : 1.0f);        
    }

    void FixedUpdate()
    {
        rigid.position += movingVec * Time.fixedDeltaTime;
    }

    public void Transmit(int portalPositionsID){
        if(portalPositionsID == -1 ){
            playerHandle.transform.position = Base.playerVec;
        }else{
            playerHandle.transform.position = portalPositions[portalPositionsID];
        }
    }
}
