using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class LibrarianManager : NPCMovementManager
{

    //Create all Destinations here
    Destination townCenter = new Destination("Town", new Vector2(4f, -23));
    Destination cemetery = new Destination("Town", new Vector2(31f, 25f));
    Destination blacksmithYard = new Destination("Town", new Vector2(39, 3));
    Destination workPlace = new Destination("Library", new Vector2(110, 110));
    Destination home = new Destination("House3", new Vector2(110, 110));

    



    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        GameObject ChildGameObject1 = transform.GetChild(0).gameObject;
        //animator = ChildGameObject1.GetComponent<Animator>();
        currentLocation = new Destination("Town");
        //desiredLocation = new Destination("Blacksmith", new Vector2(110, 110));
        seeker = this.GetComponent<Seeker>();


        minIdleTime = 12;
        maxIdleTime = 24;
        minRandDist = 4;
        maxRandDist = 6;

        graph = new Graph();

    }


    void FixedUpdate()
    {

        if (worldClock.GetComponent<TimeManager>().TotalGameHour == 0 && worldClock.GetComponent<TimeManager>().TotalGameMin == 10)
        {
            if (!pathCompleted)
            {
                desiredLocation = townCenter;
                HandleMovement(desiredLocation);
                if ((rb.position - desiredLocation.loc).magnitude < 1)
                {
                    pathCompleted = true;
                }
            }
            else
            {
                RandomBehavior();
            }
        }
        else if (worldClock.GetComponent<TimeManager>().TotalGameHour == 3 && worldClock.GetComponent<TimeManager>().TotalGameMin == 0)
        {
            if (!pathCompleted)
            {
                desiredLocation = blacksmithYard;
                HandleMovement(desiredLocation);
                if ((rb.position - desiredLocation.loc).magnitude < 1)
                {
                    pathCompleted = true;
                }
            }
        }
        else if (worldClock.GetComponent<TimeManager>().TotalGameHour == 8 && worldClock.GetComponent<TimeManager>().TotalGameMin == 30)
        {
            if (!pathCompleted)
            {
                desiredLocation = home;
                HandleMovement(desiredLocation);
                if ((rb.position - desiredLocation.loc).magnitude < 1)
                {
                    pathCompleted = true;
                }
            }
            else
            {
                RandomBehavior();
            }
        }

    }


}
