using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Health Potion", menuName = "Health Potion")]
public class HealthPotion : Potion
{
    public float HealthAdd;
    public override void Use()
    {
        GameObject player = GameObject.FindWithTag("Player");
        PlayerManager playerManager = player.GetComponent<PlayerManager>();

        playerManager.Heal(HealthAdd);

        playerManager.playerSheet.Remove(this);
    }
}