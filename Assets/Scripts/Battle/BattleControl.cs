using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class BattleControl : BattleBase
{
    private int CurrentIndex = 0;
    private CharacterStats characterStats;
    private bool temp;
    private new string name;
    private Vector3 tempPos;
    private Quaternion tempRot;
    private Vector3 targetPos;
    private bool CanMove;
    private GameObject Target;
    private Quaternion initialRotation;

    private void ChooseTarget()
    {
        GameObject moveobj = ObjList[CurrentIndex];
        string tag = moveobj.tag;
        tempPos = moveobj.transform.position;
        initialRotation = moveobj.transform.rotation;
        if (tag == "Enemy" && PlayersList.Count > 0)
        {
            Target = PlayersList[2];
        }
        if (tag == "Player" && MonstersList.Count > 0)
        {
            int randomIndex = Random.Range(0, MonstersList.Count);
            Target = MonstersList[randomIndex];
        }
        targetPos = Target.transform.position;
        targetPos.y = moveobj.transform.position.y;
    }


    public void MoveTo()
    {
        if (CanMove)
        {

            GameObject moveobj = ObjList[CurrentIndex];
            GameObject SubObj = Target.transform.GetChild(0).gameObject;
            float Distance = 1f;
            float rotationSpeed = 3f;
            if (Target != null && Vector3.Distance(moveobj.transform.position, targetPos) > Distance)
            {
                Vector3 direction = (targetPos - moveobj.transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                moveobj.transform.rotation = Quaternion.Slerp(moveobj.transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
                float speed = 1f;
                moveobj.transform.Translate(Vector3.forward * Time.deltaTime * speed);
            }
            else
            {
                targetPos = tempPos;
                targetPos.y = moveobj.transform.position.y;
                if (Target != null && Vector3.Distance(moveobj.transform.position, targetPos) > Distance)
                {

                    //要改道物件下面
                    SubObj.GetComponent<CharAbility>().HP -= 50/5;
                    CanMove = true;
                }
                else
                {
                    moveobj.transform.position = tempPos;
                    moveobj.transform.rotation = initialRotation;
                    CurrentIndex += 1;
                    CanMove = false;
                    if (CurrentIndex >=ObjList.Count)
                        CurrentIndex = 0;
                }
            }
        }
    }


    public void Awake()
    {
        ani = GetComponent<Animator>();
    }
    public void Update()
    {

        if (Input.GetKeyDown(KeyCode.F1))
        {
            name = "SillyDance";
            temp = ani.GetBool(name);
            ani.SetBool(name, !temp);

        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            name = "F2";
            temp = ani.GetBool(name);
            ani.SetBool(name, !temp);
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {

        }
        if(Input.GetKeyDown(KeyCode.F4))
        {
            name = "F4";
            temp = ani.GetBool(name);
            ani.SetBool(name, !temp);
        }
        if(Input.GetKeyDown(KeyCode.F5))
        {

        }
        if( Input.GetKeyDown(KeyCode.F6))
        {
            name = "F6";
            temp = ani.GetBool(name);
            ani.SetBool(name , !temp);
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
        {
            if (!CanMove)
                ChooseTarget();
            CanMove = true;
        }
        if (gameObject.GetComponent<CharAbility>().HP == 0)
        {
            name = "HP";
            float num = 0f; 
            ani.SetFloat(name, num);
        }




        MoveTo();
    }


}
