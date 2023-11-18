using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class BattleControl : BattleBase
{

    private CharacterStats characterStats;
    private bool temp;
    private new string name;
    private float moveSpeed = 1.4f;
    public void Awake()
    {

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
            Debug.Log("Enter");
            MoveForward();
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
        else
        {

        }

    }

    private void MoveForward()
    {
        ani.SetFloat("forward", 1.5f * Mathf.Lerp(ani.GetFloat("forward"), (2.0f), 0.5f));

        
    }

}
