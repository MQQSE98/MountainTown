using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieManager : EnemyMovementManager
{
    // Start is called before the first frame update
    void Start() {
        idleTime = 0;
        randIdleTime = 2;
        moveSpeed = 3f;
        visualDistance = 8;
        minRandDist = 1;
        maxRandDist = 1;
        randHorizontal = false;
        randVertical = true;
        moveTime = 1;

        minPlayerOffset = 2;
        maxPlayerOffset = 6;
        strafeSpeed = 3; 

        Initialize();
    }

    // Update is called once per frame
    void Update() {
        LoopedAnimation();
    }

    void FixedUpdate() {
        //StandardMovementBehavior();
        if (DistanceToPlayerSquared() > Mathf.Pow(visualDistance, 2)) {
            //cannot see player
            RandomBehavior();
        } else {
            //target player
            RangedPlayerTargetBehavior();
        }
    }
}
