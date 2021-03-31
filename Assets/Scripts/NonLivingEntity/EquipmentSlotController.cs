using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlotController : MonoBehaviour
{
    public Item item;

    private void Start()
    {
        UpdateInfo();
    }
    public void UpdateInfo()
    {
        Image displayImage = transform.Find("ItemIcon").GetComponent<Image>();

        if (item && displayImage)
        {
            displayImage.sprite = item.Icon;
            //displayImage.color = Color.white;
            displayImage.enabled = true;
        }
        else
        {
            displayImage.sprite = null;
            //displayImage.color = Color.clear;
            displayImage.enabled = false;
        }
    }
   
}
