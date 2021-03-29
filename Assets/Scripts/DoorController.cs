﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{

    public string toScene; //name of scene to load into
    public bool teleport = false; //flag to teleport to a teleport pad within current scene
    Collider2D myCollider; //door collider
    private bool byDoor = false; //detect if player is near door
    GameObject doorAlert; //UI element when near door
    GameObject player; //player gameObject
    GameObject panel; //transition animation panel
    GameObject teleportPad; //pad to teleport to 
    Animator animator; //animator to control transition effect 

    void Start()
    {
        byDoor = false;
        myCollider = GetComponent<Collider2D>();
        doorAlert = GameObject.Find("DoorAlert");
        doorAlert.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
        panel = GameObject.Find("TransitionPanel");
        animator = panel.transform.GetComponent<Animator>();
        teleportPad = GameObject.FindGameObjectWithTag("TeleportPad");
    }

    // Update is called once per frame
    void Update()
    {
        //If next to door and e is hit, either go to new scene or teleport within coordinates
        if(byDoor && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(TransitionRoutine());
            StartCoroutine(WaitReset());
        }
    }

    private IEnumerator TransitionRoutine()
    {
        if (!teleport)
        {
            animator.SetTrigger("startTrans");
            yield return new WaitForSeconds(1f);
            animator.SetTrigger("transOut");
            SceneManager.LoadScene(toScene);
        }
        else
        {
            animator.SetTrigger("startTrans");
            yield return new WaitForSeconds(1f);
            animator.SetTrigger("transOut");
            player.transform.position = teleportPad.transform.position;
            
        }

    }

    private IEnumerator WaitReset()
    {
        yield return new WaitForSeconds(3f);
        animator.Rebind();
        animator.Update(3f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        byDoor = true;
        doorAlert.SetActive(true);
        Debug.Log("Entering Trigger!!");
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        byDoor = false;
        doorAlert.SetActive(false);
        Debug.Log("Exiting Trigger!!");
    }
}
