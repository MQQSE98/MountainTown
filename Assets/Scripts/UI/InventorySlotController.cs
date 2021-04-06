using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotController : MonoBehaviour, IDropHandler
{ 
    public Item item;
    //public Text itemAmountText;
    public int amount;

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
        Text itemAmountText = transform.Find("ItemAmount").GetComponent<Text>();
        //Debug.Log("Item When updating slot info" + item.ItemName);

        if(item && displayImage)
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

    public void OnDrop(PointerEventData eventData)
    {
        Item droppedItem = characterSheet.bag[eventData.pointerDrag.GetComponent<ItemDragHandler>().rectTransform.parent.GetSiblingIndex()];
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (eventData.pointerDrag.transform.parent.name == gameObject.name)
            {
                return;
            }

            if (characterSheet.bag[transform.GetSiblingIndex()] == null)
            {
                characterSheet.bag[transform.GetSiblingIndex()] = droppedItem;
                characterSheet.bag[eventData.pointerDrag.GetComponent<ItemDragHandler>().rectTransform.parent.GetSiblingIndex()] = null;
                playerManager.UpdatePanelSlots();
            }
            else
            {
                Item tempItem = characterSheet.bag[transform.GetSiblingIndex()];
                characterSheet.bag[transform.GetSiblingIndex()] = droppedItem;
                characterSheet.bag[eventData.pointerDrag.GetComponent<ItemDragHandler>().rectTransform.parent.GetSiblingIndex()] = tempItem;
                playerManager.UpdatePanelSlots();
            }
        }
    }
}