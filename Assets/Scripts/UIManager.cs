﻿using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject Inventory;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        if(Input.GetButtonDown("Inventory"))
        {
            Inventory.SetActive(!Inventory.activeSelf);
        }
    }
}
