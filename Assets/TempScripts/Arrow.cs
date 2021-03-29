using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    public Vector2 destination;

    private PlayerCombat playerCombat;

    private float damage = 10;
    private float lifeCycleDelta = 0;
    private float lifeCycle = 7;
    public int orientation;

    private Rigidbody2D rb;
    private Animator animator;

    private bool isGrounded = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetInteger("direction", orientation);
    }

    // Update is called once per frame
    /// <summary>
    /// Checking every update whether the arrow has reached the destination, if not keep moving towards it
    /// </summary>
    void Update()
    {
        Debug.Log("Magnitude: "+ (rb.position - destination).magnitude);
        rb.position = Vector2.MoveTowards(rb.position, destination, 6 * Time.fixedDeltaTime);
        if((rb.position - destination).magnitude <= 1)
        {
            isGrounded = true;
            animator.SetBool("grounded", true);
            animator.SetInteger("direction", 12);
        }
    }

    /// <summary>
    /// Checking if arrow life cycle has reached its limit
    /// </summary>
    void FixedUpdate()
    {
        //playerCombat.RangedAttack(this.gameObject, GameObject.FindGameObjectsWithTag("Enemy"));
        if(lifeCycleDelta > lifeCycle)
        {
            Destroy(this.gameObject);
        } else
        {
            lifeCycleDelta += Time.fixedDeltaTime;
        }
    }

    /// <summary>
    /// Checks if arrow has hit an enemy while in the air
    /// </summary>
    /// <param name="col"></param>
    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Are we here?");
        if(col.gameObject.tag == "Enemy" && !isGrounded)
        {
            Debug.Log("hit");
            col.gameObject.GetComponent<EnemyM>().health -= 10;
            Destroy(this.gameObject);
        }
    }
}
