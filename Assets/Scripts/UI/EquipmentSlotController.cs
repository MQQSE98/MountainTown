using UnityEngine;
using UnityEngine.UI;


public class EquipmentSlotController : MonoBehaviour 
{
    public Item item;    
    public CharacterSheet characterSheet;
    private PlayerManager playerManager;     

    private void Start()
    {
        UpdateInfo();
        playerManager = GameObject.Find("player").GetComponent<PlayerManager>();        
    }

    public void UpdateInfo()
    {
        Image displayImage = transform.Find("ItemIcon").GetComponent<Image>();
        
        //Debug.Log("Item When updating slot info" + item.ItemName);

        if (item && displayImage)
        {
            displayImage.sprite = item.Icon;
            displayImage.color = Color.white;
        }
        else
        {
            displayImage.sprite = null;
            displayImage.color = Color.clear;
        }
    }

    public void Use()
    {
        if (item)
        {
            item.Use();
        }
    }

    public void UnEquip()
    {        
        if (this.name == "HeadSlot")
        {
            playerManager.playerSheet.AddItem(item);
            item.isEquiped = false;           
            playerManager.playerSheet.defense -= item.defense;
            playerManager.playerSheet.maxHealth -= item.health;
            playerManager.playerSheet.weight -= item.weight;
            
            item = null;
            playerManager.playerSheet.HeadSlot = null; 

        }
        if (this.name == "BodySlot")
        {
            playerManager.playerSheet.AddItem(item);
            item.isEquiped = false;
            playerManager.playerSheet.defense -= item.defense;
            playerManager.playerSheet.maxHealth -= item.health;
            playerManager.playerSheet.weight -= item.weight;
            item = null;
            playerManager.playerSheet.BodySlot = null;
        }
        if (this.name == "HandSlot")
        {
            playerManager.playerSheet.AddItem(item);
            item.isEquiped = false;
            playerManager.playerSheet.defense -= item.defense;
            playerManager.playerSheet.maxHealth -= item.health;
            playerManager.playerSheet.weight -= item.weight;
            item = null;
            playerManager.playerSheet.HandSlot = null;
        }
        if (this.name == "FeetSlot")
        {
            playerManager.playerSheet.AddItem(item);
            item.isEquiped = false;
            playerManager.playerSheet.defense -= item.defense;
            playerManager.playerSheet.maxHealth -= item.health;
            playerManager.playerSheet.weight -= item.weight;

            item = null;
            playerManager.playerSheet.BootSlot = null;
        }
        if (this.name == "RingSlot")
        {
            playerManager.playerSheet.AddItem(item);
            item.isEquiped = false;
            playerManager.playerSheet.defense -= item.defense;
            playerManager.playerSheet.maxHealth -= item.health;
            playerManager.playerSheet.weight -= item.weight;
            item = null;
            playerManager.playerSheet.RingSlot = null;
        }

        if (this.name == "AmuletSlot")
        {
            playerManager.playerSheet.AddItem(item);
            item.isEquiped = false;
            playerManager.playerSheet.defense -= item.defense;
            playerManager.playerSheet.maxHealth -= item.health;
            playerManager.playerSheet.weight -= item.weight;
            item = null;
            playerManager.playerSheet.AmuletSlot = null;
        }
        if (this.name == "LegSlot")
        {
            playerManager.playerSheet.AddItem(item);
            item.isEquiped = false;
            playerManager.playerSheet.defense -= item.defense;
            playerManager.playerSheet.maxHealth -= item.health;
            playerManager.playerSheet.weight -= item.weight;
            item = null;
            playerManager.playerSheet.LegSlot = null;
        }
        if (this.name == "ShoulderSlot")
        {
            playerManager.playerSheet.AddItem(item);
            item.isEquiped = false;
            playerManager.playerSheet.defense -= item.defense;
            playerManager.playerSheet.maxHealth -= item.health;
            playerManager.playerSheet.weight -= item.weight;
            item = null;
            playerManager.playerSheet.ShoulderSlot = null;
        }
        if (this.name == "WeaponSlot1")
        {
            playerManager.playerSheet.AddItem(item);
            item.isEquiped = false;
            playerManager.playerSheet.defense -= item.defense;
            playerManager.playerSheet.maxHealth -= item.health;
            playerManager.playerSheet.weight -= item.weight;
            item = null;
            playerManager.playerSheet.WeaponSlotLeft = null;
        }
        if (this.name == "WeaponSlot2")
        {
            playerManager.playerSheet.AddItem(item);
            item.isEquiped = false;
            playerManager.playerSheet.defense -= item.defense;
            playerManager.playerSheet.maxHealth -= item.health;
            playerManager.playerSheet.weight -= item.weight;
            item = null;
            playerManager.playerSheet.WeaponSlotRight = null;
        }

       UpdateInfo();
        
    }    
}
