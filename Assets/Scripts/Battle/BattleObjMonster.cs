using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BattleObjMonster : BattleBase
{
    public bool buff;
    public bool canMove = false;
    private GameObject Target;
    


    public void Update()
    {
        
        AnimatorStateInfo animation;
        animation = ani.GetCurrentAnimatorStateInfo(0);
        if (canMove)
        {
            MoveTo();
        }
        if (!animation.IsName("Idle"))
        {
            ani.SetBool("Attack", false);
        }
        if (gameObject.GetComponent<CharAbility>().HP == 0)
        {
            ani.SetFloat("HP", 0);

        }
    }

    private void ChooseTarget()
    {

        string tag = transform.parent.tag;
        if (tag == "Enemy" && PlayersList.Count > 0)
        {
            int randomIndex = Random.Range(0, PlayersList.Count);

            Target = PlayersList[randomIndex];
        }
        if (tag == "Player" && MonstersList.Count > 0)
        {
            int randomIndex = Random.Range(0, MonstersList.Count);
            Target = MonstersList[randomIndex];
        }
    }

    public void MoveTo()
    {
        
        string tag = transform.parent.tag;
        ChooseTarget();
        if (Target == null)
        {
            Debug.Log(PlayersList);
            Debug.LogError("Target is null");
        }
        else
        {
            GameObject targetSubObj = Target.transform.GetChild(0).gameObject;
        }


        ani.SetBool("Attack", true);

        Attack();


        canMove = false;
    }

    private void Attack()
    {
        GameObject TargetChild;
        TargetChild = Target.transform.GetChild(0).gameObject;
        TargetChild.GetComponent<CharAbility>().HP -= 50;
        if (TargetChild.GetComponent<CharAbility>().HP == 0)
        {
            Destroy(Target, 3f);
        }
         
        
    }



}
