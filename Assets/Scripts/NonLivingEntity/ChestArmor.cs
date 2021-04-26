using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChestArmor", menuName = "Chest Armor")]
public class ChestArmor : Item
{

    public override void Use()
    {
        GameObject player = GameObject.FindWithTag("Player");
        PlayerManager playerManager = player.GetComponent<PlayerManager>();

        playerManager.EquipItem(this);
        playerManager.AddEquipmentStats(this);
        playerManager.UpdateEquipmentSlots();
        playerManager.UpdatePanelSlots();
    }


}
