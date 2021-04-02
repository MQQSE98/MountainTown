/// <summary>
/// Manages the Unity player object.
/// </summary>
 
using UnityEngine;

public class PlayerManager : MonoBehaviour
{ 
    //public GameObject mainUIPanel = GameObject.Find("MainUIPanel");
    public CharacterSheet playerSheet;
    public GameObject inventoryPanel;
    public GameObject player;
    public PlayerMovement playerMovement;
    public PlayerCombat playerCombat;
    //public GameObject InventoryV2;

    public string orientation;

    //variables controlling health
    public float healthRegenValue = 0.001f;

    //variables controlling stamina
    public float stamConsumtionPercent = .0005f;
    public float stamRegenRate = 0.5f;
    public float stamConsumationRate; 

    //varibles controlling timers
    public float regenTimer = 0f;
    public float maxRegenTimer = 3f;

    public int alteredMoveSpeed = 6;
    public int defaultMoveSpeed = 3;

    public float holdTimeDelta;
    
    public ResourceController resourceController;
    public GameObject currentInteractableObject = null;
    public Animator animator;

    //melee / ranged SO data
    public MeleeWeapon currentMeleeWeapon;
    public RangedWeapon currentRangedWeapon;

    //player stamina states
    public bool isFatigued = false;
    public bool isDraining = false;

    public bool meleeInUse;
    public bool rangedInUse;


    public void Start()
    {

        //set all components 
        animator = gameObject.GetComponent<Animator>();
        playerMovement = gameObject.GetComponent<PlayerMovement>();
        playerCombat = gameObject.GetComponent<PlayerCombat>();

        //playerSheet.list.Clear();
        UpdatePanelSlots();
        playerSheet.currentHealth = playerSheet.maxHealth;
        resourceController.SetMaxHealth(playerSheet.maxHealth);

        playerSheet.currentStamina = playerSheet.maxStamina;
        resourceController.SetMaxStamina(playerSheet.maxStamina);
    }
    void Update()
    {
        TestDamage();
        Sprint();       
        PickUp();
        RegenStamina();
        RegenHealth();
        UpdatePanelSlots();
        Attack();
    }

    //Testing Methods for Resource Controller
    public void TakeDamage(int damage)
    {
        playerSheet.currentHealth -= damage;
        resourceController.SetHealth(playerSheet.currentHealth);
        if (playerSheet.currentHealth <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        Debug.Log("Sorry, you have died :((");
    }

    void Sprint()
    {
        print(playerSheet.currentStamina);
        stamConsumationRate = playerSheet.maxStamina * stamConsumtionPercent;

        if(Input.GetKey(KeyCode.G) && playerSheet.currentStamina >= 5)
        {        
            playerMovement.moveSpeed = 8;
            playerSheet.currentStamina -= 2;
            resourceController.SetStamina(playerSheet.currentStamina);
            isDraining = true;
            isFatigued = true;           
        }
        else
        {
            playerMovement.moveSpeed = defaultMoveSpeed;
            isDraining = false;
        }   
    }
    
    void RegenStamina()
    {
        if(playerSheet.currentStamina < playerSheet.maxStamina && isFatigued == false && isDraining == false)
        {
            playerSheet.currentStamina += stamRegenRate;
            resourceController.SetStamina(playerSheet.currentStamina);

        } else if(isFatigued == true)
        {
            RegenTimer();
        }
    }

    void RegenTimer()
    {
        if (regenTimer <= maxRegenTimer)
        {
            regenTimer += Time.deltaTime;
            isFatigued = true;
        }
        else if (regenTimer >= maxRegenTimer)
        {
            regenTimer = 0f;
            isFatigued = false;
        }
    }
        
    void RegenHealth()
    {
       if(playerSheet.currentHealth < playerSheet.maxHealth)
       {
            playerSheet.currentHealth += healthRegenValue;
            resourceController.SetHealth(playerSheet.currentHealth);
       }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        UnityEngine.Debug.Log(other.name);
        currentInteractableObject = other.gameObject;      
    }

    void OnTriggerExit2D(Collider2D other)
    {        
         currentInteractableObject = null;
         UnityEngine.Debug.Log(other.name);     
    }

    //HERE !! -------------------------------------------------------------------------------------
    public void PickUp()
    {
        if(Input.GetButtonDown("Interact") && currentInteractableObject)
        {
            GameObject itemObject = currentInteractableObject;

            Item itemPickedUp = itemObject.GetComponent<ItemManager>().item;

            Debug.Log("Current item is " + itemPickedUp.ItemName);

            if(itemPickedUp)
            {
                playerSheet.bag.Add(itemPickedUp);
                Destroy(itemObject);
                UpdatePanelSlots();
                Debug.Log("Pickup Successful");
            }
            else
            {
                Debug.Log("Item not picked  up");
            }
        }
    }

    public void TestDamage()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(20);
        }
    }
 
    public void Heal(float amount)
    {
        playerSheet.currentHealth += amount;

        if(playerSheet.currentHealth > playerSheet.maxHealth)
        {
            playerSheet.currentHealth = playerSheet.maxHealth; 
        }
        resourceController.SetHealth(playerSheet.currentHealth);
    }

    public void UpdatePanelSlots()
    {
        int index = 0;
        foreach(Transform child in inventoryPanel.transform)
        {
            InventorySlotController slot = child.GetComponent<InventorySlotController>();

            if(index < playerSheet.bag.Count)
            {
                slot.item = playerSheet.bag[index];
                //slot.amount = 1;                
            }
            else
            {
                slot.item = null;
            }

            slot.UpdateInfo();          
            index++;
        }
    }

    //TODO: do not run this if inventor is open 
    public void Attack()
    {
        if (animator.GetBool("Melee") == true)
        {
            meleeInUse = true;
            rangedInUse = false;
        }
        else
        {
            rangedInUse = true;
            meleeInUse = false;
        }

        if (Input.GetMouseButtonDown(0) && meleeInUse && currentMeleeWeapon != null && !inventoryPanel.GetComponent<Transform>().parent.transform.gameObject.activeSelf)
        {
            SetOrientation();
            currentMeleeWeapon.Attack();

        }
        else if (Input.GetMouseButton(0) && rangedInUse && currentRangedWeapon != null && !inventoryPanel.GetComponent<Transform>().parent.transform.gameObject.activeSelf)
        {
            SetOrientation();
            holdTimeDelta += Time.deltaTime;
 
        }
        //launching arrow
        if (Input.GetMouseButtonUp(0) && rangedInUse && currentRangedWeapon != null && !inventoryPanel.GetComponent<Transform>().parent.transform.gameObject.activeSelf)
            
        {
            currentRangedWeapon.Attack();
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
