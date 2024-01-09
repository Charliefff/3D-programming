using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;

public class CharAbility : MonoBehaviour
{
    // Start is called before the first frame update

    public int Level;
    public int HP;
    public int HPMax;
    public int MP;
    public int MPMax;
    public int Speed;
    public int Defense;
    public int Attack;
    public int Exp;
    public int ExpMax;
    public int Magic;
    public List<string> skill;

    
    public int player_ID;
    private Ability player_Ability;
    private bool is_player = true;
    public bool allow_update_ability = false;

    void Awake()
    {
        check_player();

        if(is_player)
        {
            SetAbility();
        }
        else
        {
            HP = HPMax;
        }



    }

    public void Update()
    {
        UpdateAbility();
        if(HP > HPMax)
        { HP = HPMax; }
    }

    public void check_player()
    {
        string name;

        name = transform.parent.name;
        switch (name)
        {
            case "Player1":
                player_ID = 0;
                player_Ability = Base.player[0];
                break;
            case "Player2":
                player_ID = 1;

                player_Ability = Base.player[1];
                break;
            case "Player3":
                player_ID = 2;

                player_Ability = Base.player[2];
                break;
            case "Player4":
                player_ID = 3;

                player_Ability = Base.player[3];
                break;
            case "Monster":
                is_player = false;
                break;
            default:
                // 可以處理無效名稱的情況
                is_player = false;

                Debug.Log("Error player name");
                break;
        }
        
    }

    private void SetAbility()
    {
        Level = player_Ability.Level;
        HP = player_Ability.HP;
        HPMax = player_Ability.HPMax;
        MP = player_Ability.MP;
        MPMax = player_Ability.MPMax;
        Exp = player_Ability.Exp;
        ExpMax = player_Ability.ExpMax;
        Attack = player_Ability.Attack;
        Defense = player_Ability.Defense;
        Magic = player_Ability.Magic;
        Speed = player_Ability.Speed;
    }

    public void UpdateAbility()
{
    // 確保player_Ability不是null
    if (player_Ability != null)
    {
        Base.player[player_ID].SetAbility("Actor1", Level, HP, HPMax, MP, MPMax, Exp, ExpMax, Attack, Defense, Magic, Speed);
        
        }
}

}
