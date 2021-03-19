///<summary>
/// Contains all persistent attributes of the player.
///</summary>

// consider using RangeFloat and [MinMaxRange(x,y)] for certain values

using UnityEngine;

[CreateAssetMenu(menuName = "Player/Attributes")]
public class Attributes : ScriptableObject
{
    public string Name;
    public int Level;
    public int DeathCount;
    public int AttributePoints;
    public float CurrentXP;
    public float RequiredXP;

    [Range(0f, 100f)]
    public float Health;
    [Range(0f, 100f)]
    public float Endurance;
    [Range(0f, 100f)]
    public float Mana;

    public int Attack;
    public int Defense;

    public int Strength;
    public int Stamina;
    public int Agility;
    public int Intelligence;  
}