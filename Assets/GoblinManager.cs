using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinManager : EnemyMovementManager
{
    // Start is called before the first frame update
    void Start()
    {
        idleTime = .5f;
        idleTime = .5f;
        randIdleTime = .5f;
        moveSpeed = 4f;
        visualDistance = 6f;
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

    private void FixedUpdate()
    {
        StandardMovementBehavior();
    }

}
