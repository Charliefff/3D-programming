using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

[System.Serializable]
public class Skill
{
    [JsonProperty("SkillName")]
    public string SkillName { get; set; }

    [JsonProperty("SkillType")]
    public string SkillType { get; set; }

    [JsonProperty("Attribute")]
    public string Attribute { get; set; }

    [JsonProperty("Effect")]
    public string Effect { get; set; }

    [JsonProperty("EffectValue")]
    public int EffectValue { get; set; }

    [JsonProperty("EffectDuration")]
    public int EffectDuration { get; set; }

    [JsonProperty("ManaCost")]
    public int ManaCost { get; set; }

    [JsonProperty("Range")]
    public string Range { get; set; }

    [JsonProperty("Level")]
    public string Level { get; set; }

    [JsonProperty("Description")]
    public string Description { get; set; }
}
