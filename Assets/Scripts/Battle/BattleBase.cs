using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using System.Linq;

public class BattleBase : MonoBehaviour
{
    //動畫
    public Animator ani;
    protected enum CharacterStats
    {
        Idle = 0, Walk = 1, Attack = 2, Death = 3, Dance = 4
    }


    //戰鬥控制
    
    
    private GameObject[] Monsters;
    private GameObject[] Players;
    //private void Movement
    //{
        
    //}
    protected List<GameObject> MonstersList;
    protected List<GameObject> PlayersList;
    protected List<GameObject> ObjList;
    protected List<int> SpeedList = new List<int> { 4, 0, 2, 3, 1 };
    
    //protected int HP = 100;

  

    public void Start()
    {
        
        Monsters = GameObject.FindGameObjectsWithTag("Enemy");
        Players = GameObject.FindGameObjectsWithTag("Player");
        ObjList = new List<GameObject>();
        MonstersList = new List<GameObject>();
        PlayersList = new List<GameObject>();

        ani = GetComponent<Animator>();

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
        SortBySpeed();
        
    }

    public enum player
    {
        
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
