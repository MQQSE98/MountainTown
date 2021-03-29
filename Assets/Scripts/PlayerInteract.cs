<<<<<<< HEAD
﻿using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public GameObject currentInteractableObject = null;

    void Update()
    {
        if (Input.GetButtonDown("Interact") && currentInteractableObject)
        {
            
            //do something with object
            currentInteractableObject.SendMessage("PerformInteraction");
            // UnityEngine.Debug.Log("FUCK");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("InteractableObject"))
        {
            UnityEngine.Debug.Log(other.name);
            currentInteractableObject = other.gameObject;

        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
            if(other.CompareTag("InteractableObject"))
        {
            if(other.gameObject == currentInteractableObject)
            {
                currentInteractableObject = null;
                UnityEngine.Debug.Log(other.name);
            }
        }
    }
}
=======
﻿using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public GameObject currentInteractableObject = null;

    private GameObject player;

    private void Start()
    {
         player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (Input.GetButtonDown("Interact") && currentInteractableObject)
        {
            
            //do something with object
            currentInteractableObject.SendMessage("PerformInteraction");
            // UnityEngine.Debug.Log("FUCK");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("InteractableObject"))
        {
            UnityEngine.Debug.Log(other.name);
            currentInteractableObject = other.gameObject;

        }
        else if (other.CompareTag("Item"))
        {
            UnityEngine.Debug.Log(other.name);
            currentInteractableObject = other.gameObject;

            if (other.GetComponent<ItemController>().ItemData.TypeOfItem == TypeOfItem.MeleeWeapon)
            {
                //player.GetComponent<PlayerM>().currentMeleeWeapon = other
            }
            else if (other.GetComponent<ItemController>().ItemData.TypeOfItem == TypeOfItem.RangedWeapon)
            {
                //player.GetComponent<PlayerM>().currentRangedWeap = other
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
            if(other.CompareTag("InteractableObject"))
        {
            if(other.gameObject == currentInteractableObject)
            {
                currentInteractableObject = null;
                UnityEngine.Debug.Log(other.name);
                //player.GetComponent<PlayerM>().
            }
        }
    }
}
>>>>>>> origin
