using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Melee Weapon", menuName = "Melee Weapon")]
public class MeleeWeapon : Weapon
{
    public MeleeType WeaponType;
    public bool TwoHanded;
    public override void Attack()
    {
        GameObject player = GameObject.FindWithTag("Player");
        PlayerManager playerManager = player.GetComponent<PlayerManager>();
        PlayerCombat combat = playerManager.playerCombat;
        string orientation = playerManager.orientation;
        combat.MeleeAttack(player, this, GameObject.FindGameObjectsWithTag("Enemy"), orientation);
    }

    public override void Use()
    {
        GameObject player = GameObject.FindWithTag("Player");
        PlayerManager playerManager = player.GetComponent<PlayerManager>();
        
        playerManager.EquipItem(this);
        playerManager.UpdateEquipmentSlots();
        playerManager.UpdatePanelSlots();

        
    }
}