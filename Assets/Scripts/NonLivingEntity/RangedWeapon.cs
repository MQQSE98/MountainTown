using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ranged Weapon", menuName = "Ranged Weapon")]
public class RangedWeapon : Weapon
{
    public GameObject Projectile;
    public RangedType WeaponType;
    public int AmmoCount;
    public float TravelSpeed; // rate by which projectile flies after being fired
    //NOTE: Possibly add a FlyPattern int var that corresponds to different flying animations??
    public override void Attack()
    {
        GameObject player = GameObject.FindWithTag("Player");
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        PlayerManager playerManager = player.GetComponent<PlayerManager>();
        PlayerCombat combat = playerManager.playerCombat;
        string orientation = playerManager.orientation;
        float holdTime = playerManager.holdTimeDelta;
        combat.RangedAttack(player, this, enemies, orientation, holdTime);
    }
    public override void Use()
    {
        GameObject player = GameObject.FindWithTag("Player");
        PlayerManager playerManager = player.GetComponent<PlayerManager>();

        playerManager.currentRangedWeapon = this;
    }
}
