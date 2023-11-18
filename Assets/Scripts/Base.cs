using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text.Json;
using Newtonsoft.Json;

public class Base : MonoBehaviour
{

    public SerializableDictionary<string, Consumable> consumables = new();
    public SerializableDictionary<string, Weapon> weapons = new();
    public SerializableDictionary<string, Ability> abilities = new();
    public SerializableDictionary<string, Enemy> enemies = new();
    public SerializableDictionary<string, Skill> skills = new();
    public SerializableDictionary<string, State> states = new();

    //public SerializableDictionary<string, Monster>
    public static Ability[] player = new Ability[4];

    public static string consumables_path = "../Json/consumable.json";
    public static string weapons_path = "../Json/weapon.json";
    public static string abilities_path = "";
    public static string enemies_path = "../Json/enemy.json";
    public static string skills_path = "../Json/skill.json";
    public static string states_path = "../Json/state.json";

    public static Vector3 playerVec;
    public static int enemyID;
    public static string sceneName;
    public static string Bagdatapath = "../Json/Bagjson";
    void Start()
    {

        DataLoader();
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
        consumables = LoadJsonData<Consumable>(weapons_path);
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
