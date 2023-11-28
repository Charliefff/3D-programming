using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUI : BattleBase
{
    protected List<int> PlayerStateList = new List<int>();
    protected List<int> MonsterStateList = new List<int>();
    private List<int> previousPlayerStateList;
    private List<int> previousMonsterStateList;

    void Start()
    {
        
        InitializeStateList(PlayersList, PlayerStateList);
        InitializeStateList(MonstersList, MonsterStateList);

        previousPlayerStateList = new List<int>(PlayerStateList);
        previousMonsterStateList = new List<int>(MonsterStateList);
    }

    public void Update()
    {
        UpdateState(PlayersList, PlayerStateList);
        UpdateState(MonstersList, MonsterStateList);

        if (!AreListsEqual(PlayerStateList, previousPlayerStateList))
        {
            Debug.Log("Player State Changed: " + PlayerStateList);
            previousPlayerStateList = new List<int>(PlayerStateList); 
        }

        if (!AreListsEqual(MonsterStateList, previousMonsterStateList))
        {
            Debug.Log("Monster State Changed: " + MonsterStateList);
            previousMonsterStateList = new List<int>(MonsterStateList);
        }
    }

    
    private bool AreListsEqual(List<int> list1, List<int> list2)
    {
        if (list1.Count != list2.Count)
        {
            return false;
        }
        for (int i = 0; i < list1.Count; i++)
        {
            if (list1[i] != list2[i])
            {
                return false;
            }
        }
        return true;
    }

    void InitializeStateList(List<GameObject> targets, List<int> stateList)
    {
        foreach (var target in targets)
        {
            if (target != null)
            {
                GameObject targetChild = target.transform.GetChild(0).gameObject;
                stateList.Add(targetChild.GetComponent<CharAbility>().HP);
            }
            else
            {
                stateList.Add(0);
            }
        }
    }

    void UpdateState(List<GameObject> target, List<int> HPList)
    {
        for (int i = 0; i < target.Count; i++)
        {
            if (target[i] != null)
            {
                GameObject targetChild = target[i].transform.GetChild(0).gameObject;
                HPList[i] = targetChild.GetComponent<CharAbility>().HP;
            }
            else
            {
                HPList[i] = 0;
            }
        }
    }

}
