using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleBase : MonoBehaviour
{
    public Animator ani;
    protected GameObject target;
    protected void Attack()
    {
        
    }
    protected int HP;

    protected enum CharacterStats
    {
        Idle = 0, Walk = 1, Attack = 2, Death = 3, Dance = 4
    }
}
