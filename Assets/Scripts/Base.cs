using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text.Json;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;
using System;


public class Base : MonoBehaviour
{
    public static Ability[] player = new Ability[4];
    public static Dictionary<string, int> bagConsumable = new Dictionary<string, int>();
    public static Dictionary<string, int> bagWeapons = new Dictionary<string, int>();
    public static Dictionary<string, SaveData> saveDataDictionary = new Dictionary<string, SaveData>();

    public static int money;
    public static Vector3 playerVec;
    public static int enemyID;
    public static string sceneName;
    public static string Bagdatapath = "../Json/Bagjson";

    public static Dictionary<string, Consumable> dicConsumable;
    public static Dictionary<string, Weapon> dicWeapon;
    public static Dictionary<string, Ability> dicAbility;
    public static Dictionary<string, Enemy> dicEnemy;
    public static Dictionary<string, Skill> dicSkill;
    public static Dictionary<string, State> dicState;


    private SerializableDictionary<string, Consumable> consumables = new();
    private SerializableDictionary<string, Weapon> weapons = new();
    private SerializableDictionary<string, Ability> abilities = new();
    private SerializableDictionary<string, Enemy> enemies = new();
    private SerializableDictionary<string, Skill> skills = new();
    private SerializableDictionary<string, State> states = new();
    //public SerializableDictionary<string, Monster>
    private SerializableDictionary<string, SaveData> saveData = new();


    private string consumables_path = "../Json/consumable.json";
    private string weapons_path = "../Json/weapon.json";
    private string abilities_path = "";
    private string enemies_path = "../Json/enemy.json";
    private string skills_path = "../Json/skill.json";
    private string states_path = "../Json/state.json";

    private float timeCount;

