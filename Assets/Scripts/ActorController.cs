using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : MonoBehaviour
{
    public GameObject model;
    public PlayerInput pi;
    public float walkSpeed = 1.4f;
    public float runMultiplier = 2.7f;

    [SerializeField]
    private Animator anim;
    private Rigidbody rigid;
    private Vector3 movingVec;
    // Start is called before the first frame update
    void Awake()
    {
        pi = GetComponent<PlayerInput> ();
        anim = model.GetComponent<Animator>();
        rigid = GetComponent<Rigidbody> ();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("forward",pi.Dmag * ((pi.run)? 2.0f : 1.0f));
        if(pi.Dmag > 0.1f){
            model.transform.forward = pi.Dvec;
        }
        movingVec = pi.Dmag * model.transform.forward * walkSpeed * ((pi.run)? runMultiplier : 1.0f);        
    }

    void FixedUpdate()
    {
        rigid.position += movingVec * Time.fixedDeltaTime;
    }
}
