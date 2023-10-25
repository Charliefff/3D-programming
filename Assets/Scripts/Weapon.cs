using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ªZ¾¹Ãþ§O
/// </summary>
public class Weapon : Item
{
    public int Damage {  get; set; }
    public WeaponType WPType{  get; set; }

    public Weapon (int ID, string name, ItemType type , ItemQuality quality, string description, int capacity, int buyPrice, int sellPrice, string sprite, int damage, WeaponType weaponType) : base(ID, name, type, quality, description, capacity, buyPrice, sellPrice, sprite)
    {
        this.Damage = damage;
        this.WPType = weaponType;
    }

    public enum WeaponType
    {
        skill,
        MainHand
    }

}
