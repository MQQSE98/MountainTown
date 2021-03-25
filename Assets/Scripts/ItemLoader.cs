﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLoader : MonoBehaviour
{
    public ItemSO[] lootPool;

    private GameObject currentPrefab;

    // Start is called before the first frame update
    void Start()
    {
        for (int i =0; i < lootPool.Length; i++)
        {
            currentPrefab = lootPool[i].Prefab;
            currentPrefab.GetComponent<ItemController>().ItemData = lootPool[i];
            currentPrefab.GetComponent<SpriteRenderer>().sprite = lootPool[i].Icon;
            currentPrefab.SetActive(true);
            Instantiate(currentPrefab, new Vector3 (1.5F, 1, 0), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