    void Awake(){
        DataLoader();
        dicConsumable = consumables.ToDictionary();
        dicWeapon = weapons.ToDictionary();
        dicAbility = abilities.ToDictionary();
        dicEnemy = enemies.ToDictionary();
        dicSkill = skills.ToDictionary();
        dicState = states.ToDictionary();

        for(int i=0;i<4;i++){
            player[i] = new Ability();
        }
        
        GameInitialization();   
    }
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        playerVec = new Vector3(0,0,0);
        timeCount = Time.time;       
    }

    void Update()
    {
        if(Time.time - timeCount > 10){
            string temp = SceneManager.GetActiveScene().name;
            if(temp == "GrassLandScene" || temp == "DesertScene" || temp == "DungeonScene"){
                timeCount = Time.time;    
                SaveGame(0);
            }  
        }
    }

    public static void SaveGame(int index)
    {
        SaveData temp = new SaveData();
        temp.SceneName = SceneManager.GetActiveScene().name;

        if(temp.SceneName == "GrassLandScene" || temp.SceneName == "DesertScene" || temp.SceneName == "DungeonScene"){
            temp.Money = money;
            temp.BagConsumable = bagConsumable;
            temp.BagWeapons = bagWeapons;

            DateTime now = DateTime.Now;
            temp.Day = now.ToString("yyyy/MM/dd HH:mm:ss");

            playerVec = GameObject.Find("PlayerHandle").GetComponent<Transform>().position;
            temp.PlayerPosX = playerVec.x;
            temp.PlayerPosY = playerVec.y;
            temp.PlayerPosZ = playerVec.z;
            
            for(int i=0;i<4;i++){
                temp.Player[i] = player[i];
            }

            saveDataDictionary["" + index] = temp;

            string jsonOutput = JsonConvert.SerializeObject(saveDataDictionary, Formatting.Indented);
            File.WriteAllText(Application.dataPath + "/Json/Data.json", jsonOutput);        
        }        
    }

    public static void LoadGame(int index)
    {
        money = saveDataDictionary["" + index].Money;
        for(int i=0; i<4; i++){
            player[i] = saveDataDictionary["" + index].Player[i];
        }

        sceneName = saveDataDictionary["" + index].SceneName;
        playerVec.x = saveDataDictionary["" + index].PlayerPosX;
        playerVec.y = saveDataDictionary["" + index].PlayerPosY+0.1f;
        playerVec.z = saveDataDictionary["" + index].PlayerPosZ;

        GameObject.Find("Loading").GetComponent<LoadingController>().SwitchScene(sceneName,true,true);
    }

    public static void LoadGameDic(){
        string jsonContent = File.ReadAllText(Application.dataPath + "/Json/Data.json");   
        saveDataDictionary = JsonConvert.DeserializeObject<Dictionary<string, SaveData>>(jsonContent);
    }

    private void GameInitialization(){
        LoadGameDic();

        money = 10000;
        player[0].SetAbility("紅緋·影月", 2, 20, 20, 8, 50, 8, 10, 50, 10, 20, 30);
        player[1].SetAbility("暗影·銀風", 5, 52, 120, 20, 50, 10, 17, 50, 10, 20, 30);
        player[2].SetAbility("金煌·雷雅", 3, 70, 70, 40, 50, 3, 10, 50, 10, 20, 30);
        player[3].SetAbility("赤炎·鐵鬚", 7, 89, 170, 30, 50, 7, 25, 50, 10, 20, 30);

        bagConsumable["1"] = 3;
        bagConsumable["2"] = 5;
        bagConsumable["4"] = 2;
        bagConsumable["15"] = 7;
        bagConsumable["7"] = 1;
        bagConsumable["14"] = 8;
        bagConsumable["16"] = 10;
        bagConsumable["13"] = 22;
        bagConsumable["3"] = 1;
        bagConsumable["5"] = 10;

        bagWeapons["1"] = 1;
        bagWeapons["2"] = 1;
        bagWeapons["3"] = 1;
        bagWeapons["4"] = 1;
        bagWeapons["8"] = 1;
        bagWeapons["10"] = 1;
        bagWeapons["15"] = 1;
        bagWeapons["16"] = 1;
        bagWeapons["21"] = 1;

        player[0].WeaponList.Add("1");

        player[0].SkillList.Add("1");
        player[0].SkillList.Add("2");
        player[0].SkillHistoryList.Add("3");
        player[0].SkillHistoryList.Add("4");
        player[0].SkillHistoryList.Add("5");
        player[0].SkillHistoryList.Add("6");
        player[0].SkillHistoryList.Add("7");
        player[0].SkillHistoryList.Add("8");
    }
    
    private void OnApplicationQuit()
    {
        SaveGame(0);
    }

    private void DataLoader()
    {
        consumables = LoadJsonData<Consumable>(consumables_path);
        weapons = LoadJsonData<Weapon>(weapons_path);
        enemies = LoadJsonData<Enemy>(enemies_path);
        skills = LoadJsonData<Skill>(skills_path);
        states = LoadJsonData<State>(states_path);

    }

    private SerializableDictionary<string, T> LoadJsonData<T>(string fileName)
    {
        string path = Path.Combine(Application.streamingAssetsPath, fileName);
        string jsonContent = File.ReadAllText(path);
        var loader = new JsonLoader<T>();
        return loader.LoadFromJson(jsonContent);
    }
}
public class JsonLoader<T>
{
    public SerializableDictionary<string, T> LoadFromJson(string jsonContent)
    {
       
        Dictionary<string, T> loadedData = JsonConvert.DeserializeObject<Dictionary<string, T>>(jsonContent);
        var serializableDictionary = new SerializableDictionary<string, T>();

        foreach (var entry in loadedData)
        {
            serializableDictionary.keys.Add(entry.Key);
            serializableDictionary.values.Add(entry.Value);
        }

        return serializableDictionary;
    }
}

[System.Serializable]
public class SerializableDictionary<K, V>
{
    public List<K> keys = new();
    public List<V> values = new();

    public Dictionary<K, V> ToDictionary()
    {
        var dictionary = new Dictionary<K, V>();
        for (int i = 0; i < Mathf.Min(keys.Count, values.Count); i++)
        {
            dictionary[keys[i]] = values[i];
        }
        return dictionary;
    }
}