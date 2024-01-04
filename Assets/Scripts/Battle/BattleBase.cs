using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using System.Linq;
using static UnityEngine.GraphicsBuffer;

public class BattleBase : MonoBehaviour
{
    //�ʵe
    public Animator ani;
    protected enum CharacterStats
    {
        Idle = 0, Walk = 1, Attack = 2, Death = 3, Dance = 4
    }


    //�԰�����

    protected bool AnimationEnd = false;
    private GameObject[] Monsters;
    private GameObject[] Players;
    private GameObject[] HP;
    private GameObject[] AbilityUI;

    protected static string StateObj;
    protected static int CurrentIndex;
    protected List<GameObject> HPList;
    protected List<GameObject> MonstersList;
    protected List<GameObject> PlayersList;
    protected List<GameObject> ObjList;
    protected List<GameObject> AbilityUIList;
    protected List<int> SpeedList = new List<int> { 4, 0, 2, 3, 1 };

    //��X�|������
    protected GameObject Player1 = null;
    protected GameObject Player2 = null;
    protected GameObject Player3 = null;
    protected GameObject Player4 = null;
    protected GameObject Monster = null;

    
    
    //protected int HP = 100;

  

    public void Awake()
    {
        
        HP = GameObject.FindGameObjectsWithTag("HP");
        Monsters = GameObject.FindGameObjectsWithTag("Enemy");
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
        foreach (var o in Monsters)
        {
            ObjList.Add(o);
            MonstersList.Add(o);
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

        playergameObj();
        SortBySpeed();

    }

    private void playergameObj()
    {
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
                Monster = ObjList[i].transform.GetChild(0).gameObject;
            }
        }
    }

    //�ھڳt��sort
    private void SortBySpeed()
    {
        
        ObjList = ObjList.Select((obj, index) => new { obj, speed = SpeedList[index] })
                         .OrderByDescending(x => x.speed)
                         .Select(x => x.obj)
                         .ToList();
    }



}
