using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class BattleObjMonster : BattleBase
{
    public bool buff;
    public bool canMove = false;
    private GameObject Target;
    private bool end = true;


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
        if (gameObject.GetComponent<CharAbility>().HP <= 0 && end)
        {
            ani.SetFloat("HP", 0);
            EndBattle();
        }
    }

    private void ChooseTarget()
    {

        string tag = transform.parent.tag;
        if (tag == "Enemy" && PlayersList.Count > 0)
        {
            int randomIndex = Random.Range(0, PlayersList.Count);

 
            Target = PlayersList[randomIndex];
            while (Target == null)
            {
                Target = PlayersList[randomIndex];

            }
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

    private void EndBattle()
    {
        end = false;
        //Debug.Log("Battle End");
        GameObject.Find("Loading").GetComponent<LoadingController>().SwitchScene("GrassLandScene",true,true);
    }
}




