using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyM : MonoBehaviour
{

    public float health = 100;
    public float hitRange;
    private EnemyCombat enemyCombat;
    public float hitDelta;

    public GameObject player;
    //string orientation;


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
        enemyCombat = gameObject.GetComponent<EnemyCombat>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Distance between player and boss: " + (player.GetComponent<Transform>().position - this.transform.position));
        OnDeath();
    }

    void FixedUpdate()
    {
        if (hitDelta > 1.5)
        {
            enemyCombat.MeleeAttack(this.gameObject, player);
            hitDelta = 0;
        }
        else
        {
            hitDelta += Time.fixedDeltaTime;
        }

    }
    public void OnDeath()
    {
        if (this.gameObject.name == "SanguineSludge_Boss")
        {
            
            if (health <= 0)
            {
                this.hitRange = 0;
                this.gameObject.GetComponent<SSBOSSMovementControl>().DIE();
                //Destroy(this.gameObject);
            }
        }
        else
        {
            if (health <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
    public void setRange(float range)
    {
        hitRange = range;
    }
}