using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyMovementManager : MonoBehaviour {
    Path currentPath;

    public Rigidbody2D rb;
    public Rigidbody2D playerRB;
    public Animator animator;
    Seeker seeker;

    private bool onPlayerPath;
    private bool onRandomPath;
    private bool randomMovement;

    protected float idleTime;
    protected float randIdleTime;
    protected float nextWaypointDistance = .5f;
    protected float moveSpeed;
    protected float visualDistance;

    protected float minRandDist;
    protected float maxRandDist;
    protected bool randHorizontal;
    protected bool randVertical;
    private Vector2 currentRandDest;

    private float idleDelta;
    private float movementDelta;
    private int currentWaypoint;

    protected float moveTime;
    private float moveDelta;

    private float vertical = 0;
    private float horizontal = 0;

    private bool isIdle;

    //new variables
    protected float strafeSpeed;
    protected Vector2 strafeOffset;
    private bool reachedStrafeDestination = true;

    protected float minPlayerOffset;
    protected float maxPlayerOffset;
    private bool inRangeZone;


    void Start() {
        Initialize();
    }

    /// <summary>
    /// Presets components necessary off of the enemy object.
    /// </summary>
    protected void Initialize() {
        seeker = this.GetComponent<Seeker>();
        //get child object
        GameObject ChildGameObject1 = transform.GetChild(0).gameObject;
        animator = ChildGameObject1.GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody2D>();
        playerRB = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
        moveDelta = -1;
        onPlayerPath = false;
        onRandomPath = false;
    }

    /// <summary>
    /// Create a random location to travel to and assign it to the currentRandLoc variable
    /// </summary>
    /// <returns>random location for the enemy to move to</returns>
    void GetRandomDestination() {
        int dir;
        if(randVertical && randHorizontal) {
            dir = Random.Range(0, 4);
        } else if(randVertical) {
            dir = Random.Range(2, 4);
        } else {
            dir = Random.Range(0, 2);
        }
        float dist = Random.Range(minRandDist, maxRandDist + 1);
        switch (dir) {
            case 0:
                currentRandDest = new Vector2(rb.position.x + dist, rb.position.y);
                break;
            case 1:
                currentRandDest = new Vector2(rb.position.x - dist, rb.position.y);
                break;
            case 2:
                currentRandDest = new Vector2(rb.position.x, rb.position.y + dist);
                break;
            case 3:
                currentRandDest = new Vector2(rb.position.x, rb.position.y - dist);
                break;
        }
        randomMovement = true;
    }

    /// <summary>
    /// Creates a path towards provided Vector2 location.
    /// </summary>
    /// <param name="loc">location for the path to end at</param>
    protected void MoveToLoc(Vector2 loc) {
        if (seeker.IsDone()) {
            seeker.StartPath(rb.position, loc, OnPathComplete);
        }
    }

    /// <summary>
    /// Called once a path has been formed. Determines whether this is a random path or targeted path towards the player.
    /// </summary>
    /// <param name="p"></param>
    void OnPathComplete(Path p) {
        if (!p.error) {
            currentPath = p;
            currentWaypoint = 1;
            if (randomMovement) {
                if (onPlayerPath) {
                    isIdle = true;
                    reachedStrafeDestination = true;
                    onPlayerPath = false;
                }
                onRandomPath = true;
                moveDelta = -1;
            } else {
                if (moveDelta < 0) {
                    moveDelta = 0;
                    isIdle = true;
                }
                onPlayerPath = true;
                onRandomPath = false;
            }
        }
    }

    /// <summary>
    /// Checks whether the path needs to be updated.
    /// </summary>
    /// <returns>
    /// True if the path has not been updated within the past .5 seconds
    /// False otherwise
    /// </returns>
    protected bool UpdatePlayerTargetPathDelta() {
        if(movementDelta >= .5) {
            movementDelta = 0;
            return true;
        } else {
            movementDelta += Time.fixedDeltaTime;
            return false;
        }
    } 

    /// <summary>
    /// Checks if the enemy is ready to exit idle while in a random movement state
    /// </summary>
    /// <returns>
    /// True if the enemy has been idle for randIdleTime
    /// False otherwise
    /// </returns>
    bool CheckRandomIdleDelta() {
        if(idleDelta >= randIdleTime) {
            idleDelta = 0;
            return true;
        } else {
            idleDelta += Time.fixedDeltaTime;
            return false;
        }
    }

    /// <summary>
    /// Checks if the enemy is ready to exit idle while in a player target movement state
    /// </summary>
    /// <returns>
    /// True if the enemy has been idle for idleTime
    /// False otherwise
    /// </returns>
    bool CheckPlayerTargetIdleDelta() {
        if(idleDelta >= idleTime) {
            idleDelta = 0;
            return true;
        } else {
            idleDelta += Time.fixedDeltaTime;
            return false;
        }
    }

    /// <summary>
    /// Checks if the enemy is ready to go idle while in player target movement state
    /// </summary>
    /// <returns>
    /// True if enemy has been moving for moveTime
    /// False otherwise
    /// </returns>
    bool CheckMoveDelta() {
        if (moveDelta >= moveTime) {
            isIdle = true;
            return true;
        } else {
            moveDelta += Time.fixedDeltaTime;
            isIdle = false;
            return false;
        }
    }

    /// <summary>
    /// Checks the distance between the enemy and the players current location. Distance is returned squared as it is faster to process.
    /// </summary>
    /// <returns>
    /// The distance between the player and the enemy squared.
    /// </returns>
    protected float DistanceToPlayerSquared() {
        return (rb.position - playerRB.position).sqrMagnitude;
    }

    /// <summary>
    /// Function for random behavior of the enemy. A random direction is created determined by GetRandomDestination() and a path is created.
    /// </summary>
    protected void RandomBehavior() {
        if (!onRandomPath) {
            if (CheckRandomIdleDelta()) {
                GetRandomDestination();
                MoveToLoc(currentRandDest);
            }
        } else {
            isIdle = false;
            float distance = Vector2.Distance(rb.position,
                currentPath.vectorPath[currentWaypoint]);
            if (distance < nextWaypointDistance) {
                //need to update waypoint
                currentWaypoint++;
            }
            if (currentWaypoint >= currentPath.vectorPath.Count) {
                //reach destination
                onRandomPath = false;
                randomMovement = false;
                return;
            }
            //currently on random path, so continue moving
            rb.position = Vector2.MoveTowards(rb.position,
                (Vector2)currentPath.vectorPath[currentWaypoint], moveSpeed * Time.fixedDeltaTime);
            return;
        }
    }

    /// <summary>
    /// Function for standard player targeting behavior with a melee enemy. A path is formed towards the player and this path is followed.
    /// </summary>
    protected void MeleePlayerTargetBehavior() {
        if (UpdatePlayerTargetPathDelta() && (rb.position - playerRB.position).magnitude > .5f) {
            randomMovement = false;
            MoveToLoc(playerRB.position);
        }
        if (onPlayerPath) {
            //check distance between here and next waypoint
            float distance = Vector2.Distance(rb.position,
            currentPath.vectorPath[currentWaypoint]);
            if (distance < nextWaypointDistance) {
                //check if need to increase currentwaypoint
                if (currentWaypoint >= currentPath.vectorPath.Count - 1 || (rb.position-playerRB.position).magnitude < .5f) {
                    onPlayerPath = false;
                    return;
                } else {
                    currentWaypoint++;
                }
            } else {
                if (CheckMoveDelta()) {
                    if (CheckPlayerTargetIdleDelta()) {
                        moveDelta = 0;
                    }
                } else {
                    rb.position = Vector2.MoveTowards(rb.position,
                    (Vector2)currentPath.vectorPath[currentWaypoint], moveSpeed * Time.fixedDeltaTime);
                }
            }
        }
    }


    /// <summary>
    /// Gets the offset coordinates between the enemy and the player based on provided offset
    /// </summary>
    /// <param name="offset">offset from player location</param>
    /// <returns>
    /// The Vector2 coordinates for the offset location between the enemy and the player
    /// </returns>
    Vector2 GetOffsetCoords(float offset) {
        //check if x and y offset are negative or positive

        //get angle between in radians
        float theta = Mathf.Asin(Mathf.Pow((rb.position.x-playerRB.position.x), 2)/DistanceToPlayerSquared());
        //use sin to find x'
        float xPrime = offset * Mathf.Sin(theta);
        //use cos to find y'
        float yPrime = offset * Mathf.Cos(theta);

        if (rb.position.x - playerRB.position.x < 0) {
            xPrime = -xPrime;
        }
        if (rb.position.y - playerRB.position.y < 0) {
            yPrime = -yPrime;
        }

        //returning value is wrong, should be primes + playerRB
        Vector2 loc = new Vector2();
        loc.x = xPrime+playerRB.position.x;
        loc.y = yPrime+playerRB.position.y;

        return loc;
    }

    protected void RangedPlayerTargetBehavior() {
        if (UpdatePlayerTargetPathDelta()) {
            randomMovement = false;
            //this if statement will be pretty tricky
            if((rb.position - GetOffsetCoords(minPlayerOffset)).magnitude <= 1) {
                //if enemy is within .5 of minOffset set inRange = true
                inRangeZone = true;
            }
            if((rb.position - playerRB.position).magnitude > (GetOffsetCoords(maxPlayerOffset)-playerRB.position).magnitude) {
                //if enemy is further than max offset set inRange  = false
                inRangeZone = false;
            }
            if (!inRangeZone) {
                //outside of maxOffset, move towards minOffset
                //next statement necessary for going back into strafing after targeting player
                reachedStrafeDestination = true;
                MoveToLoc(GetOffsetCoords(minPlayerOffset));
            }
        }
        if(inRangeZone) {
            //do strafing behavior
                if(reachedStrafeDestination == true) {
                    //get
                    //this has a big flaw
                    //need to keep same distance and angle but update the path
                    SetStrafeOffset(GetOrientationQuadrant());
                    MoveToLoc(new Vector2(playerRB.position.x+strafeOffset.x, playerRB.position.y+strafeOffset.y));
                } else {
                    MoveToLoc(new Vector2(playerRB.position.x + strafeOffset.x, playerRB.position.y + strafeOffset.y));
                    if((rb.position - (new Vector2(playerRB.position.x + strafeOffset.x, playerRB.position.y + strafeOffset.y))).magnitude < .5) {
                        reachedStrafeDestination = true;
                    }
                    rb.position = Vector2.MoveTowards(rb.position,
                    (Vector2)currentPath.vectorPath[currentWaypoint], strafeSpeed * Time.fixedDeltaTime);
                    return;
                }
        }
        if (onPlayerPath) {
            //check distance between here and next waypoint
            float distance = Vector2.Distance(rb.position,
            currentPath.vectorPath[currentWaypoint]);
            if (distance < nextWaypointDistance) {
                //check if need to increase currentwaypoint
                if (currentWaypoint >= currentPath.vectorPath.Count - 1 || (rb.position - playerRB.position).magnitude < .5f) {
                    onPlayerPath = false;
                    return;
                } else {
                    currentWaypoint++;
                }
            } else {
                if (CheckMoveDelta()) {
                    if (CheckPlayerTargetIdleDelta()) {
                        moveDelta = 0;
                    }
                } else {
                    rb.position = Vector2.MoveTowards(rb.position,
                    (Vector2)currentPath.vectorPath[currentWaypoint], moveSpeed * Time.fixedDeltaTime);
                }
            }
        }
    }

    /// <summary>
    /// Returns the orientation with which loc1 is with respect to loc2 on an 8 quadrant split
    /// </summary>
    /// <param name="loc1">first vecotr2</param>
    /// <param name="loc2">second vector2</param>
    /// <returns>
    /// 0-up-right
    /// 1-down-right
    /// 2-down-left
    /// 3-up-left
    /// </returns>
    protected int GetOrientationQuadrant() {
        float xDiff = rb.position.x - playerRB.position.x;
        float yDiff = rb.position.y - playerRB.position.y;
        //logic is wrong
        if(xDiff < 0) {
            //left
            if(yDiff > 0) {
                //up-left
                if(rb.position.y < playerRB.position.y+Mathf.Abs(xDiff)) {
                    //rb.y > playerRB.y+xDiff
                    //Q7
                    return 3;
                } else {
                    //Q8
                    return 3;
                }
            } else {
                //down-left
                if(rb.position.x < playerRB.position.x + yDiff) {
                    //Q6
                    return 2;
                } else {
                    //Q5
                    return 2;
                }
            }
        } else {
            //right
            if(yDiff > 0) {
                //up-right
                if(rb.position.y < playerRB.position.y+xDiff) {
                    //Q2
                    return 0;
                } else {
                    //Q1
                    return 0;
                }
            } else {
                //down-right
                if(rb.position.x < playerRB.position.x+Mathf.Abs(yDiff)) {
                    //Q4
                    return 1;
                } else {
                    //Q3
                    return 1;
                }
            }
        }
        
    }

    /// <summary>
    /// Finds a random location within the arc surrounding the player within the bounds of minPlayerOffset and maxPlayeroffset
    /// </summary>
    /// <param name="dir">direction for which the player is oriented towards the player</param>
    protected void SetStrafeOffset(int dir) {
        //logic is on point
        Debug.Log(dir);
        float angle = 0;
        float distance;
        distance = Random.Range(minPlayerOffset, maxPlayerOffset);
        switch(dir) {
            case 0:
                angle = Random.Range(11*Mathf.PI/6, 8*Mathf.PI/3);
                break;
            case 1:
                angle = Random.Range(4*Mathf.PI/3, 13*Mathf.PI/6);
                break;
            case 2:
                angle = Random.Range(5*Mathf.PI/6, 5* Mathf.PI/3);
                break;
            case 3:
                angle = Random.Range(Mathf.PI/3, 7*Mathf.PI/6);
                break;
        }
        reachedStrafeDestination = false;
        strafeOffset = new Vector2(distance*Mathf.Cos(angle), distance*Mathf.Sin(angle));
    }

    protected void StandardMovementBehavior() {
        if (DistanceToPlayerSquared() > Mathf.Pow(visualDistance, 2)) {
            //cannot see player
            RandomBehavior();
        } else {
            //target player
            MeleePlayerTargetBehavior();
        }
    }

    protected void LoopedAnimation() {
        if (currentPath != null) {
            if (currentWaypoint < currentPath.vectorPath.Count - 1) {
                horizontal = currentPath.vectorPath[currentWaypoint].x - rb.position.x;
                vertical = currentPath.vectorPath[currentWaypoint].y - rb.position.y;
            }
        }
        //checking if next movement-if so removes one direction for animation 
        if (Mathf.Abs(horizontal) > Mathf.Abs(vertical)) {
            vertical = 0;
        } else {
            horizontal = 0;
        }

        if (!onPlayerPath && !onRandomPath) {
            animator.SetBool("Move_U", false);
            animator.SetBool("Move_R", false);
            animator.SetBool("Move_L", false);
            animator.SetBool("Move_D", false);
            return;
        }
        //Here need to set back to idle if coming off of a movement period into an idle one 
        if (horizontal != 0) {
            //moving horizontal
            if (horizontal > 0) {
                //moving right
                animator.SetBool("Move_U", false);
                animator.SetBool("Move_D", false);
                animator.SetBool("Move_L", false);
                animator.SetBool("Move_R", true);
            } else {
                //moving left
                animator.SetBool("Move_U", false);
                animator.SetBool("Move_D", false);
                animator.SetBool("Move_R", false);
                animator.SetBool("Move_L", true);
            }
        } else {
            //moving vertical
            if (vertical > 0) {
                animator.SetBool("Move_R", false);
                animator.SetBool("Move_D", false);
                animator.SetBool("Move_L", false);
                animator.SetBool("Move_U", true);
            } else {
                animator.SetBool("Move_U", false);
                animator.SetBool("Move_R", false);
                animator.SetBool("Move_L", false);
                animator.SetBool("Move_D", true);
            }
        }
    }

    protected void NonLoopedAnimation() {
        if (currentPath != null) {
            if (currentWaypoint < currentPath.vectorPath.Count - 1) {
                horizontal = currentPath.vectorPath[currentWaypoint].x - rb.position.x;
                vertical = currentPath.vectorPath[currentWaypoint].y - rb.position.y;
            }
        }
        //checking if next movement-if so removes one direction for animation 
        if (Mathf.Abs(horizontal) > Mathf.Abs(vertical)) {
            vertical = 0;
        } else {
            horizontal = 0;
        }

        if ((!onPlayerPath && !onRandomPath) || isIdle) {
            animator.SetBool("Move_U", false);
            animator.SetBool("Move_R", false);
            animator.SetBool("Move_L", false);
            animator.SetBool("Move_D", false);
            return;
        }
        if(onPlayerPath) {
            //determine if idle, if so set to idle
        }
        if (horizontal != 0) {
            //moving horizontal
            if (horizontal > 0 && MovementAnimationActive()==false) {
                //moving right
                animator.SetBool("Move_U", false);
                animator.SetBool("Move_D", false);
                animator.SetBool("Move_L", false);
                animator.SetBool("Move_R", true);
            } else if(MovementAnimationActive()==false) {
                //moving left
                animator.SetBool("Move_U", false);
                animator.SetBool("Move_D", false);
                animator.SetBool("Move_R", false);
                animator.SetBool("Move_L", true);
            }
        } else {
            //moving vertical
            if (vertical > 0 && MovementAnimationActive()==false) {
                //moving up
                animator.SetBool("Move_R", false);
                animator.SetBool("Move_D", false);
                animator.SetBool("Move_L", false);
                animator.SetBool("Move_U", true);
            } else if(MovementAnimationActive()==false) {
                //moving down
                animator.SetBool("Move_U", false);
                animator.SetBool("Move_R", false);
                animator.SetBool("Move_L", false);
                animator.SetBool("Move_D", true);
            }
        }
    }

    bool MovementAnimationActive() {
        if(animator.GetBool("Move_U")||animator.GetBool("Move_D")||animator.GetBool("Move_L")|| animator.GetBool("Move_R")) {
            return true;
        } else {
            return false;
        }
    }

}
