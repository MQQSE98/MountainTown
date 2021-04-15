using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GloopManager : EnemyMovementManager
{
    // Start is called before the first frame update
    void Start()
    {
        idleTime = .5f;
        randIdleTime = .5f;
        moveSpeed = 2f;
        visualDistance = 3f;
        minRandDist = 2;
        maxRandDist = 3;
        randHorizontal = true;
        randVertical = true;
        moveTime = 1;
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        NonLoopedAnimation();
    }

    void FixedUpdate() {
        /**
        if(DistanceToPlayerSquared() <= Mathf.Pow(visualDistance, 2)) {
            RangedPlayerTargetBehavior();
        } else {
            RandomBehavior();
        }
        */
        

        StandardMovementBehavior();
    }

}
