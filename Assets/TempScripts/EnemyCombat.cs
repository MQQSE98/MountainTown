using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Combat/EnemyCombat")]
public class EnemyCombat : Combat
{

    public Attributes playerAttributes;


    public void MeleeAttack(GameObject enemy, GameObject player)
    {

        if ((player.transform.position - enemy.transform.position).magnitude < enemy.GetComponent<EnemyM>().HitRange)
        {
            playerAttributes.health -= 10;
            if (playerAttributes.health <= 0)
                Destroy(player);
            //Debug.Log("Angle: "+angle);


        }
    }
}
