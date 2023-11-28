using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharAbility : MonoBehaviour
{
    // Start is called before the first frame update
    public int HP;

    //Base copy 到時候接這個
    public static Ability[] player = new Ability[4];
    void Awake()
    {
        for (int i = 0; i < 4; i++)
        {
            player[i] = new Ability();
        }

        player[0].SetAbility("Actor1", 2, 89, 100, 20, 50, 3, 10, 50, 10, 20, 30);
        player[1].SetAbility("Actor2", 2, 89, 100, 20, 50, 3, 10, 50, 10, 20, 30);
        player[2].SetAbility("Actor3", 2, 89, 100, 20, 50, 3, 10, 50, 10, 20, 30);
        player[3].SetAbility("Actor4", 2, 89, 100, 20, 50, 3, 10, 50, 10, 20, 30);
    }




}
