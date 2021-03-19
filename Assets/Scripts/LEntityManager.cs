using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;
using Unity.Collections;
using Pathfinding;

public class LEntityManager : MonoBehaviour {
    /**
    Path currentPath;
    Vector2 currentDest;
    int currentWaypoint = 0;
    bool reachedDestination = false;
    bool onPath;
    float moveSpeed = 2;
    float nextWaypointDistance = .3f;
    TimeManager tm;
    public Rigidbody2D rb;

    Seeker seeker;

    bool updatePath = false;
    
    void Start() {
        seeker = this.GetComponent<Seeker>();
        tm = GameObject.FindWithTag("MainCamera").GetComponent<TimeManager>();
        MoveToLoc(new Vector2(0, 0));
    }

    void MoveToLoc(Vector2 loc) {
        //currentDest = loc;
        onPath = true;
        //still getting called
        //problem is seeker, may need to clear the seeker
        seeker.StartPath(rb.position, loc, OnPathComplete);
    }

    void OnPathComplete(Path p) {
        //only getting called once
        //somehow works on collision
        if (!p.error) {
            currentPath = p;
            currentWaypoint = 0;
        }
    }

    void FixedUpdate() {
        if (onPath == false && tm.TotalGameHour == 0 && tm.TotalGameMin == 12) {
            
        }
        if (currentPath == null) {
            return;
        }
        
        if(onPath) {
            if (currentWaypoint >= currentPath.vectorPath.Count) {
                reachedDestination = true;
                onPath = false;
                //currentDest = transform.position;
            } else {
                reachedDestination = false;
                rb.position = Vector2.MoveTowards(rb.position, 
                    (Vector2)currentPath.vectorPath[currentWaypoint], moveSpeed*Time.deltaTime);

                float distance = Vector2.Distance(rb.position, currentPath.vectorPath[currentWaypoint]);

                if (distance < nextWaypointDistance) {
                    currentWaypoint++;
                }
            }
            
            if(updatePath) {
                MoveToLoc(currentDest);
                updatePath = false;
            }

        }
    }


    void Update() {
    }

    void OnCollisionEnter2D(Collision2D col) {
        if(col.gameObject.tag=="Player" || col.gameObject.tag=="NPC") {
            if(onPath) {
                updatePath = true;
            }
            
        }
        
    }
    */
}