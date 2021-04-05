using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateStaminaHealth : MonoBehaviour
{
    private PlayerManager pm;
    private GameObject healthBar;
    private GameObject staminaBar;
    private float healthPercent;
    private float staminaPercent;

    // Start is called before the first frame update
    void Start()
    {
        pm = GameObject.Find("Player_V3_0").GetComponent<PlayerManager>();
        healthBar = GameObject.Find("HealthBar");
        staminaBar = GameObject.Find("StaminaBar");
        //healthPercent = pm.getHealthPercentage();
        //staminaPercent = pm.getStaminaPercentage();
        healthPercent = 0.73F;
        staminaPercent = 0.22F;
        Debug.Log("healthscale: " + healthPercent);
        Debug.Log("staminascale: " + staminaPercent);
    }

    // Update is called once per frame
    void Update()
    {
        //healthPercent = pm.getHealthPercentage();
        //staminaPercent = pm.getStaminaPercentage();
        //should never happen, but stop UI component from scaling across the screen if more health than max
        if (healthPercent > 1) healthPercent = 1;
        if (staminaPercent > 1) staminaPercent = 1;

        healthBar.GetComponent<RectTransform>().localScale = new Vector3(healthPercent, 1, 1);
        staminaBar.GetComponent<RectTransform>().localScale = new Vector3(staminaPercent, 1, 1);
    }

}
