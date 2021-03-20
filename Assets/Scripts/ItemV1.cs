using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//comment to update :)
public enum TypeOfItem
{
    MeleeWeapon, 
    RangedWeapon,
    HealthPotion, 
    SpeedPotion, 
    StaminaPotion
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
public abstract class ItemSO : ScriptableObject
{
    public GameObject Prefab;
    public TypeOfItem TypeOfItem;
    public string ItemName;
    public string ItemDescription;
    public Sprite Icon;
    public float Rarity;
    public float HealthDebuff; //for items that deal damage to the player when used
    public float StaminaDebuff; //for itmes that make player have less stamina when used 
    public float SpeedDebuff; //for items that make player slower when used
}

public abstract class Scroll : ItemSO
{
    public float failPercent;

}

public abstract class Armour : ItemSO
{
    public float agilityBonus;
    public float defense;

}

public class LightArmour : Armour
{

}
public class MediumArmour : Armour
{

}
public class HeavyArmour : Armour
{

}

//base abstract weapon class
public abstract class Weapon : ItemSO
{
    public float Damage; //damage the weapon does
    public float AttackRate; //rate of fire
    public DamageType DamageType; //type of elemental damage the item does (makes stronger / weaker against different enemy types)
}

//base abstract potion class
public abstract class Potion : ItemSO
{
    public float EffectDuration; //time the potion lasts if IsTimed
    public bool IsTimed; //toggle for potions with timed effects
}

[CreateAssetMenu(fileName = "New Melee Weapon", menuName = "Melee Weapon")]
public class MeleeWeapon : Weapon
{
    public MeleeType WeaponType;
    public bool TwoHanded;
}

[CreateAssetMenu(fileName = "New Ranged Weapon", menuName = "Ranged Weapon")]
public class RangedWeapon : Weapon
{
    public GameObject Projectile;
    public RangedType WeaponType;
    public int AmmoCount;
    public float TravelSpeed; // rate by which projectile flies after being fired
    public float Range; // how far projectile flies before it stops doing damage
    //NOTE: Possibly add a FlyPattern int var that corresponds to different flying animations??
}

[CreateAssetMenu(fileName = "New Health Potion", menuName = "Health Potion")]
public class HealthPotion : Potion
{
    public float HealthAdd;
}

[CreateAssetMenu(fileName = "New Stamina Potion", menuName = "Stamina Potion")]
public class StaminaPotion : Potion
{
    public float StaminaAdd;
}

[CreateAssetMenu(fileName = "New Speed Potion", menuName = "Speed Potion")]
public class SpeedPotion : Potion
{
    public float SpeedAdd;
}

//Item type of a potion that has multiple effects (I.E. Health + Speed)
//We have already defined this functionality above, just combine the effects of multiple potions into one!
[CreateAssetMenu(fileName = "New Combo Potion", menuName = "Combo Potion")]
public class ComboPotion : ItemSO
{
    public Potion[] PotionList;
}



