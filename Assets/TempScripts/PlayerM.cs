using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerM : MonoBehaviour
{

    public PlayerCombat playerCombat;
    public Animator animator;
    public string orientation;

    private float holdTimeDelta = 0;

    //hitRange values will need to be retrieve from the current active weapon
    private float hitRange = .6f;
    private float bowHitRange = 10;

    bool rangedInUse = false;
    bool meleeInUse = false;

    [SerializeField] private GameObject poisonArrow;

    public float HitRange
    {
        get{
            return hitRange;
        }
        set{
            hitRange = value;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(animator.GetBool("Melee") == true)
        {
            meleeInUse = true;
            rangedInUse = false;
        } else
        {
            rangedInUse = true;
            meleeInUse = false;
        }
            


        if (Input.GetMouseButtonDown(0) && meleeInUse)
	    {
            SetOrientation(); 
	        playerCombat.MeleeAttack(this.gameObject, GameObject.FindGameObjectsWithTag("Enemy"), orientation);
            
        } else if((Input.GetMouseButton(0) && rangedInUse))
        {
            SetOrientation();
            holdTimeDelta += Time.deltaTime;
            //Debug.Log("Melee not in use");
           

        }
        //launching arrow
        if(Input.GetMouseButtonUp(0) && rangedInUse)
        {
            float dist = 0;
            if (holdTimeDelta >= 1)
                dist = bowHitRange;
            else
                dist = holdTimeDelta * bowHitRange;

            //create arrow and set its destination and start location
            //additionally set circle collider to be on the head of arrow
            Debug.Log("Instantiate");
            GameObject arrow = Instantiate(poisonArrow, transform.position, Quaternion.identity);
            float x = 0;
            float y = 0;
            switch (orientation)
            {
                case "Up":
                    arrow.GetComponent<Arrow>().destination = new Vector2(transform.position.x, transform.position.y+dist);
                    arrow.GetComponent<CircleCollider2D>().offset = new Vector2(-.01f, .37f);
                    arrow.GetComponent<Arrow>().orientation = 0;
                    break;
                case "Down":
                    arrow.GetComponent<Arrow>().destination = new Vector2(transform.position.x, transform.position.y - dist);
                    arrow.GetComponent<CircleCollider2D>().offset = new Vector2(.01f, -.37f);
                    arrow.GetComponent<Arrow>().orientation = 4;
                    break;
                case "Left":
                    arrow.GetComponent<Arrow>().destination = new Vector2(transform.position.x-dist, transform.position.y);
                    arrow.GetComponent<CircleCollider2D>().offset = new Vector2(-.3f, -.02f);
                    arrow.GetComponent<Arrow>().orientation = 6;
                    break;
                case "Right":
                    arrow.GetComponent<Arrow>().destination = new Vector2(transform.position.x+dist, transform.position.y);
                    arrow.GetComponent<CircleCollider2D>().offset = new Vector2(.37f, .01f);
                    arrow.GetComponent<Arrow>().orientation = 2;
                    break;
                case "UpRight":
                    y = dist * Mathf.Sin(45 * Mathf.Deg2Rad);
                    x = dist * Mathf.Cos(45 * Mathf.Deg2Rad);
                    arrow.GetComponent<Arrow>().destination = new Vector2(transform.position.x + x, transform.position.y+ y);
                    arrow.GetComponent<CircleCollider2D>().offset = new Vector2(.23f, .25f);
                    arrow.GetComponent<Arrow>().orientation = 1;
                    break;
                case "DownRight":
                    y = dist * Mathf.Sin(315 * Mathf.Deg2Rad);
                    x = dist * Mathf.Cos(315 * Mathf.Deg2Rad);
                    arrow.GetComponent<Arrow>().destination = new Vector2(transform.position.x + x, transform.position.y+y);
                    arrow.GetComponent<CircleCollider2D>().offset = new Vector2(.27f, -.23f);
                    arrow.GetComponent<Arrow>().orientation = 3;
                    break;
                case "UpLeft":
                    y = dist * Mathf.Sin(135 * Mathf.Deg2Rad);
                    x = dist * Mathf.Cos(135 * Mathf.Deg2Rad);
                    arrow.GetComponent<Arrow>().destination = new Vector2(transform.position.x + x, transform.position.y+y);
                    arrow.GetComponent<CircleCollider2D>().offset = new Vector2(-.26f, .23f);
                    arrow.GetComponent<Arrow>().orientation = 7;
                    break;
                case "DownLeft":
                    y = dist * Mathf.Sin(225 * Mathf.Deg2Rad);
                    x = dist * Mathf.Cos(225 * Mathf.Deg2Rad);
                    arrow.GetComponent<Arrow>().destination = new Vector2(transform.position.x + x, transform.position.y+y);
                    arrow.GetComponent<CircleCollider2D>().offset = new Vector2(-.22f, -.26f);
                    arrow.GetComponent<Arrow>().orientation = 5;
                    break;
            }




            holdTimeDelta = 0;
        }
         
    }

    public void SetOrientation()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Up"))
        {
            orientation = "Up";
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Down"))
        {
            orientation = "Down";
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Left"))
        {
            orientation = "Left";
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Right"))
        {
            orientation = "Right";
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("UpRight"))
        {
            orientation = "UpRight";
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("UpLeft"))
        {
            orientation = "UpLeft";
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("DownRight"))
        {
            orientation = "DownRight";
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("DownLeft"))
        {
            orientation = "DownLeft";
        }
    }




}
