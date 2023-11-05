using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

[System.Serializable]
public class Ability
{
    [JsonProperty("hp")]
    public int HP { get; set; }

    [JsonProperty("mp")]
    public int MP { get; set; }

    [JsonProperty("attack")]
    public int Attack { get; set; }

    [JsonProperty("defense")]
    public int Defense { get; set; }

    [JsonProperty("magic")]
    public int Magic { get; set; }

    [JsonProperty("speed")]
    public int Speed { get; set; }

    [JsonProperty("immunity")]
    public string Immunity { get; set; }

    [JsonProperty("duration")]
    public int Duration { get; set; }

 
    [JsonProperty("image")]
    public string Image { get; set; }

}

