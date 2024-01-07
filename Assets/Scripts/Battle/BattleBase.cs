using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using System.Linq;
using static UnityEngine.GraphicsBuffer;
using Unity.VisualScripting;
using UnityEditor.PackageManager;

public class BattleBase : MonoBehaviour
{
    //Base 資訊
    protected int BaseEnemy_ID ;
    protected int BaseEnemy_Level;
    protected string Base_seneriao;
    protected int Base_Ability;
    protected List<int> Player1_ability;
    protected List<int> Player2_ability;
    protected List<int> Player3_ability;
    protected List<int> Player4_ability;
    protected List<string> Player1_skill;
    protected List<string> Player2_skill;
    protected List<string> Player3_skill;
    protected List<string> Player4_skill;
    //protected Dictionary Player1_Ability;

    //動畫
    public Animator ani;



    //戰鬥控制
    protected bool AnimationEnd = false;
    private GameObject[] Monsters;
    private GameObject[] Players;
    private GameObject[] HP;
    private GameObject[] AbilityUI;

    protected static string StateObj;
    protected List<GameObject> HPList;
    protected List<GameObject> MonstersList;
    protected List<GameObject> PlayersList;
    protected List<GameObject> ObjList;
    protected List<GameObject> AbilityUIList;
    //protected List<int> SpeedList = new List<int> {100, Base.player[0].Speed, Base.player[1].Speed, Base.player[2].Speed, Base.player[3].Speed};
    protected List<int> SpeedList = new List<int> { 5, 0, 2, 3, 1 };


    //抓出四隻角色
    protected GameObject Player1 = null;
    protected GameObject Player2 = null;
    protected GameObject Player3 = null;
    protected GameObject Player4 = null;
    protected GameObject Monster = null;



    public bool PlayerAttack;

    //音效
    protected GameObject Battle_music;
    protected GameObject Victory_music;
    protected GameObject Gameover_music;



    public void Awake()
    {

        GetBaseInfo();
        SetMusic();
        SetMonster();
        SetMonster();
        SetPlayer();
        playergameObj();
        SortBySpeed();


    }

    private void SetMusic()
    {
        GameObject Music_parent = GameObject.Find("Music");

        if (Music_parent != null)
        {

            Battle_music = Music_parent.transform.Find("BattleMusic_music").gameObject;
            Victory_music = Music_parent.transform.Find("Victory_music").gameObject;
            Gameover_music = Music_parent.transform.Find("GameOver_music").gameObject;

        }

        Battle_music.SetActive(true);
    }



    protected void UpdatePlayerAttack(bool movable)
    {
        PlayerAttack = movable;
    }

    protected bool GetPlayerAttack()
    {

        Debug.Log(PlayerAttack);
        return PlayerAttack;

    }

    protected enum CharacterStats
    {
        Idle = 0, Walk = 1, Attack = 2, Death = 3, Dance = 4
    }

    private void GetBaseInfo()
    {
        //Base資訊
        Base_seneriao = Base.sceneName;
        BaseEnemy_ID = Base.enemyID;
        //SpeedList = new List<int> { 100, Base.player[0].Speed, Base.player[1].Speed, Base.player[2].Speed, Base.player[3].Speed };


    }

    private void SetMonster()
    {
        string BaseEnemy_Name = "Enemy0" + BaseEnemy_ID;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {

            Transform enemyTransform = enemy.transform;

            // 遍歷敵人遊戲物件的所有子物件
            for (int i = 0; i < enemyTransform.childCount; i++)
            {
                // 獲取子物件的名稱
                string childObjectName = enemyTransform.GetChild(i).name;
                GameObject child_enemy = enemyTransform.GetChild(i).gameObject;

                if (childObjectName != BaseEnemy_Name)
                {
                    child_enemy.SetActive(false);
                }
            }
            
        }

        Monsters = enemies;


    }

    private void SetPlayer()
    {
        HP = GameObject.FindGameObjectsWithTag("HP");
        Players = GameObject.FindGameObjectsWithTag("Player");
        AbilityUI = GameObject.FindGameObjectsWithTag("Ability");


        HPList = new List<GameObject>();
        ObjList = new List<GameObject>();
        MonstersList = new List<GameObject>();
        PlayersList = new List<GameObject>();
        AbilityUIList = new List<GameObject>();

        ani = GetComponent<Animator>();

        for (int i = 0; i < HP.Length; i++)
        {
            HPList.Add(HP[i]);
        }

        for (int i = 0; i < Monsters.Length; i++)
        {
            ObjList.Add(Monsters[i]);
            MonstersList.Add(Monsters[i]);
        }
        foreach (var o in Players)
        {
            ObjList.Add(o);
            PlayersList.Add(o);
        }
        foreach (var o in AbilityUI)
        {
            AbilityUIList.Add(o);
        }
    }

    private void playergameObj()
    {
        string BaseEnemy_Name = "Enemy0" + BaseEnemy_ID;
        //Debug.Log(BaseEnemy_Name);
        for (int i = 0; i < ObjList.Count; i++)
        {
            if (ObjList[i].name == "Player1")
            {
                Player1 = ObjList[i].transform.GetChild(0).gameObject;
            }
            else if (ObjList[i].name == "Player2")
            {
                Player2 = ObjList[i].transform.GetChild(0).gameObject;
            }
            else if (ObjList[i].name == "Player3")
            {
                Player3 = ObjList[i].transform.GetChild(0).gameObject;
            }
            else if (ObjList[i].name == "Player4")
            {
                Player4 = ObjList[i].transform.GetChild(0).gameObject;
            }
            else
            {
                Monster = ObjList[i].transform.Find(BaseEnemy_Name).gameObject;
            }
        }
    }

    //根據速度sort
    private void SortBySpeed()
    {

        ObjList = ObjList.Select((obj, index) => new { obj, speed = SpeedList[index] })
                         .OrderByDescending(x => x.speed)
                         .Select(x => x.obj)
                         .ToList();
    }
}
