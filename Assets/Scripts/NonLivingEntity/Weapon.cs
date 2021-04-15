using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//base abstract weapon class
public abstract class Weapon : Item
{
    public float Range; // how far projectile flies / sword goes before it stops doing damage
    public float Damage; //damage the weapon does
    public float AttackRate; //rate of fire
    public DamageType DamageType; //type of elemental damage the item does (makes stronger / weaker against different enemy types)
    public abstract void Attack();
}