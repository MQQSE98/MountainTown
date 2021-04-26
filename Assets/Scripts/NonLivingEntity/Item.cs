using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//hh
public enum TypeOfItem
{
    MeleeWeapon, 
    RangedWeapon,
    HealthPotion, 
    SpeedPotion, 
    StaminaPotion,
    HeadArmor,
    ChestArmor,
    HandArmor,
    FeetArmor,
    LegArmor,
    Ring,
    Amulet,
    ShoulderArmor
}

public enum DamageType
{
    Fire, 
    Ice, 
    Magic, 
    Blunt, 
    Slashing, 
    Piercing
}

public enum MeleeType
{
    OneHanded, 
    TwoHanded, 
    DualWield
}

public enum RangedType
{
    Bow, 
    Thrown
}

//this is a comment
//base abstract item class
public abstract class Item : ScriptableObject
{
    [SerializeField] string id;
    public string ID { get { return id; } }

    public GameObject Prefab;
    public TypeOfItem TypeOfItem;
    public bool isEquiped;
    public bool takesTwoHands;
    public bool isOffHand;
    public string ItemName;
    [TextArea(15, 20)]
    public string ItemDescription;
    public bool isInInventory;
    public Sprite Icon;
    public float Rarity;
    public float HealthDebuff; //for items that deal damage to the player when used
    public float StaminaDebuff; //for itmes that make player have less stamina when used 
    public float SpeedDebuff; //for items that make player slower when used
    public int weight;
    public int health;
    public int defense;

    public int attack;
    
    [Range(1, 10)]
    public int MaxStackAmount;
    public abstract void Use();

    private void OnValidate()
    {
        string path = UnityEditor.AssetDatabase.GetAssetPath(this);
        id = UnityEditor.AssetDatabase.AssetPathToGUID(path);
    }

}




