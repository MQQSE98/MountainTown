using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 //test
[CreateAssetMenu(menuName = "Combat/EnemyCombat")]
public class EnemyCombat : Combat
{

    private PlayerManager playerManager;

    private void Start()
    {
        playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
    }


    public void MeleeAttack(GameObject enemy, GameObject player)
    {

        if ((player.transform.position - enemy.transform.position).magnitude < enemy.GetComponent<EnemyM>().HitRange)
        {
            playerManager.TakeDamage(10);
        }
    }
}
