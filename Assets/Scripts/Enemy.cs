
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

[System.Serializable]
public class Enemy
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("level")]
    public int Level { get; set; }

    [JsonProperty("health")]
    public int Health { get; set; }

    [JsonProperty("attack")]
    public int Attack { get; set; }

    [JsonProperty("defense")]
    public int Defense { get; set; }

    [JsonProperty("experience")]
    public int Experience { get; set; }

    [JsonProperty("speed")]
    public int Speed { get; set; }

    [JsonProperty("item")]
    public string Item { get; set; }

    [JsonProperty("skill")]
    public string Skill { get; set; }

    [JsonProperty("gold")]
    public int Gold { get; set; }
}
