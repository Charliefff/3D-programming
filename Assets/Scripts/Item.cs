using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public int ID {  get; }
    public string Name { get; set; }
    public ItemType Type { get; set; }
    public ItemQuality Quality { get; set; }
    public string Descrition { get; set; }
    public int Capacity { get; set; }
    public int BuyPrice { get; set; }
    public int SellPrice { get; set; }
    public string Sprite { get; set; }

    public Item() { 
    
    }

    public Item (int ID, string name, ItemType type, ItemQuality quality, string descrition, int capacity, int buyPrice, int sellPrice, string sprite)
    {
        this.ID = ID;
        this.Name = name;
        this.Type = type;
        this.Quality = quality;
        this.Descrition = descrition;
        this.Capacity = capacity;
        this.BuyPrice = buyPrice;
        this.SellPrice = sellPrice;
        this.Sprite = sprite;
    }

    public enum ItemType
    {
        Comsumable,
        Equipment, 
        Weapon, 
        Material
    }

    public enum ItemQuality
    {
        Commpon, 
        Unmmon, 
        Rare, 
        Epic, 
        Legendary, 
        Artifact
    }

}
