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
public abstract class Item : ScriptableObject
{
    [SerializeField] string id;
    public string ID { get { return id; } }

    public GameObject Prefab;
    public TypeOfItem TypeOfItem;
    public string ItemName;
    [TextArea(15, 20)]
    public string ItemDescription;
    public bool isInInventory;
    public Sprite Icon;
    public float Rarity;
    public float HealthDebuff; //for items that deal damage to the player when used
    public float StaminaDebuff; //for itmes that make player have less stamina when used 
    public float SpeedDebuff; //for items that make player slower when used
    [Range(1, 10)]
    public int MaxStackAmount;
    public abstract void Use();

    private void OnValidate()
    {
        string path = UnityEditor.AssetDatabase.GetAssetPath(this);
        id = UnityEditor.AssetDatabase.AssetPathToGUID(path);
    }

}

//public abstract class Scroll : Item
//{
//    public float failPercent;

//}

//public abstract class Armour : Item
//{
//    public float agilityBonus;
//    public float defense;
//    public override void Use()
//    {
//        throw new System.NotImplementedException();
//    }
//}

//public class LightArmour : Armour
//{

//}
//public class MediumArmour : Armour
//{

//}
//public class HeavyArmour : Armour
//{

//}

//base abstract weapon class
//public abstract class Weapon : Item
//{
//    public float Range; // how far projectile flies / sword goes before it stops doing damage
//    public float Damage; //damage the weapon does
//    public float AttackRate; //rate of fire
//    public DamageType DamageType; //type of elemental damage the item does (makes stronger / weaker against different enemy types)
//    public abstract void Attack();
//}

//base abstract potion class
//public abstract class Potion : Item
//{
//    public float EffectDuration; //time the potion lasts if IsTimed
//    public bool IsTimed; //toggle for potions with timed effects
//}

//[CreateAssetMenu(fileName = "New Melee Weapon", menuName = "Melee Weapon")]
//public class MeleeWeapon : Weapon
//{
//    public MeleeType WeaponType;
//    public bool TwoHanded;
//    public override void Attack()
//    {
//        GameObject player = GameObject.FindWithTag("Player");
//        PlayerManager playerManager = player.GetComponent<PlayerManager>();
//        PlayerCombat combat = playerManager.playerCombat;
//        string orientation = playerManager.orientation;
//        combat.MeleeAttack(player, this, GameObject.FindGameObjectsWithTag("Enemy"), orientation);
//    }

//    public override void Use()
//    {
//        GameObject player = GameObject.FindWithTag("Player");
//        PlayerManager playerManager = player.GetComponent<PlayerManager>();

//        playerManager.currentMeleeWeapon = this;
//    }
//}

//[CreateAssetMenu(fileName = "New Ranged Weapon", menuName = "Ranged Weapon")]
//public class RangedWeapon : Weapon
//{
//    public GameObject Projectile;
//    public RangedType WeaponType;
//    public int AmmoCount;
//    public float TravelSpeed; // rate by which projectile flies after being fired
//    //NOTE: Possibly add a FlyPattern int var that corresponds to different flying animations??
//    public override void Attack()
//    {
//        GameObject player = GameObject.FindWithTag("Player");
//        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
//        PlayerManager playerManager = player.GetComponent<PlayerManager>();
//        PlayerCombat combat = playerManager.playerCombat;
//        string orientation = playerManager.orientation;
//        float holdTime = playerManager.holdTimeDelta;
//        combat.RangedAttack(player, this, enemies, orientation, holdTime);
//    }
//    public override void Use()
//    {
//        GameObject player = GameObject.FindWithTag("Player");
//        PlayerManager playerManager = player.GetComponent<PlayerManager>();

//        playerManager.currentRangedWeapon = this;
//    }
//}

//[CreateAssetMenu(fileName = "New Health Potion", menuName = "Health Potion")]
//public class HealthPotion : Potion
//{
//    public float HealthAdd;
//    public override void Use()
//    {
//        GameObject player = GameObject.FindWithTag("Player");
//        PlayerManager playerManager = player.GetComponent<PlayerManager>();

//        playerManager.Heal(HealthAdd);

//        playerManager.playerSheet.Remove(this);
//    }
//}

//[CreateAssetMenu(fileName = "New Stamina Potion", menuName = "Stamina Potion")]
//public class StaminaPotion : Potion
//{
//    public float StaminaAdd;
//    public override void Use()
//    {
//        throw new System.NotImplementedException();
//    }
//}

//[CreateAssetMenu(fileName = "New Speed Potion", menuName = "Speed Potion")]
//public class SpeedPotion : Potion
//{
//    public float SpeedAdd;
//    public override void Use()
//    {
//        throw new System.NotImplementedException();
//    }
//}

////Item type of a potion that has multiple effects (I.E. Health + Speed)
////We have already defined this functionality above, just combine the effects of multiple potions into one!
//[CreateAssetMenu(fileName = "New Combo Potion", menuName = "Combo Potion")]
//public class ComboPotion : Item
//{
//    public Potion[] PotionList;
//    public override void Use()
//    {
//        throw new System.NotImplementedException();
//    }
//}



