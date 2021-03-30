///<summary>
/// Contains all persistent attributes of the player.
///</summary>

// consider using RangeFloat and [MinMaxRange(x,y)] for certain values

using UnityEngine;

[CreateAssetMenu(menuName = "Player/Attributes")]
public class Attributes : ScriptableObject
{
    public string characterName;
    public int level;
    public int deathCount;
    public int attributePoints;
    public float currentExperience;
    public float requiredExperience;

    [Range(0f, 100f)]
    public float health;
    [Range(0f, 100f)]
    public float endurance;
    [Range(0f, 100f)]
    public float mana;

    public int attack;
    public int defense;

    public int strength;
    public int stamina;
    public int agility;
    public int intelligence;  
}