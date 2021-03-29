//using System.Collections;
//using System.Collections.Generic;
//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;
//using UnityEngine;
//using Unity.Collections;
//using Pathfinding;

//public class ShopManager : LEntityManager {

//    private int currency;

//    void MoveToLoc(Vector2 loc) {
//        onPath = true;
//        seeker.StartPath(rb.position, loc, OnPathComplete);
//    }

//    void OnPathComplete(Path p) {
//        if(!p.error) {
//            currentPath = p;
//            currentWaypoint = 0;
//        }
//    }

//    void FixedUpdate() {
//        if(onPath == false && tm.TotalGameHour == 0 && tm.TotalGameMin == 12) {
//            MoveToLoc(new Vector2(-3, 2));
//        }
//        if(currentPath == null) {
//            return;
//        }
//        if (currentWaypoint >= currentPath.vectorPath.Count) {
//            reachedDestination = true;
//        } else {
//            reachedDestination = false;
//        }

//        Vector2 dir = ((Vector2) currentPath.vectorPath[currentWaypoint] - rb.position).normalized;
//        Vector2 force = dir * moveSpeed * Time.deltaTime;

//        rb.AddForce(force);

//        float distance = Vector2.Distance(rb.position, currentPath.vectorPath[currentWaypoint]);

//        if(distance < nextWaypointDistance) {
//            currentWaypoint++;
//        }

//    }

//}
