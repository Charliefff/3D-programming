using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

[System.Serializable]
public class State
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("duration")]
    public int Duration { get; set; }

    [JsonProperty("hp")]
    public int Hp { get; set; }

    [JsonProperty("mp")]
    public int Mp { get; set; }

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
}
