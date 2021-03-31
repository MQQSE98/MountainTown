using UnityEngine;
using UnityEngine.UI;

public class ResourceController: MonoBehaviour
{   
    public Slider healthBar, stamBar, manaBar, expBar;
   
    public void SetMaxHealth(float health)
    {
        healthBar.maxValue = health;
        healthBar.value = health;
    }

    public void SetHealth(float health)
    {
        healthBar.value = health;
    }
   
    public void SetMaxStamina(float stamina)
    {
        stamBar.maxValue = stamina;
        stamBar.value = stamina;

    }

    public void SetStamina(float stamina)
    {
        stamBar.value = stamina;
    }

    public void SetMaxMana(float mana)
    {
        manaBar.maxValue = mana;
        manaBar.value = mana;
       
    }

    public void SetMana(float mana)
    {
        manaBar.value = mana;
    }

    public void SetMaxExp(float exp)
    {
        expBar.maxValue = exp;
        expBar.value = exp;
    }

    public void SetExp(float exp)
    {
        expBar.value = exp;
    }
}
