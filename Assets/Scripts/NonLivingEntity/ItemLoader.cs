using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLoader : MonoBehaviour
{
    public Item[] lootPool;

    private GameObject currentPrefab;

    // Start is called before the first frame update
    void Start()
    {
        for (int i =0; i < lootPool.Length; i++)
        {
            if(i == 0)
            {
                currentPrefab = lootPool[i].Prefab;
                currentPrefab.GetComponent<ItemManager>().item = lootPool[i];
                currentPrefab.GetComponent<SpriteRenderer>().sprite = lootPool[i].Icon;
                currentPrefab.SetActive(true);
                Instantiate(currentPrefab, new Vector3(-9F, -1, 0), Quaternion.identity);
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
