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

    Vector2 movement;

    [SerializeField]
    private float diagonalTime;

    void Start() {
        animator = GetComponent<Animator>();
        animator.SetBool("Melee", true);
    }

    //create a property for attacking

    // Update is called once per frame
    void Update()
    {
       


    private float diagonalDelta = 0;

    void Update() {

        //changing melee to non-melee
        
        if (Input.GetKeyDown(KeyCode.Q)) {
                if (animator.GetBool("Melee")) {
                    animator.SetBool("Melee", false);
                } else {
                    animator.SetBool("Melee", true);
                }
        }
        
        movement.x = Input.GetAxisRaw("Horizontal"); // returns -1 or 1 dpending on horizontal movment
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);

        animator.SetFloat("Vertical", movement.y);

        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    void FixedUpdate()
    {
        // movement
        if (animator.GetBool("Blocking") == false && animator.GetBool("Attacking") == false) {
            if(animator.GetBool("Roll")) {
                        rb.MovePosition(rb.position + movement.normalized * rollSpeed * Time.fixedDeltaTime);
                    } else {
                        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
                    
                }
        }
        
        
        
    }

    Vector2 GetMouseLoc() {
        Vector2 mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
        return mouseWorldPos;
    }


}
