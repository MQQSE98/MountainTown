using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHeathController : MonoBehaviour
{
    public Slider bossHealthBar;


    public void SetMaxHealth(float health)
    {
        bossHealthBar.maxValue = health;
        bossHealthBar.value = health;
    }
    public void SetHealth(float health)
    {
        bossHealthBar.value = health;
    }

}
