using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text.Json;


[System.Serializable]
public class SerializableDictionary
{
    public Dictionary<int, int> ItemDic = new Dictionary<int, int>();
}

public class Base : MonoBehaviour
{
    // Start is called before the first frame update
    public static Ability[] player = new Ability[4];
    public static SerializableDictionary serializedItemDic = new SerializableDictionary();


    public static Vector3 playerVec;
    public static int enemyID;
    public static string sceneName;
    public static string Bagdatapath = "Assets/Json/Bagjson";
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
        //Svae Bag info
        string json = JsonUtility.ToJson(serializedItemDic); 
        File.WriteAllText(Bagdatapath, json); 


    }

    public static void LoadData()
    {
        // Load Bag info
        if (File.Exists(Bagdatapath))
        {
            string json = File.ReadAllText(Bagdatapath);
            serializedItemDic = JsonUtility.FromJson<SerializableDictionary>(json);
        }
    }

    
    private void OnApplicationQuit()
    {
        SaveData();
    }
}
