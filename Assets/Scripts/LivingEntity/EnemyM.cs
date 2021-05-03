using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EnemyM : MonoBehaviour
{

    public float health = 100;
    public float hitRange;
    private EnemyCombat enemyCombat; 
    public float hitDelta;
    public Item[] lootPool;
    
    private GameObject currentPrefab;
    private Vector3 deathPosition;
    private System.Random rand = new System.Random();
    private UnityEngine.Object loadedPrefab;
    private GameObject player;
    public Image credits;

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
        

        if (gameObject.transform.name == "SanguineSludge_Boss")
        {
            BossHeathController bossHeathController = gameObject.GetComponent<BossHeathController>();
            bossHeathController.SetMaxHealth(health);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Distance between player and boss: " + (player.GetComponent<Transform>().position - this.transform.position));
        OnDeath();
    }

    //handle all the things enemy needs to do on death
    //destroy itself
    //drop loot
    //drop XP
    void Die()
    {
        deathPosition = gameObject.transform.position;
        Destroy(this.gameObject);
        LootDrop();
        XPDrop();
        
    }

    //spawn loot drop item
    void LootDrop()
    {
        for (int i = 0; i < lootPool.Length; i++)
        {
            if (i == 0)
            {
                currentPrefab = lootPool[i].Prefab;
                currentPrefab.GetComponent<ItemManager>().item = lootPool[i];
                currentPrefab.GetComponent<SpriteRenderer>().sprite = lootPool[i].Icon;
                currentPrefab.GetComponent<SpriteRenderer>().sortingOrder = 1; //have to do this so it does not render under the base layerssds
                currentPrefab.SetActive(true);
                Instantiate(currentPrefab, deathPosition, Quaternion.identity);
            }
            else
            {
                currentPrefab = lootPool[i].Prefab;
                currentPrefab.GetComponent<ItemManager>().item = lootPool[i];
                currentPrefab.GetComponent<SpriteRenderer>().sprite = lootPool[i].Icon;
                currentPrefab.SetActive(true);
                Instantiate(currentPrefab, new Vector3(-12F, -1, 0), Quaternion.identity);
            }
        }
    }

    //spawn XP drop item
    void XPDrop()
    {
        int dropNum = rand.Next(20, 30);
        //loadedPrefab = Assetdatabase.load("Assets/Prefabs/ItemPrefabs/XpBall");
        SpawnAroundPoint(dropNum, deathPosition, 0.5F);
    }

   
    void SpawnAroundPoint(int num, Vector3 point, float radius)
    {
        for (int i = 0; i < num; i++)
        {

            /* Distance around the circle */
            var radians = 2 * Mathf.PI / num * i;

            /* Get the vector direction */
            var vertical = Mathf.Sin(radians);
            var horizontal = Mathf.Cos(radians);

            float randomX = rand.Next(5, 10);
            float randomY = rand.Next(5, 10);

            //Vector3 offsetVector = new Vector3();

            var spawnDir = new Vector3(horizontal, 0, vertical);

            /* Get the spawn position */
            var spawnPos = point + spawnDir * radius; // Radius is just the distance away from the point

            /* Now spawn */
            GameObject xp = (GameObject)Instantiate(Resources.Load("XpBall"), spawnPos, Quaternion.identity);
            GameObject gold = (GameObject)Instantiate(Resources.Load("Gold"), spawnPos, Quaternion.identity);


            /* Rotate the enemy to face towards player */
            xp.GetComponent<SpriteRenderer>().sortingOrder = 2;
            xp.transform.LookAt(point);

            gold.GetComponent<SpriteRenderer>().sortingOrder = 2;
            gold.transform.LookAt(point);

            /* Adjust height */
            xp.transform.Translate(new Vector3(randomX, randomY, 0));
            xp.SetActive(true);

            gold.transform.Translate(new Vector3(randomX, randomY, 0));
            gold.SetActive(true);


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
    public void OnDeath()
    {
        if (this.gameObject.name == "SanguineSludge_Boss")
        {
            
            if (health <= 0)
            {
                this.hitRange = 0;
                this.gameObject.GetComponent<SSBOSSMovementControl>().DIE();

                credits.enabled = true;
                //Destroy(this.gameObject);
            }
        }
        else
        {
            if (health <= 0)
            {
                Die();
            }
        }
    }
    public void setRange(float range)
    {
        hitRange = range;
    }
}
