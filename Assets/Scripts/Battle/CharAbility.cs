using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharAbility : MonoBehaviour
{
    // Start is called before the first frame update
    public int HP;
    public int MaxHP;
    public int MP;
    public int Speed;
    public int Defense;
    public int Attack;



    //Base copy 到時候接這個
    public static Ability[] player = new Ability[4];
    void Awake()
    {
        Debug.Log(Base.player[0].SkillList);
        HP = MaxHP;
    }




}
