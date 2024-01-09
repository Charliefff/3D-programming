using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.ParticleSystem;

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
    private float _lerpSpeed = 3;

    private bool old_defend = false;
    public GameObject particle;


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
            ani.SetBool("Heal", false);
            ani.SetBool("Doubleatt", false);
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




        Attack();
        canMove = false;
    }

    private void Attack()
    {
        GameObject TargetChild;
        int Def ;
        int Att;
        int randomChance = Random.Range(0, 100);


        if (old_defend)
        {
            transform.GetComponent<CharAbility>().Defense -= 10;
            old_defend = false;
        }

        if (randomChance <= 20 && (transform.GetComponent<CharAbility>().HP < transform.GetComponent<CharAbility>().HPMax))
        {

            ani.SetBool("Heal", true);
            GameObject healParticle = particle.transform.Find("Heal").gameObject;
            healParticle.SetActive(true);
            transform.GetComponent<CharAbility>().HP += (int)Mathf.Round(transform.GetComponent<CharAbility>().HPMax * 0.25f);
            if(transform.GetComponent<CharAbility>().HP > transform.GetComponent<CharAbility>().HPMax)
            {
                transform.GetComponent<CharAbility>().HP = transform.GetComponent<CharAbility>().HPMax;
            }
            StartCoroutine(DisableAfterSeconds(healParticle, 2f));
        }
        else if(randomChance > 20 && randomChance <= 40)
        {
            ani.SetBool("Heal", true);
            GameObject DefParticle = particle.transform.Find("Buff_Def").gameObject;
            DefParticle.SetActive(true);
            transform.GetComponent<CharAbility>().Defense += 10;
            old_defend = true;
            StartCoroutine(DisableAfterSeconds(DefParticle, 2f));
        }
        else if (randomChance > 60 && randomChance <= 65)
        {
            ani.SetBool("Doubleatt", true);
            TargetChild = Target.transform.GetChild(0).gameObject;
            Def = TargetChild.GetComponent<CharAbility>().Defense;
            Att = transform.GetComponent<CharAbility>().Attack;
            TargetChild.GetComponent<CharAbility>().HP -= (3 * Att / Def);
        }
        else
        {
            ani.SetBool("Attack", true);
            TargetChild = Target.transform.GetChild(0).gameObject;
            Def = TargetChild.GetComponent<CharAbility>().Defense;
            Att = transform.GetComponent<CharAbility>().Attack;
            TargetChild.GetComponent<CharAbility>().HP -= (Att / Def);

        }


    }

    private IEnumerator DisableAfterSeconds(GameObject obj, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        obj.SetActive(false);
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

        LevelUp();

    }



    private void GameOver()
    {
        End.SetActive(true);
        Battle_music.SetActive(false);
        Gameover_music.SetActive(true);
    }

    private void LevelUp()
    {
        Base.player[0].Exp += transform.GetComponent<CharAbility>().Exp;
        Base.player[1].Exp += transform.GetComponent<CharAbility>().Exp;
        Base.player[2].Exp += transform.GetComponent<CharAbility>().Exp;
        Base.player[3].Exp += transform.GetComponent<CharAbility>().Exp;

        for (int i = 0; i < Base.player.Length; i++)
        {
            Base.player[i].Exp += transform.GetComponent<CharAbility>().Exp;
            while (Base.player[i].Exp > Base.player[i].ExpMax)
            {

                Base.player[i].Level += 1;
                int LV = Base.player[i].Level;

                Base.player[i].HPMax += (LV * 5);
                Base.player[i].HP = Base.player[i].HPMax;

                Base.player[i].MPMax += (LV * 5);
                Base.player[i].MP = Base.player[i].MPMax;

                Base.player[i].Attack += 1 * i;
                Base.player[i].Defense += 1 * i;
                Base.player[i].Magic += 1 * i;
                Base.player[i].Exp = (Base.player[i].Exp - Base.player[i].ExpMax);
                Base.player[i].ExpMax = (LV * 3);

            }
        }
    }
    private void CheckState()
    {
        if (transform.GetComponent<CharAbility>().HP <= 0)
        {
            ani.SetFloat("HP", 0);
            StartCoroutine(EndBattleAfterDelay(3f));

        }

        if (oldHP > transform.GetComponent<CharAbility>().HP && transform.GetComponent<CharAbility>().HP > 0)
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




