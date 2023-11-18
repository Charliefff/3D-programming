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

    public void Awake()
    {
        ani.SetBool("SillyDance", false);


    }
    public void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.F1))
        {
            name = "SillyDance";
            temp = ani.GetBool(name);
            ani.SetBool(name, !temp);
        }

        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            Attack();
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
}
