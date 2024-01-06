using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;


public class BattleControl : BattleBase
{
    public string skill;
    public bool Playerattack;

    private GameObject moveobj;
    private Animator Ani;
    private bool previousStateIsAttack = false;
    private bool MonsterAttack = true;
    private string movable_name;

    protected int CurrentIndex = 0;



    private void Start()
    {
        //Debug.Log(Monster.name);
        GameObject moveChild;

        if (CurrentIndex == 0)
        {
            moveobj = ObjList[CurrentIndex];
            Movable_name();
            moveChild = moveobj.transform.Find(movable_name).gameObject;

            Transform portalGreen = moveChild.transform.Find("Portal green");
            if (portalGreen != null)
            {

                portalGreen.gameObject.SetActive(true);
            }
        }
    }

    private void Update()
    {
        if (moveobj.name == "Monster")  
        {
            Movable_name();

            Ani = moveobj.transform.Find(movable_name).GetComponent<Animator>();
            var stateInfo = Ani.GetCurrentAnimatorStateInfo(0);

            if (stateInfo.IsName("Idle") && MonsterAttack)
            {
                Attack();
                MonsterAttack = false;
            }
        }
        if (Playerattack)
        {
            Movable_name();

            Attack();
            MonsterAttack = true;
            Playerattack = false;
        }

        Updateportal();

    }


    private void Attack() {
        Movable();
        
        GameObject moveChild = moveobj.transform.Find(movable_name).gameObject;
        if (moveChild.transform.Find("Portal green"))
        {
            Transform portalGreen = moveChild.transform.Find("Portal green");
            if (portalGreen != null)
            {
                portalGreen.gameObject.SetActive(false);
            }
        }
    }

    //控制腳色轉換
    private void Updateportal()
    {
        int checkgameover = 0;
        //Debug.Log(movable_name);
        Ani = moveobj.transform.Find(movable_name).GetComponent<Animator>();
        var stateInfo = Ani.GetCurrentAnimatorStateInfo(0);

        // 檢查當前狀態是否為 idle 且前一狀態為 attack
        if (stateInfo.IsName("Idle") && previousStateIsAttack)
        {
            CurrentIndex += 1;
            if (CurrentIndex >= ObjList.Count)
            {
                CurrentIndex = 0;
            }

            moveobj = ObjList[CurrentIndex];
            Movable_name();
            while (moveobj.transform.Find(movable_name).GetComponent<CharAbility>().HP <= 0)
            {
                CurrentIndex += 1;
                if (CurrentIndex >= ObjList.Count)
                {
                    CurrentIndex = 0;
                }
                moveobj = ObjList[CurrentIndex];
                Movable_name();
                checkgameover++;
                if (checkgameover > 4)
                {
                    break;
                }

            }
            StateObj = moveobj.name;

            GameObject moveChild = moveobj.transform.Find(movable_name).gameObject;
            if (moveChild.transform.Find("Portal green"))
            {
                Transform portalGreen = moveChild.transform.Find("Portal green");

                if (portalGreen != null)
                {
                    portalGreen.gameObject.SetActive(true);
                }

            }
            previousStateIsAttack = false;

        }
        else if (stateInfo.IsName("Attack"))
        {
            previousStateIsAttack = true;
        }
    }

    private void Movable()
    {
        GameObject ChildObj = moveobj.transform.Find(movable_name).gameObject;
        if (moveobj.name == "Player1")
        {
            BattleObj1 script = ChildObj.GetComponent<BattleObj1>();
            if (script != null)
            {
                script.canMove = true;
            }
        }
        if (moveobj.name == "Player2")
        {
            BattleObj2 script = ChildObj.GetComponent<BattleObj2>();
            if (script != null)
            {
                script.canMove = true;
            }
        }

        if (moveobj.name == "Player3")
        {
            BattleObj3 script = ChildObj.GetComponent<BattleObj3>();
            if (script != null)
            {
                script.canMove = true;
            }
        }
        if (moveobj.name == "Player4")
        {
            BattleObj4 script = ChildObj.GetComponent<BattleObj4>();
            if (script != null)
            {
                script.canMove = true;
            }
        }
        else
        {
            BattleObjMonster script = ChildObj.GetComponent<BattleObjMonster>();
            if (script != null)
            {
                script.canMove = true;
            }
        }
    }   

    public void CanMove()
    {
        Playerattack = true;
    }
    
    public void SkillName(string name)
    {
        skill = name;
        Debug.Log(skill);
    }

    private void Movable_name()
    {
        if (moveobj.name == "Monster")
        {
            movable_name = Monster.name;
        }
        else
        {
            movable_name = moveobj.transform.GetChild(0).gameObject.name;
        }
    }
}
