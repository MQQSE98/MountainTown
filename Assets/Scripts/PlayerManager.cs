using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    /**
    private int attack = 10;
    private int defense = 10;
    private int currentHealth = 100;
    private int maxHealth = 100;
    private int currentStamina = 100;
    private int maxStamina = 100;
    private int perception = 0;
    private int agility = 0;
    private int sharpShooter = 0;
    private int charisma = 0;
    private int luck = 0;
    private int currentLevel = 1;
    private int perkPoints = 0;
    private int money = 0;

    private int deathCount = 0;

    private float currentXP = 0;
    private float requiredXP = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Checks whether player has enough XP to level up. 
    /// </summary>
    /// <returns>
    /// True if the player successfully leveled up
    /// False if the player failed to meet the requirements
    /// </returns>
    private bool checkLevelUp() {
        if (currentXP >= requiredXP) {
            currentXP++;
            currentXP = currentXP - requiredXP;
            requiredXP = (float)(requiredXP * 1.5);
            perkPoints++;
            attack += (int)(attack * .2);
            defense += (int)(defense * .2);
            maxStamina += (int)(maxStamina * .05);
            maxHealth += (int)(maxHealth * .05);
            currentLevel++;
            return true;
        } else {
            return false;
        }
    }

    /// <summary>
    /// returns a ratio of players currentHealth / maxHealth. 
    /// </summary>
    /// <returns>
    /// currentHealth / maxHealth
    /// </returns>
    public float getHealthPercentage() {
        return (float)currentHealth / (float)maxHealth;
    }

    /// <summary>
    /// returns a ratio of players currentStamina / maxStamina. 
    /// </summary>
    /// <returns>
    /// currentStamina / maxStamina
    /// </returns>
    public float getStaminaPercentage() {
        return currentStamina / maxStamina;
    }

    /// <summary>
    /// Checks to ensure currentHealth >= 0 
    /// </summary>
    public void checkHealth() {
        if(currentHealth <= 0) {
            death();
        }
    }

    /// <summary>
    /// Moves Player to spawn, resets health and stamina, and removes
    /// inventory items.
    /// </summary>
    private void death() {
        deathCount++;
        currentHealth = maxHealth;
        currentStamina = maxStamina;
        //load scene change
        //delete inventory items on scene change
        //keep armor on scene change
    }
    */
}
