using System.Collections;
using UnityEngine;
 //test
public class EnemyCombat : Combat
{
    private SpriteRenderer sr;
    private PlayerManager playerManager;
    public ParticleSystem gloopDamageEffect;

    private void Start()
    {
        playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        sr = gameObject.GetComponent<Transform>().GetComponentInChildren<SpriteRenderer>();
    }


    public void MeleeAttack(GameObject enemy, GameObject player)
    {
       if ((player.transform.position - enemy.transform.position).magnitude < enemy.GetComponent<EnemyM>().HitRange)
        {
            playerManager.TakeDamage(1);
        }
    }
    public void TakeDamage()
    {
        Instantiate(gloopDamageEffect, transform.position, Quaternion.identity, transform);
    }

        

}
