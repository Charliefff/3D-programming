using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text.Json;
using Newtonsoft.Json;

public class Base : MonoBehaviour
{
    public static Ability[] player = new Ability[4];
    public static Dictionary<string, int> bagConsumable = new Dictionary<string, int>();
    public static Dictionary<string, int> bagWeapons = new Dictionary<string, int>();
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

    private string consumables_path = "../Json/consumable.json";
    private string weapons_path = "../Json/weapon.json";
    private string abilities_path = "";
    private string enemies_path = "../Json/enemy.json";
    private string skills_path = "../Json/skill.json";
    private string states_path = "../Json/state.json";
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
        
        player[0].SetAbility("Actor1", 2, 89, 100, 8, 50, 8, 10, 50, 10, 20, 30);
        player[1].SetAbility("Actor2", 5, 52, 120, 20, 50, 10, 17, 50, 10, 20, 30);
        player[2].SetAbility("Actor3", 3, 70, 70, 40, 50, 3, 10, 50, 10, 20, 30);
        player[3].SetAbility("Actor4", 7, 89, 170, 30, 50, 7, 25, 50, 10, 20, 30);

        bagConsumable["1"] = 3;
        bagConsumable["2"] = 5;
        bagConsumable["4"] = 2;
        bagConsumable["15"] = 7;
        bagConsumable["7"] = 1;
        bagConsumable["14"] = 8;
        bagConsumable["16"] = 10;
        bagConsumable["13"] = 22;
        bagConsumable["3"] = 1;


    }
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        playerVec = new Vector3(0,0,0);
    }

    void Update()
    {

    }

    public static void SaveData()
    {

        
    }

    public static void LoadData()
    {

    }

    
    private void OnApplicationQuit()
    {
        SaveData();
    }

    private void DataLoader()
    {
        consumables = LoadJsonData<Consumable>(consumables_path);
        weapons = LoadJsonData<Weapon>(weapons_path);
        //abilities = LoadJsonData<Ability>("Abilities.json");
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