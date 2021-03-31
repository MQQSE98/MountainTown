/**
 * Author: Joseph Mills
**/

using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/Character Sheet")]
public class CharacterSheet : ScriptableObject
{
    [Header("Character Info")]
    public string playerName;
    public int level;
    public int experiencePoints;
    public int attributePoints;
    public int skillPoints;

    [Header("Prime Attributes")]
    public float maxHealth;
    public float currentHealth;
    public float maxStamina;
    public float currentStamina;
    public float maxMana;
    public float currentMana;
    public float attack;
    public float defense;

    [Header("Ability Scores")]
    public int power;
    public int vitality;
    public int endurance;
    public int agility;
    public int intellect;
    public int luck;

    [Header("Skills")]
    public int oneHanded;
    public int twoHanded;
    public int dualWield;
    public int bow;
    public int thrown;
    public int channeling;
    public int useScroll;
    public int lore;
    public int lightArmor;
    public int mediumArmor;
    public int heavyArmor;

    [Header("Inventory")]
    public List<Item> bag = new List<Item>();
    public int gold;

    public void Add(Item item)
    {
        if (bag.Count < 35)
        {
            bag.Add(item);
            //item.isInInventory = true;
        }
        else
        {
            Debug.Log("Inventory is full");
        }
    }

    public void Remove(Item item)
    {
        bag.Remove(item);
    }
}