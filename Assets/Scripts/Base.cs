using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    // Start is called before the first frame update
    public static Ability[] player = new Ability[4];
    public static Dictionary<int,int> ItemDic = new Dictionary<int, int>(); //uid,quantity 

    public static Vector3 playerVec;
    public static int enemyID;
    public static string sceneName;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        playerVec = new Vector3(0,0,0);

        for (int i = 0; i < player.Length; i++) player[i] = new Ability(); 

        player[1].MaxBlood = 100;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void SaveData()
    {

    }

    public static void LoadData()
    {
        
    }
}
