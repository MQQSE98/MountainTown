using System.Collections;
using System.Collections.Generic;
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
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            Die();
        }
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
        int dropNum = rand.Next(1, 6);
        loadedPrefab = Resources.Load("Assets/Prefabs/ItemPrefabs/XpBall.prefab");
        SpawnAroundPoint(dropNum, deathPosition, 2F);
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

            var spawnDir = new Vector3(horizontal, 0, vertical);

            /* Get the spawn position */
            var spawnPos = point + spawnDir * radius; // Radius is just the distance away from the point

            /* Now spawn */
            var xp = Instantiate(loadedPrefab, spawnPos, Quaternion.identity) as GameObject;

            /* Rotate the enemy to face towards player */
            xp.transform.LookAt(point);

            /* Adjust height */
            xp.transform.Translate(new Vector3(0, xp.transform.localScale.y / 2, 0));
            xp.SetActive(true);
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
