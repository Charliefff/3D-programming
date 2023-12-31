using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

[System.Serializable]
public class Ability
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("level")]
    public int Level { get; set; }

    [JsonProperty("hp")]
    public int HP { get; set; }

    [JsonProperty("hpmax")]
    public int HPMax { get; set; }

    [JsonProperty("mp")]
    public int MP { get; set; }

    [JsonProperty("mpmax")]
    public int MPMax { get; set; }

    [JsonProperty("exp")]
    public int Exp { get; set; }

    [JsonProperty("expmax")]
    public int ExpMax { get; set; }

    [JsonProperty("attack")]
    public int Attack { get; set; }

    [JsonProperty("defense")]
    public int Defense { get; set; }

    [JsonProperty("magic")]
    public int Magic { get; set; }

    [JsonProperty("speed")]
    public int Speed { get; set; }
 
    [JsonProperty("image")]
    public string Image { get; set; }

    [JsonProperty("weapomList")]
    public List<string> WeaponList { get; set; }

    [JsonProperty("skillList")]
    public List<string> SkillList { get; set; }

    [JsonProperty("skillHistoryList")]
    public List<string> SkillHistoryList { get; set; }

    public Ability()
    {
        WeaponList = new List<string>();
        SkillList = new List<string>();
        SkillHistoryList = new List<string>();
    }

    public void SetAbility(
        string name,
        int level,
        int hp, 
        int hpmax, 
        int mp, 
        int mpmax, 
        int exp,
        int expmax,
        int atk, 
        int def, 
        int mag, 
        int spd, 
        string img = "")
    {
        Name = name;
        Level = level;
        HP = hp;
        HPMax = hpmax;
        MP = mp;
        MPMax = mpmax;
        Exp = exp; 
        ExpMax = expmax; 
        Attack = atk;
        Defense = def;
        Magic = mag;
        Speed = spd;
        Image = img;
    }


}

