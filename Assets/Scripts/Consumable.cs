using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;


public enum ItemQuality
{
    Common,
    Rare,
    Epic,
}

public enum ItemType
{
    Consumable,
    Material
}
[System.Serializable]
public class Consumable
{
    public string Name { get; set; }
    public string Type { get; set; }
    public string Quality { get; set; }
    public string Description { get; set; }
    public int SellPrice { get; set; }
    public int BuyPrice { get; set; }
    public int HP { get; set; }
    public int MP { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public int Magic { get; set; }
    public int Speed { get; set; }
    public string Immunity { get; set; }
    public int Duration { get; set; }
    public string Image { get; set; }

    [JsonIgnore]
    public ItemType ItemTypeEnum => (ItemType)System.Enum.Parse(typeof(ItemType), Type, true);
    [JsonIgnore]
    public ItemQuality ItemQualityEnum => (ItemQuality)System.Enum.Parse(typeof(ItemQuality), Quality, true);
}
