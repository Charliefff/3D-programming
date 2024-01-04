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
    private GameObject moveobj;
    private Animator Ani;
    private bool previousStateIsAttack = false;
    private bool MonsterAttack = true;
    public bool Playerattack;
    public string skill;
    
    private void Start()
    {
        

        if (CurrentIndex == 0)
        {
            moveobj = ObjList[CurrentIndex];
            GameObject moveChild = moveobj.transform.GetChild(0).gameObject;
            if (moveChild.transform.Find("Portal green"))
            {

                Transform portalGreen = moveChild.transform.Find("Portal green");
                if (portalGreen != null)
                {
                    portalGreen.gameObject.SetActive(true);
                }

            }
        }
    }
    private void Update()
    {


        if (moveobj.name == "Monster")  
        {
            Ani = moveobj.transform.GetChild(0).GetComponent<Animator>();
            var stateInfo = Ani.GetCurrentAnimatorStateInfo(0);

            if (stateInfo.IsName("Idle") && MonsterAttack)
            {
                Attack();
                MonsterAttack = false;
            }
        }
        if (Playerattack)
        {
            Attack();
            MonsterAttack = true;
            Playerattack = false;
        }

        Updateportal();

    }


    private void Attack() {
        Movable();
        GameObject moveChild = moveobj.transform.GetChild(0).gameObject;
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
        Ani = moveobj.transform.GetChild(0).GetComponent<Animator>();
        var stateInfo = Ani.GetCurrentAnimatorStateInfo(0);

        // 檢查當前狀態是否為 idle 且前一狀態為 attack
        if (stateInfo.IsName("Idle") && previousStateIsAttack)
        {
            CurrentIndex += 1;
            if (CurrentIndex >= ObjList.Count)
            {
                CurrentIndex = 0;
            }
            //判斷死亡
            if (ObjList[CurrentIndex].transform.GetChild(0).GetComponent<CharAbility>().HP <= 0)
            {
                CurrentIndex += 1;
                if (CurrentIndex >= ObjList.Count)
                {
                    CurrentIndex = 0;
                }
            }
            else
            {
                moveobj = ObjList[CurrentIndex];
                StateObj = moveobj.name;

                GameObject moveChild = moveobj.transform.GetChild(0).gameObject;
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
                
        }
        else if (stateInfo.IsName("Attack"))
        {
            previousStateIsAttack = true;
        }
    }

    private void Movable()
    {
        GameObject ChildObj = moveobj.transform.GetChild(0).gameObject;
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

}
