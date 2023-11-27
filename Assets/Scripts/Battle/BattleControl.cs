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
    private bool canMove;
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
            Target = PlayersList[1];
        }
        if (tag == "Player" && MonstersList.Count > 0)
        {
            int randomIndex = Random.Range(0, MonstersList.Count);
            Target = MonstersList[randomIndex];
        }
    }

    IEnumerator ResetAnimationParameter(string paramName)
    {
        yield return new WaitForSeconds(2f);
        ani.SetBool(paramName, false);
    }

    public void MoveTo()
    {
        if (canMove)
        {
            
            GameObject moveobj = ObjList[CurrentIndex];
            GameObject targetSubObj = Target.transform.GetChild(0).gameObject;
            GameObject moveSubOnj = moveobj.transform.GetChild(0).gameObject;
            string aniName = moveobj.name;
  
            //攻擊腳色特效
            if (moveSubOnj.transform.Find("Portal green"))
            {

                Transform portalGreen = moveSubOnj.transform.Find("Portal green");
                if (portalGreen != null)
                {
                    portalGreen.gameObject.SetActive(true);
                }

            }
            else
                Debug.Log("NotGet ");

            ani.SetBool(aniName, true);

            //改成加入event
            StartCoroutine(ResetAnimationParameter(aniName));
            targetSubObj.GetComponent<CharAbility>().HP -= 60 / 5;
            
            CurrentIndex += 1;
            canMove = false;
            if (CurrentIndex >= ObjList.Count)
                CurrentIndex = 0;

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
            name = "F1";
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
            if (!canMove)
                ChooseTarget();
            canMove = true;
        }
        if (gameObject.GetComponent<CharAbility>().HP <= 0)
        {

            GameObject father_obj;
            father_obj = gameObject.transform.parent.gameObject;
            name = "HP";
            float num = 0f; 
            ani.SetFloat(name, num);
            
            if (father_obj.tag == "Enemy")
            {
                Destroy(father_obj, 4f);
            }
            else
                Destroy(gameObject, 4f);
        }




        MoveTo();
    }


}
