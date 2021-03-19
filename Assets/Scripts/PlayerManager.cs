using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public Attributes PlayerAttributes;
    public Inventory PlayerInventory;
    public Skills PlayerSkills;
    public PlayerCombat PlayerCombat;
    
    private bool isItem = false; 
    public GameObject CurrentInteractableObject = null;
    public GameObject[] Bag = new GameObject[25];
    public Button[] BagButtons = new Button[25];


    void Update()
    {


        PickUp();
       
    }
    void OnTriggerEnter2D(Collider2D other)
    {

        

        UnityEngine.Debug.Log(other.name);
        CurrentInteractableObject = other.gameObject;

        
    }

    void OnTriggerExit2D(Collider2D other)
    {
        
          
         CurrentInteractableObject = null;
         UnityEngine.Debug.Log(other.name);
        
    }

    public void PickUp()
    {  

        if (Input.GetButtonDown("Interact") && CurrentInteractableObject)
        {
            string _id = CurrentInteractableObject.GetComponent<ItemManager>().item.ID;

            print(_id);
            GameObject item = CurrentInteractableObject;
            PlayerInventory.AddItemID(_id, 1);


            AddItemToInventory(item);
            
            UnityEngine.Debug.Log("Pickup Successful");
        }
    }

    public void AddItemToInventory(GameObject item)
    {
        bool inBag = false;
        for(int i = 0; i < Bag.Length; i++)
        {
            if(Bag[i] == null)
            {
                Bag[i] = CurrentInteractableObject;

                BagButtons[i].image.overrideSprite = CurrentInteractableObject.GetComponent<SpriteRenderer>().sprite;

                inBag = true;
                break;
            }
        }

        if (!inBag)
        {
            Debug.Log("Bags Full");
        }
    }
}
