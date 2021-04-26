using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Amulet", menuName = "Amulet")]
public class Amulet : Item
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

