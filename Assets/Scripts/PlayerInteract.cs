
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