using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    

     void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Enemy") && gameObject.GetComponentInParent<PlayerMovement>().blocking == true)
        {
            if (other.gameObject.GetComponentInParent<EnemyCombat>().enemyAttacking == true)
            {
                gameObject.GetComponentInParent<PlayerManager>().blockedAttack = true;
                
            }

        }
    }

    
    
}
