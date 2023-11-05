using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

[System.Serializable]
public class Weapon
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("damage")]
    public int Damage { get; set; }

    [JsonProperty("price")]
    public int Price { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("quality")]
    public string QualityString { get; set; }


    [JsonIgnore]
    public ItemQuality Quality
    {
        get
        {
            return (ItemQuality)System.Enum.Parse(typeof(ItemQuality), QualityString, true);
        }
    }
}

