///<summary>
/// Contains all persistent inventory items of the player.
///</summary>
using System;


using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Player/Inventory")]
public class Inventory : ScriptableObject
{
    public string[] BagSlots = new string[25];
    public int[] ItemAmounts = new int[25];
    public int amount;

    //public GameObject HeadSlot;
    //public GameObject ChestSlot;
    //public GameObject LegsSlot;
    //public GameObject FeetSlot;
    //public GameObject LeftHandSlot;
    //public GameObject RightHandSlot;
    //public GameObject LeftRingSlot;
    //public GameObject RightRingSlot;
    //public GameObject LeftEarSlot;
    //public GameObject RightEarSlot;
    //public GameObject NeckSlot;

    public int Gold;
    public int Silver;
    public int Copper;


    public void AddItemID(String _id, int _amount)
    {
        Debug.Log(_id);
        UnityEngine.Debug.Log("Item Added");
        bool hasItem = false;
        amount += _amount;
        //find first open slot
        for(int i = 0; i < BagSlots.Length; i++)
        {
            Debug.Log(BagSlots[i]);
             if(BagSlots[i] == "")
             {
                     BagSlots[i] = _id;
                     ItemAmounts[i] = amount;
                     
                     
                     hasItem = true;
                     break;
              }
            

           
        }
        if(!hasItem)
        {
            Debug.Log("Bags Full");
        }

    }

    
  

}


