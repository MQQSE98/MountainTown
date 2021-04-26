using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SSBOSSMovementControl : MonoBehaviour
{

    public float moveSpeed = 2f;

    private float attackDelta;

    public Rigidbody2D rb;
    public Animator animator;
    public int strategy;
    public int direction;
    public int timer;
    public int holdup = 0;
    //public int wait = 0;
    public bool wasFlipped;

    public float attackDistance;
    public float bufferDistance;
    public Transform target;

    public float oldX = 0;
    public float newX = 0;
    public float oldY = 0;
    public float newY = 0;

    public int HP = 10;
    public int atk = 10;
    public int def = 1;

    public GameObject self;

    public float normRange = 3.2f;
    public float biteRange = 4f;

    public bool dead = false;
    public bool freeMove = true;
    public bool isActive = false;


    Vector2 movement;


    private void Awake()
    {
        
    }
    void Start()
    {
        animator = GetComponent<Animator>();
        //locate the player
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        self = this.gameObject;
        //StartCoroutine(deathCheck());

    }

    private void Event_CanAttackAgain()
    {
        // This was my test case, you can add anything you need to do after the animation has finished here
        // Like load a new scene etc
        freeMove = true;
    }

    // Update is called once per frame

    void Update()
    {

        if (isActive)
        {
            doBossStuff();
        }
        else
        {
            checkActive();
        }
    }

    private void checkActive()
    {
        if ((this.transform.position - target.position).sqrMagnitude <= Mathf.Pow(20F, 2))
        {
            isActive = true;
        }
    }
    private void doBossStuff()
    {
        Debug.Log("Distance between player and boss: " + (target.position - this.transform.position));
        //TEST CASE
        if (animator.GetBool("Death") && holdup < 300 /*&& dead == false*/)
        {
            animator.Play("SSDeath");
            //dead = true;
            holdup = 300;
            //Destroy(self);
        }
        /*else if (dead)
        {
            Destroy(self);
            
        }*/

        //random strategy

        strategy = Random.Range(0, 36);
        //test case
        strategy = 9;
        direction = Random.Range(0, 4);
        //test case
        direction = 1;
        if (holdup == 0 && animator.GetBool("Death") == false && freeMove == true)
        {
            this.gameObject.GetComponent<EnemyM>().setRange(3);
            if (direction == 0) //for flipping directions
            {
                if (wasFlipped == false)
                {
                    transform.Rotate(0, 180f, 0);
                }
                wasFlipped = true;
            }
            else if (wasFlipped)
            {
                transform.Rotate(0, 180f, 0);
                wasFlipped = false;
            }

            if (strategy < 30) //bite
            {

                this.gameObject.GetComponent<EnemyM>().setRange(4);
                for (int t = 0; t < 10; t++) //chase player
                {


                    oldX = transform.position.x;
                    oldY = transform.position.y;
                    transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * .75f * Time.deltaTime);
                    newX = transform.position.x;
                    newY = transform.position.y;


                    if (newX - oldX > 0 && (newX - oldX) * (newX - oldX) >= (newY - oldY) * (newY - oldY))
                    {


                        if (wasFlipped == false)
                        {
                            transform.Rotate(0, 180f, 0);
                        }
                        wasFlipped = true;
                        animator.SetBool("Right", true);
                        animator.SetBool("Left", false);
                        animator.SetBool("Up", false);
                        animator.SetBool("Down", false);
                        animator.SetBool("Bite", true);
                        animator.SetBool("Jump", false);
                        animator.SetBool("Shoot", false);
                        //animator.PlayInFixedTime("SSBiteR");

                    }
                    else if (newX - oldX < 0 && (newX - oldX) * (newX - oldX) >= (newY - oldY) * (newY - oldY))
                    {


                        if (wasFlipped)
                        {
                            transform.Rotate(0, 180f, 0);
                            wasFlipped = false;
                        }
                        animator.SetBool("Right", false);
                        animator.SetBool("Left", true);
                        animator.SetBool("Up", false);
                        animator.SetBool("Down", false);
                        animator.SetBool("Bite", true);
                        animator.SetBool("Jump", false);
                        animator.SetBool("Shoot", false);
                        //animator.PlayInFixedTime("SSBiteL");
                    }
                    if (newY - oldY > 0 && (newX - oldX) * (newX - oldX) < (newY - oldY) * (newY - oldY))
                    {


                        if (wasFlipped)
                        {
                            transform.Rotate(0, 180f, 0);
                            wasFlipped = false;

                        }
                        animator.SetBool("Right", false);
                        animator.SetBool("Left", false);
                        animator.SetBool("Up", true);
                        animator.SetBool("Down", false);
                        animator.SetBool("Bite", true);
                        animator.SetBool("Jump", false);
                        animator.SetBool("Shoot", false);
                        //animator.PlayInFixedTime("SSBiteB");

                    }
                    else if (newY - oldY < 0 && (newX - oldX) * (newX - oldX) < (newY - oldY) * (newY - oldY))
                    {


                        if (wasFlipped)
                        {
                            transform.Rotate(0, 180f, 0);
                            wasFlipped = false;
                        }
                        animator.SetBool("Right", false);
                        animator.SetBool("Left", false);
                        animator.SetBool("Up", false);
                        animator.SetBool("Down", true);
                        animator.SetBool("Bite", true);
                        animator.SetBool("Jump", false);
                        animator.SetBool("Shoot", false);
                        //animator.PlayInFixedTime("SSBiteF");
                    }
                    //animator.SetBool("Bite", false);
                }
            }
            else if (strategy > 29 && strategy < 36) //jump
            {
                animator.SetBool("Bite", false);
                animator.SetBool("Jump", true);
                animator.SetBool("Shoot", false);
                if (direction == 0)
                {
                    movement.x = 10;
                    movement.y = 0;
                    animator.SetBool("Right", true);
                    animator.SetBool("Left", false);
                    animator.SetBool("Up", false);
                    animator.SetBool("Down", false);
                }
                else if (direction == 1)
                {
                    movement.x = -10;
                    movement.y = 0;
                    animator.SetBool("Right", false);
                    animator.SetBool("Left", true);
                    animator.SetBool("Up", false);
                    animator.SetBool("Down", false);
                }
                else if (direction == 2)
                {
                    movement.x = 0;
                    movement.y = 10;
                    animator.SetBool("Right", false);
                    animator.SetBool("Left", false);
                    animator.SetBool("Up", true);
                    animator.SetBool("Down", false);
                }
                else if (direction == 3)
                {
                    movement.x = 0;
                    movement.y = -10;
                    animator.SetBool("Right", false);
                    animator.SetBool("Left", false);
                    animator.SetBool("Up", false);
                    animator.SetBool("Down", true);
                }
            }
            else if (strategy > 35 && strategy < 50) //shoot
            {
                animator.SetBool("Bite", false);
                animator.SetBool("Jump", false);
                animator.SetBool("Shoot", true);
                //shoot particles

            }
            /*else if (strategy > 49) //roam
            {

                animator.SetBool("Bite", false);
                animator.SetBool("Jump", false);
                animator.SetBool("Shoot", false);

                for (timer = 0; timer < 2; timer++)
                {
                    if (direction == 0)
                    {
                        animator.SetBool("Right", true);
                        animator.SetBool("Left", false);
                        animator.SetBool("Up", false);
                        animator.SetBool("Down", false);
                        for (timer = 0; timer < 2; timer++)
                        {
                            movement.x = 3;
                            movement.y = 0;
                        }
                    }
                    else if (direction == 1)
                    {
                        animator.SetBool("Right", false);
                        animator.SetBool("Left", true);
                        animator.SetBool("Up", false);
                        animator.SetBool("Down", false);
                        for (timer = 0; timer < 2; timer++)
                        {
                            movement.x = -3;
                            movement.y = 0;
                        }
                    }
                    else if (direction == 2)
                    {
                        animator.SetBool("Right", false);
                        animator.SetBool("Left", false);
                        animator.SetBool("Up", true);
                        animator.SetBool("Down", false);
                        for (timer = 0; timer < 2; timer++)
                        {
                            movement.x = 0;
                            movement.y = 3;
                            animator.SetFloat("Horizontal", movement.x);
                            animator.SetFloat("Vertical", movement.y);
                        }
                    }
                    else if (direction == 3)
                    {
                        animator.SetBool("Right", false);
                        animator.SetBool("Left", false);
                        animator.SetBool("Up", false);
                        animator.SetBool("Down", true);
                        for (timer = 0; timer < 2; timer++)
                        {
                            movement.x = 0;
                            movement.y = -3;
                        }
                    }
                }
            }*/
            holdup = 20;

        }
        if (holdup < 300)
        {
            holdup--;
        }
        else
        {
            holdup++;
        }
        if (holdup == 400)
        {
            Destroy(self);
        }



        /**if ((Input.GetKeyDown(KeyCode.W) ^ Input.GetKeyDown(KeyCode.A) ^ Input.GetKeyDown(KeyCode.S) ^ Input.GetKeyDown(KeyCode.D)) && movement.x == 0 && movement.y == 0)
        {
            animator.SetBool("Diagonal", false);
        }**/
        //movement.x = Input.GetAxisRaw("Horizontal"); // returns -1 or 1 dpending on horizontal movment
        //movement.y = Input.GetAxisRaw("Vertical");
        /**movement.x = Random.Range(-1, 1); // returns -1 or 1 dpending on horizontal movment
        movement.y = Random.Range(-1, 1);

        animator.SetFloat("Horizontal", movement.x);

        animator.SetFloat("Vertical", movement.y);**/

        /**
        if (movement.x != 0 && movement.y != 0)
        {
            animator.SetBool("Diagonal", true);
        }
        else if (movement.x != 0 ^ movement.y != 0)
        {
            animator.SetBool("Diagonal", false);
                
        }
        */



        /**if (movement.x != 0 && movement.y != 0)
        {
            animator.SetBool("Diagonal", true);
        }
        else if (movement.x != 0 ^ movement.y != 0)
        {
            if (diagonalDelta > diagonalTime)
            {
                animator.SetBool("Diagonal", false);
                diagonalDelta = 0;
            }
            else
            {
                diagonalDelta += Time.deltaTime;
            }
        }

        CheckStrategy();**/
    }

    private bool isTouching; // determines if the player is touching
    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        isTouching = true;


        if (collision.gameObject.tag == "Player")
        {
            if (isTouching == true)
            {
                //GetComponent<AudioSource>().Play();
                HP -= 1;
                if (HP <= 0) 
                {
                    animator.SetBool("Death", true);
                    
                }
                isTouching = false;
            }//end of while


        } // of player tag check

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isTouching = false;
    }*/

    /*IEnumerator deathCheck()
    {
        if (animator.GetBool("Death"))
        {
            yield return new WaitForSeconds(5);
            
        }
    }*/
    public void DIE()
    {
        animator.SetBool("Death", true);
        //Play death anim here
        //animator.Play("SSDeath");
        //Play Explosion sound here.

        
        
    }


    void FixedUpdate()
    {
        
                rb.MovePosition(rb.position + movement/**.normalized**/ * moveSpeed * Time.fixedDeltaTime);



    }


}