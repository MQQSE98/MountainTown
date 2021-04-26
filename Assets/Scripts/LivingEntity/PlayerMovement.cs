using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 5f;
    public float rollSpeed = 10f;

    public Vector2 mousePos;
    private float attackDelta;

    public Rigidbody2D rb;
    public Animator animator;

    public bool dash;

    Vector2 movement;

    [SerializeField]
    private float diagonalTime;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("Melee", true);
    }

    //create a property for attacking

    // Update is called once per frame


    private float diagonalDelta = 0;

    void Update()
    {

        //changing melee to non-melee

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (animator.GetBool("Melee"))
            {
                animator.SetBool("Melee", false);
            }
            else
            {
                animator.SetBool("Melee", true);
            }
        }
        if ((Input.GetKeyDown(KeyCode.W) ^ Input.GetKeyDown(KeyCode.A) ^ Input.GetKeyDown(KeyCode.S) ^ Input.GetKeyDown(KeyCode.D)) && movement.x == 0 && movement.y == 0)
        {
            animator.SetBool("Diagonal", false);
        }
        movement.x = Input.GetAxisRaw("Horizontal"); // returns -1 or 1 dpending on horizontal movment
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);

        animator.SetFloat("Vertical", movement.y);

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



        if (movement.x != 0 && movement.y != 0)
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

        CheckDash();
        CheckAttacking();
        CheckBlocking();
    }

    void CheckDash()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            dash = true;
        }
        else
        {
            dash = false;
        }
    }
    //void CheckRolling()
    //{
    //    //Check if player is holding space
    //    if (Input.GetKey(KeyCode.Space))
    //    {
    //        animator.SetBool("Roll", true);
    //    }
    //    else
    //    {
    //        animator.SetBool("Roll", false);
    //    }
    //}


    void CheckAttacking()
    {
        if (animator.GetBool("Melee") == false)
        {

            if (Input.GetMouseButton(0))
            {
                animator.SetBool("BowAttacking", true);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                animator.SetBool("BowAttacking", false);
            }

        }
        else
        {

            //Primary Mouse initially clicked
            if (Input.GetMouseButtonDown(0))
            {
                animator.SetBool("Attacking", true);
                attackDelta = 0;
                mousePos = GetMouseLoc();
            }
            //Primary Mouse held down
            if (Input.GetMouseButton(0))
            {
                attackDelta += Time.deltaTime;
            }
            //Primary Mouse released
            if (Input.GetMouseButtonUp(0))
            {
                //Debug.Log("Time: "+delta);
                if (attackDelta < .25)
                {
                    animator.SetTrigger("Attack1");
                }
                else
                {
                    animator.SetTrigger("Attack2");
                }
            }
        }


    }

    void CheckBlocking()
    {
        //check if Secondary Mouse is active
        if (Input.GetMouseButton(1))
        {
            animator.SetBool("Blocking", true);
        }
        else
        {
            animator.SetBool("Blocking", false);
        }
    }

    void FixedUpdate()
    {
        // movement
        if (animator.GetBool("Blocking") == false && animator.GetBool("Attacking") == false)
        {
            if (dash == true)
            {
               
                rb.MovePosition(rb.position + movement.normalized * rollSpeed * Time.fixedDeltaTime);
            }
            else
            {
                rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);

            }
        }



    }

    Vector2 GetMouseLoc()
    {
        Vector2 mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
        return mouseWorldPos;
    }

}