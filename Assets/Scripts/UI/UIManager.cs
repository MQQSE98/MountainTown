using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//reload comment
public class UIManager : MonoBehaviour
{
    public GameObject InventoryV2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetButtonDown("Inventory"))
        {
            InventoryV2.SetActive(!InventoryV2.activeSelf);
        }
    }
}
