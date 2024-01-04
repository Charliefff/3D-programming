using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BattleObj4 : BattleBase
{
    public bool buff;
    public bool canMove = false;
    private GameObject Target;
    private string aniName;


    public void Update()
    {
        aniName = transform.parent.name;
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
        if (transform.GetComponent<CharAbility>().HP == 0)
        {
            ani.SetFloat("HP", 0);

        }
    }

    private void ChooseTarget()
    {

        string tag = transform.parent.tag;
        if (tag == "Enemy" && PlayersList.Count > 0)
        {
            Target = PlayersList[1];
        }
        if (tag == "Player" && MonstersList.Count > 0)
        {
            int randomIndex = Random.Range(0, MonstersList.Count);
            Target = MonstersList[randomIndex];
        }
        if (transform.GetComponent<CharAbility>().HP <= 0)
        {
            ani.SetFloat("HP", 0);
        }
    }

    public void MoveTo()
    {

        string tag = transform.parent.tag;
        Debug.Log(tag);
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
        int Def;
        int Att;
        TargetChild = Target.transform.GetChild(0).gameObject;
        Def = TargetChild.GetComponent<CharAbility>().Defense;
        Att = transform.GetComponent<CharAbility>().Attack;
        TargetChild.GetComponent<CharAbility>().HP -= (Att / Def);
        if (TargetChild.GetComponent<CharAbility>().HP <= 0)
        {
            Destroy(Target, 3f);
        }
    }


}
