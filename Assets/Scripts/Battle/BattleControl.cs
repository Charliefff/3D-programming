using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BattleControl : BattleBase
{
    private int CurrentIndex;
    private CharacterStats characterStats;
    private bool temp;
    private new string name;
    private Vector3 tempPos;
    
    private Vector3 targetPos;
    private bool CanMove;
    private GameObject Target;
    

    private void ChooseTarget()
    {
        GameObject moveobj = ObjList[CurrentIndex];
        string tag = moveobj.tag;
        tempPos = moveobj.transform.position;
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
        targetPos = Target.transform.position;
    }


    public void MoveTo()
    {
        if ( CanMove)
        {
            GameObject moveobj = ObjList[CurrentIndex];
            float Distance = 2f;
            
            
      
            if (Target != null && Vector3.Distance(moveobj.transform.position, targetPos) > Distance)
            {
                
                targetPos.y = moveobj.transform.position.y;
                moveobj.transform.LookAt(targetPos);
                Vector3 direction = (Target.transform.position - moveobj.transform.position).normalized;
                float speed = 1f;
                moveobj.transform.Translate(Vector3.forward * Time.deltaTime * speed);
                
            }
            else
            {
                
                CanMove = false;
                targetPos = tempPos;
                if (Target != null && Vector3.Distance(moveobj.transform.position, targetPos) > Distance)
                {
                    
                    CanMove = true;
                }
                else
                {
                    CanMove = false;
                    moveobj.transform.position = tempPos;
                    CurrentIndex++;

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

        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
        {
            if (!CanMove)
                ChooseTarget();
            CanMove = true;
        }
        if (HP == 0)
        {
            characterStats = CharacterStats.Death;

        }


        if (characterStats == CharacterStats.Death)
        {
            name = "HP";
            ani.SetInteger(name, 0);
        }

        MoveTo();
    }

}
