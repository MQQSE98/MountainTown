using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/Character Sheet")]
public class CharacterSheet : ScriptableObject
{
    [Header("Character Info")]
    public string playerName;
    public int level;
    public int ExpToLevel;
    public int experiencePoints;
    public int attributePoints;
    public int skillPoints;
    public float weight;

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

    [Header("Equipment")]
    public Item HeadSlot;
    public Item BodySlot;
    public Item HandSlot;
    public Item BootSlot;
    public Item LegSlot;
    public Item ShoulderSlot;
    public Item RingSlot;    
    public Item AmuletSlot;    
    public Item WeaponSlotLeft;
    public Item WeaponSlotRight;
        
    public void AddItem(Item item)
    {

        for (int i = 0; i < bag.Count; i++)
        {
            if (bag[i] == null)
            {
                bag[i] = item;

                break;
            }
            else if (bag[i] == item)
            {
                Debug.Log("There is already an item at index" + i + item.ItemName);
            }


        }


    }

    public void Remove(Item item)
    {
        bag.Remove(item);
        bag.Add(null);
    }
}