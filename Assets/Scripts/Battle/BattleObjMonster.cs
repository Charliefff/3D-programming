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

    public GameObject Victory;
    public GameObject End;

    private List<int> chosenIndices = new List<int>();
    private bool hasChosenAll = false;
    private int oldHP;


    public void Start()
    {
        oldHP = transform.GetComponent<CharAbility>().HP;
    }

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

        CheckState();
    }

 

    private void ChooseTarget()
    {
        string tag = transform.parent.tag;
        if (tag == "Enemy" && PlayersList.Count > 0)
        {
            CheckHP();
            int randomIndex = GetRandomIndex();
            Target = PlayersList[randomIndex];

            if (chosenIndices.Count >= PlayersList.Count)
            {
                hasChosenAll = true;
                StartCoroutine(EndBattleAfterDelay(3f));
            }
        }
    }

    private void CheckHP()
    {
        GameObject temptarget;
        for (int i = 0; i < PlayersList.Count; i++)
        {
            temptarget = PlayersList[i];
            if (temptarget.transform.GetChild(0).gameObject.GetComponent<CharAbility>().HP <= 0)
            {
                chosenIndices.Add(i);
            }
        }

    }
    
    private int GetRandomIndex()
    {
        int randomIndex = Random.Range(0, PlayersList.Count);
        while (chosenIndices.Contains(randomIndex))
        {
            randomIndex = Random.Range(0, PlayersList.Count);
        }
        return randomIndex;
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
        //if (TargetChild.GetComponent<CharAbility>().HP <= 0)
        //{
        //    Destroy(Target, 3f);
        //}
    }
    private IEnumerator EndBattleAfterDelay(float delay)
    {
        end = false;
        yield return new WaitForSeconds(delay);
        if (hasChosenAll)
        {
            GameOver();
        }
        else
        {
            
            EndBattle(); // ¤T¬í«á°õ¦æ EndBattle
        }
    }

    private void EndBattle()
    {

        Victory.SetActive(true);
        Battle_music.SetActive(false);
        Victory_music.SetActive(true);

        GameObject ExBar1 = Victory.transform.Find("Ability1").gameObject;
        GameObject ExBar2 = Victory.transform.Find("Ability2").gameObject;
        GameObject ExBar3 = Victory.transform.Find("Ability3").gameObject;
        GameObject ExBar4 = Victory.transform.Find("Ability4").gameObject;


    }

    private void GameOver()
    {
        End.SetActive(true);
        Battle_music.SetActive(false);
        Gameover_music.SetActive(true);
    }

    private void CheckState()
    {
        if (transform.GetComponent<CharAbility>().HP <= 0)
        {
            ani.SetFloat("HP", 0);
            StartCoroutine(EndBattleAfterDelay(3f));


        }

        if (oldHP != transform.GetComponent<CharAbility>().HP && transform.GetComponent<CharAbility>().HP > 0)
        {
            ani.SetBool("Pain", true);
            oldHP = transform.GetComponent<CharAbility>().HP;
        }
        else
        {
            ani.SetBool("Pain", false);

        }

        if (hasChosenAll)
        {
            ani.SetBool("Victory", true);
        }
    }
}




