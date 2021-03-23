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

    //Variables controlling health
    public float healthRegenValue = 0.001f;

    //variables controlling stamina
    public float stamConsumtionPercent = .0005f;
    public float stamRegenRate = 0.5f;
    public float stamConsumationRate; 

    //varibles controlling timers
    public float regenTimer = 0f;
    public float MaxRegenTimer = 3f;

    public int alteredMoveSpeed = 6;
    public int defaultMoveSpeed = 3;
    
    public ResourceController resourceController;
    public GameObject CurrentInteractableObject = null;
    
    public bool isFatigued = false;
    public bool isDraining = false;    

    public void Start()
    {
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
    }

    //Testing Methods for Resource Controller
    void TakeDamage(int damage)
    {
        playerSheet.currentHealth -= damage;
        resourceController.SetHealth(playerSheet.currentHealth);
    }
    void Sprint()
    {
        print(playerSheet.currentStamina);
        stamConsumationRate = playerSheet.maxStamina  * stamConsumtionPercent;

        if (Input.GetKey(KeyCode.G) &&  playerSheet.currentStamina >= 5)
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

        } else if (isFatigued == true)
        {
            RegenTimer();
        }
    }

    void RegenTimer()
    {
        if (regenTimer <= MaxRegenTimer)
        {
            regenTimer += Time.deltaTime;
            isFatigued = true;
        }
        else if (regenTimer >= MaxRegenTimer)
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
        CurrentInteractableObject = other.gameObject;      
    }

    void OnTriggerExit2D(Collider2D other)
    {        
         CurrentInteractableObject = null;
         UnityEngine.Debug.Log(other.name);     
    }

    public void PickUp()
    {
        if (Input.GetButtonDown("Interact") && CurrentInteractableObject)
        {
            GameObject itemObject = CurrentInteractableObject;

            Item itemPickedUp = itemObject.GetComponent<ItemManager>().item;

            Debug.Log("Current item is " + itemPickedUp.ItemName);

            if (itemPickedUp)
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(20);
        }
    }
 
    public void Heal(float amount)
    {
        playerSheet.currentHealth += amount;

        if(playerSheet.currentHealth > playerSheet.maxHealth)
        {
            playerSheet.maxHealth = playerSheet.currentHealth;
        }
        resourceController.SetHealth(playerSheet.currentHealth);
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
}