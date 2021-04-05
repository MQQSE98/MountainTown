/// <summary>
/// Manages the Unity player object.
/// </summary>

using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{ 
    //public GameObject mainUIPanel = GameObject.Find("MainUIPanel");
    public CharacterSheet playerSheet;
    public PlayerMovement playerMovement;
    public PlayerCombat playerCombat;
    public GameObject inventoryPanel;

    Animator animator;

    [HideInInspector]
    public string orientation;

    //variables controlling health
    public float healthRegenValue = 0.001f;

    //variables controlling stamina
    public float stamConsumtionPercent = .0005f;
    public float stamRegenRate = 0.5f;
    [HideInInspector]
    public float stamConsumationRate; 

    //varibles controlling timers
    [HideInInspector]
    public float regenTimer = 0f;
    public float maxRegenTimer = 3f;

    public int alteredMoveSpeed = 6;
    public int defaultMoveSpeed = 3;

    [HideInInspector]
    public float holdTimeDelta;
    
    public ResourceController resourceController;
    public GameObject currentInteractableObject = null;

    //melee / ranged SO data
    public MeleeWeapon currentMeleeWeapon;
    public RangedWeapon currentRangedWeapon;

    //player stamina states
    [HideInInspector]
    public bool isFatigued = false;
    [HideInInspector]
    public bool isDraining = false;
    [HideInInspector]
    public bool meleeInUse;
    [HideInInspector]
    public bool rangedInUse;


    public void Start()
    {
        InitializePlayer();
        
        //set all components 
        animator = gameObject.GetComponent<Animator>();    
        playerMovement = gameObject.GetComponent<PlayerMovement>();
        playerCombat = gameObject.GetComponent<PlayerCombat>();

        //playerSheet.list.Clear();
        UpdatePanelSlots();
        //playerSheet.currentHealth = playerSheet.maxHealth;
        resourceController.SetMaxHealth(playerSheet.maxHealth);

        //playerSheet.currentStamina = playerSheet.maxStamina;
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

    //-----------PLAYER CALCULATIONS----------\\
    public float AbilityScoreMultiplier(int ability)
    {
        float multiplier = ability / 2;
        return multiplier;
    }

    public float SkillMultiplier(int skill)
    {
        float multiplier = (float)Math.Pow(skill, 0.5f);
        return multiplier;
    }

    public void AttackRating(int weaponRating, int weaponSkill)
    {
        playerSheet.attack = playerSheet.level * 10 + weaponRating * SkillMultiplier(weaponSkill);
    }

    public void DefenseRating(int armorRating, int armorSkill)
    {
        playerSheet.defense = playerSheet.level * 10 + armorRating * SkillMultiplier(armorSkill);
    }

    public void MaxHealth()
    {
        int playerBase = playerSheet.level * 10;
        playerSheet.maxHealth = playerBase + AbilityScoreMultiplier(playerSheet.vitality) * playerBase;
    }

    public void InitializePlayer()
    {
        InitializeInfo();
        InitializePrimeAttributes();
        InitializeAbilityScores();
    }

    public void InitializeInfo()
    {
        playerSheet.playerName = "Hero";
        playerSheet.level = 1;
        playerSheet.experiencePoints = 0;
        playerSheet.attributePoints = 5;
        playerSheet.skillPoints = 5;
    }

    public void InitializePrimeAttributes()
    {
        MaxHealth();
        playerSheet.currentHealth = playerSheet.maxHealth;
        playerSheet.maxStamina = 100;
        playerSheet.currentStamina = 100;
        playerSheet.maxMana = 100;
        playerSheet.currentMana = 100; 
    }

    public void InitializeAbilityScores()
    {
        playerSheet.power = 1;
        playerSheet.vitality = 1;
        playerSheet.endurance = 1;
        playerSheet.agility = 1;
        playerSheet.intellect = 1;
        playerSheet.luck = 1;
    }
    //-----------PLAYER CALCULATIONS----------\\

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