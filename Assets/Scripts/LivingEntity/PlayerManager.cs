/// <summary>
/// Manages the Unity player object.
/// </summary>

using System;
using System.Collections;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //public GameObject mainUIPanel = GameObject.Find("MainUIPanel");
    public CharacterSheet playerSheet;
    public PlayerMovement playerMovement;
    public PlayerCombat playerCombat;
    public GameObject inventoryPanel;
    public GameObject equipmentPanel;
    public GameObject statPanel;
    public GameObject goldPanel;

    //PlayerParticles
    public ParticleSystem healthLoss;
    public ParticleSystem DashDown;
    
    

    private SpriteRenderer sr;
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

        sr = gameObject.GetComponent<SpriteRenderer>();
        
        //playerSheet.list.Clear();
        UpdatePanelSlots();
        UpdateStats();
        UpdateGold();

        //playerSheet.currentHealth = playerSheet.maxHealth;
        resourceController.SetMaxHealth(playerSheet.maxHealth);

        //playerSheet.currentStamina = playerSheet.maxStamina;
        resourceController.SetMaxStamina(playerSheet.maxStamina);

        resourceController.SetMaxExp(playerSheet.ExpToLevel);
        resourceController.SetExp(playerSheet.experiencePoints);
    }
    void Update()
    {
        TestDamage();
        Sprint();
        PickUp();
        RegenStamina();
        RegenHealth();
        UpdatePanelSlots();
        UpdateEquipmentSlots();
        UpdateStats();
        UpdateGold();
        Attack();
        Dash();
        
        LevelUp();
     
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

    public void MaxStamina()
    {
        int playerBase = playerSheet.level * 10;
        playerSheet.maxStamina = playerBase + AbilityScoreMultiplier(playerSheet.endurance) * playerBase;
    }

    public void MaxMana()
    {
        int playerBase = playerSheet.level * 10;
        playerSheet.maxMana = playerBase + AbilityScoreMultiplier(playerSheet.intellect) * playerBase;
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
        playerSheet.ExpToLevel = playerSheet.level * 200;
        playerSheet.skillPoints = 5;
    }

    public void InitializePrimeAttributes()
    {
        MaxHealth();
        MaxStamina();
        MaxMana();
        playerSheet.currentHealth = playerSheet.maxHealth;
        playerSheet.currentStamina = playerSheet.maxStamina;
        playerSheet.currentMana = playerSheet.maxMana;
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
        Instantiate(healthLoss, transform.position, Quaternion.identity, transform);       
        StartCoroutine(DamageEffectSequence(sr, Color.red, 0.5f, 0.5f));
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

        if (Input.GetKey(KeyCode.G) && playerSheet.currentStamina >= 5)
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
        if (playerSheet.currentStamina < playerSheet.maxStamina && isFatigued == false && isDraining == false)
        {
            playerSheet.currentStamina += stamRegenRate;
            resourceController.SetStamina(playerSheet.currentStamina);

        } else if (isFatigued == true)
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
        if (playerSheet.currentHealth < playerSheet.maxHealth)
        {
            playerSheet.currentHealth += healthRegenValue;
            resourceController.SetHealth(playerSheet.currentHealth);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        UnityEngine.Debug.Log(other.name);
        currentInteractableObject = other.gameObject;

        if(other.gameObject.CompareTag("Gold"))
        {
            Gold gold = other.gameObject.GetComponent<Gold>();
            float delay = 2.0f;

            gold.magnetIsOn = true;

            playerSheet.gold += 1;
            Destroy(other.gameObject, delay);
        }
        if(other.gameObject.CompareTag("EXP"))
        {
            Gold gold = other.gameObject.GetComponent<Gold>(); //calling it gold here, but just reusing the controller
            float delay = 2.0f;

            gold.magnetIsOn = true;

            playerSheet.experiencePoints += gold.amount;
            resourceController.SetExp(playerSheet.experiencePoints);
            
            Destroy(other.gameObject, delay);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        currentInteractableObject = null;
        UnityEngine.Debug.Log(other.name);
    }

    //HERE !! -------------------------------------------------------------------------------------
    public void PickUp()
    {
        if (Input.GetButtonDown("Interact") && currentInteractableObject)
        {
            GameObject itemObject = currentInteractableObject;

            Item itemPickedUp = itemObject.GetComponent<ItemManager>().item;

            Debug.Log("Current item is " + itemPickedUp.ItemName);

            if (itemPickedUp)
            {
                playerSheet.AddItem(itemPickedUp);
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //TakeDamage(20);
        }
    }

    public void Heal(float amount)
    {
        playerSheet.currentHealth += amount;

        if (playerSheet.currentHealth > playerSheet.maxHealth)
        {
            playerSheet.currentHealth = playerSheet.maxHealth;
        }
        resourceController.SetHealth(playerSheet.currentHealth);
    }

    public void UpdateStats()
    {
        foreach(Transform child in statPanel.transform)
        {
            StatPanelController stat = child.GetComponent<StatPanelController>();
            if(child.name == "Attack")
            {
                stat.value.text = playerSheet.attack.ToString();
            }
            if(child.name == "Defence")
            {
                stat.value.text = playerSheet.defense.ToString();
            }
            if(child.name == "Health")
            {
                stat.value.text = playerSheet.maxHealth.ToString();
            }
            if(child.name == "Stamina")
            {
                stat.value.text = playerSheet.maxStamina.ToString();
            }
            if(child.name == "Luck")
            {
                stat.value.text = playerSheet.luck.ToString();
            }
            if(child.name == "Mana")
            {
                stat.value.text = playerSheet.maxMana.ToString();
            }
            if(child.name == "EXP")
            {
                stat.value.text = playerSheet.experiencePoints.ToString();
            }
            if(child.name == "Weight")
            {
                stat.value.text = playerSheet.weight.ToString();
            }

            //stat.UpdateInfo();
            
        }
    }
    public void UpdateGold()
    {
        foreach(Transform child in goldPanel.transform)
        {
            GoldPanelController gold = child.GetComponent<GoldPanelController>();

            gold.gold.text = playerSheet.gold.ToString();
        }
    }

    public void UpdatePanelSlots()
    {
        int index = 0;
        foreach (Transform child in inventoryPanel.transform)
        {
            InventorySlotController slot = child.GetComponent<InventorySlotController>();

            if (index < playerSheet.bag.Count)
            {
                slot.item = playerSheet.bag[index];
                //slot.amount = 1;                f
            }
            else
            {
                slot.item = null;
            }

            slot.UpdateInfo();
            index++;
        }
    }
    public void UpdateEquipmentSlots()
    {
        
        foreach (Transform child in equipmentPanel.transform)
        {
            EquipmentSlotController equipmentSlot = child.GetComponent<EquipmentSlotController>();

            
                if (child.name == "HeadSlot")
                {
                    equipmentSlot.item = playerSheet.HeadSlot;
                }
                if(child.name == "BodySlot")
                {
                    equipmentSlot.item = playerSheet.BodySlot;
                }
                if(child.name == "HandSlot")
                {
                    equipmentSlot.item = playerSheet.HandSlot;
                }
                if (child.name == "FeetSlot")
                {
                    equipmentSlot.item = playerSheet.BootSlot;
                }
                if (child.name == "RingSlot")
                {
                    equipmentSlot.item = playerSheet.RingSlot;
                }
                
                if (child.name == "AmuletSlot")
                {
                    equipmentSlot.item = playerSheet.AmuletSlot;
                }               
                if (child.name == "LegSlot")
                {
                    equipmentSlot.item = playerSheet.LegSlot;
                }
                if (child.name == "ShoulderSlot")
                {
                    equipmentSlot.item = playerSheet.ShoulderSlot;
                }
                if (child.name == "WeaponSlot1")
                {
                    equipmentSlot.item = playerSheet.WeaponSlotLeft;
                }
                if (child.name == "WeaponSlot2")
                {
                    equipmentSlot.item = playerSheet.WeaponSlotRight;
                }                           
                            
            equipmentSlot.UpdateInfo();
            
        }
    }
    public void AddEquipmentStats(Item item)
    {
        if (item.isEquiped == true)
        {
            playerSheet.defense += item.defense;
            playerSheet.maxHealth += item.health;
            playerSheet.weight += item.weight;
        }                
    }
    
    public void EquipItem(Item item)
    {
        Item tempItem;
        if (item.TypeOfItem == TypeOfItem.HeadArmor)
        {
            if (playerSheet.HeadSlot == null)
            {
                playerSheet.HeadSlot = item;
                item.isEquiped = true;
                playerSheet.Remove(item);
            }
            else
            {
                tempItem = playerSheet.HeadSlot;
                playerSheet.HeadSlot = item;
                item.isEquiped = true;
                playerSheet.AddItem(tempItem);
                playerSheet.Remove(item);
            }
        }
        if (item.TypeOfItem == TypeOfItem.HandArmor)
        {
            if (playerSheet.HandSlot == null)
            {

                playerSheet.HandSlot = item;
                item.isEquiped = true;
                playerSheet.Remove(item);
            }
            else
            {
                tempItem = playerSheet.HandSlot;
                playerSheet.HandSlot = item;
                item.isEquiped = true;
                playerSheet.AddItem(tempItem);
                playerSheet.Remove(item);
            }
        }
        if (item.TypeOfItem == TypeOfItem.ChestArmor)
        {
            if (playerSheet.BodySlot == null)
            {
                playerSheet.BodySlot = item;
                item.isEquiped = true;
                playerSheet.Remove(item);
            }
            else
            {
                tempItem = playerSheet.BodySlot;
                playerSheet.BodySlot = item;
                item.isEquiped = true;
                playerSheet.AddItem(tempItem);
                playerSheet.Remove(item);
            }
        }
        if (item.TypeOfItem == TypeOfItem.LegArmor)
        {
            if (playerSheet.LegSlot == null)
            {
                playerSheet.LegSlot = item;
                item.isEquiped = true;
                playerSheet.Remove(item);
            }
            else
            {
                tempItem = playerSheet.LegSlot;
                playerSheet.LegSlot = item;
                playerSheet.AddItem(tempItem);
                item.isEquiped = true;
                playerSheet.Remove(item);
            }
        }
        if (item.TypeOfItem == TypeOfItem.FeetArmor)
        {
            if (playerSheet.BootSlot == null)
            {
                playerSheet.BootSlot = item;
                playerSheet.Remove(item);
            }
            else
            {
                tempItem = playerSheet.BootSlot;
                playerSheet.BootSlot = item;
                playerSheet.AddItem(tempItem);
                playerSheet.Remove(item);
            }
        }
        if (item.TypeOfItem == TypeOfItem.ShoulderArmor)
        {
            if (playerSheet.ShoulderSlot == null)
            {
                playerSheet.ShoulderSlot = item;
                playerSheet.Remove(item);
            }
            else
            {
                tempItem = playerSheet.ShoulderSlot;
                playerSheet.ShoulderSlot = item;
                playerSheet.AddItem(tempItem);
                playerSheet.Remove(item);
            }

        }
        if (item.TypeOfItem == TypeOfItem.Amulet)
        {
            if (playerSheet.AmuletSlot == null)
            {
                playerSheet.AmuletSlot = item;
                playerSheet.Remove(item);
            }
            else
            {
                tempItem = playerSheet.AmuletSlot;
                playerSheet.AmuletSlot = item;
                playerSheet.AddItem(tempItem);
                playerSheet.Remove(item);
            }


        }
        if (item.TypeOfItem == TypeOfItem.MeleeWeapon || item.TypeOfItem == TypeOfItem.RangedWeapon && item.takesTwoHands == false)
        {
            if (playerSheet.WeaponSlotLeft == null)
            {
                playerSheet.WeaponSlotLeft = item;
                playerSheet.Remove(item);
            }
            if (playerSheet.WeaponSlotLeft != null)
            {
                tempItem = playerSheet.WeaponSlotLeft;
                playerSheet.WeaponSlotLeft = item;
                playerSheet.AddItem(tempItem);
                playerSheet.Remove(item);
            }

            if (playerSheet.WeaponSlotRight != null && playerSheet.WeaponSlotRight.takesTwoHands == true)
            {
                playerSheet.WeaponSlotRight = null;
            }

        }
        if (item.TypeOfItem == TypeOfItem.MeleeWeapon || item.TypeOfItem == TypeOfItem.RangedWeapon && item.takesTwoHands == true)
        {
            //if slots are empty, add weapon to both slots
            if (playerSheet.WeaponSlotLeft == null && playerSheet.WeaponSlotRight == null)
            {
                playerSheet.WeaponSlotLeft = item;
                playerSheet.WeaponSlotRight = item;

                //remove item from bag
                playerSheet.Remove(item);
            }

            if (playerSheet.WeaponSlotLeft != null && playerSheet.WeaponSlotRight != null || playerSheet.WeaponSlotLeft == null && playerSheet.WeaponSlotRight != null || playerSheet.WeaponSlotLeft != null && playerSheet.WeaponSlotRight == null)
            {
                tempItem = playerSheet.WeaponSlotLeft;
                playerSheet.WeaponSlotLeft = item;
                playerSheet.AddItem(tempItem);
                playerSheet.Remove(item);

                if (playerSheet.WeaponSlotRight.isOffHand == true)
                {
                    tempItem = playerSheet.WeaponSlotRight;
                    playerSheet.WeaponSlotRight = item;
                    playerSheet.AddItem(tempItem);

                }
            }
        }
        if (item.TypeOfItem == TypeOfItem.MeleeWeapon || item.TypeOfItem == TypeOfItem.RangedWeapon && item.isOffHand == true)
        {
            if (playerSheet.WeaponSlotRight != null && playerSheet.WeaponSlotRight.takesTwoHands == true)
            {
                tempItem = playerSheet.WeaponSlotLeft;
                playerSheet.WeaponSlotLeft = null;
                playerSheet.AddItem(tempItem);
                playerSheet.Remove(item);
            }
            if (playerSheet.WeaponSlotRight != null && playerSheet.WeaponSlotRight.takesTwoHands == false)
            {
                tempItem = playerSheet.WeaponSlotRight;
                playerSheet.WeaponSlotRight = item;
                playerSheet.AddItem(tempItem);
                playerSheet.Remove(item);
            }
            if (playerSheet.WeaponSlotRight == null)
            {
                playerSheet.WeaponSlotRight = item;
                playerSheet.Remove(item);
            }
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
            Debug.Log(orientation);
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
    public void Dash()
    {
        
        if(playerMovement.dash == true)
        {
           
                Instantiate(DashDown, transform.position, Quaternion.identity);
            
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



    IEnumerator DamageEffectSequence(SpriteRenderer sr, Color dmgColor, float duration, float delay)
    {
        // save origin color
        Color originColor = sr.color;

        // tint the sprite with damage color
        sr.color = dmgColor;

        // you can delay the animation
        yield return new WaitForSeconds(delay);

        // lerp animation with given duration in seconds
        for (float t = 0; t < 1.0f; t += Time.deltaTime / duration)
        {
            sr.color = Color.Lerp(dmgColor, originColor, t);

            yield return null;
        }

        // restore origin color
        sr.color = originColor;
    }
    

    public void LevelUp()
    {
        

        if(playerSheet.experiencePoints >= playerSheet.ExpToLevel)
        {
           
            playerSheet.level += 1;
            playerSheet.ExpToLevel = playerSheet.level * 200;
            playerSheet.maxHealth += 20;
            playerSheet.maxStamina += 20;
            playerSheet.attack += 3;
            playerSheet.defense += 3;

            animator.Play("LevelUp");


        }
    }
}