
using System.Data.SqlTypes;
using UnityEngine;

[System.Serializable]
public class Ability 
{
    public int MaxBlood;
    public double MaxMana;
    public int Blood;
    public double Mana;
    public int Money;
    public double experience;
    public int Level;
    public double ability;
}

[CreateAssetMenu(fileName = "Character_ability", menuName = "3D_gaming/Create Character ability")]
public class AbilityTest : ScriptableObject
{
    public Ability ability_test;
}
