using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

[System.Serializable]
public class Weapon
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("category")]
    public string Category { get; set; }

    [JsonProperty("buyPrice")]
    public int BuyPrice { get; set; }

    [JsonProperty("sellPrice")]
    public int SellPrice { get; set; }

    [JsonProperty("quality")]
    public string Quality { get; set; }

    [JsonProperty("HP")]
    public int HP { get; set; }

    [JsonProperty("MP")]
    public int MP { get; set; }

    [JsonProperty("ATK")]
    public int ATK { get; set; }

    [JsonProperty("MAG")]
    public int MAG { get; set; }

    [JsonProperty("DEF")]
    public int DEF { get; set; }

    [JsonProperty("AGI")]
    public int AGI { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("img")]
    public string Image { get; set; }
}
