using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    [JsonProperty("day")]
    public string Day { get; set; }

    [JsonProperty("money")]
    public int Money { get; set; }

    [JsonProperty("bagConsumable")]
    public Dictionary<string, int> BagConsumable{ get; set; }

    [JsonProperty("BagWeapons")]
    public Dictionary<string, int> BagWeapons{ get; set; }

    [JsonProperty("playerPosX")]
    public float PlayerPosX { get; set; }

    [JsonProperty("playerPosY")]
    public float PlayerPosY { get; set; }

    [JsonProperty("playerPosZ")]
    public float PlayerPosZ { get; set; }

    [JsonProperty("sceneName")]
    public string SceneName { get; set; }

    [JsonProperty("player")]
    public Ability[] Player { get; set; }

    public SaveData(){
        Player = new Ability[4];
        BagConsumable = new Dictionary<string, int>();
        BagWeapons = new Dictionary<string, int>();
    }    
}
