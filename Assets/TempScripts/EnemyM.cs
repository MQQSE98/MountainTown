using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyM : MonoBehaviour
{

    public float health = 100;
    public float hitRange;
    public EnemyCombat enemyCombat;
    public float hitDelta;

    public float HitRange
    {
        get
        {
            return hitRange;
        }
        set
        {
            hitRange = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    void FixedUpdate()
    {
        if(hitDelta > 1.5)
        {
            enemyCombat.MeleeAttack(this.gameObject, GameObject.FindWithTag("Player"));
            hitDelta = 0;
        } else
        {
            hitDelta += Time.fixedDeltaTime;
        }
        
    }
}
