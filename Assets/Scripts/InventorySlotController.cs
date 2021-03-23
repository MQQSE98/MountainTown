using UnityEngine;
using UnityEngine.UI;

public class InventorySlotController : MonoBehaviour
{
    public Item item;
    //public Text itemAmountText;
    public int amount;
    private void Start()
    {
        UpdateInfo();
    }
    public void UpdateInfo()
    {
        Image displayImage = transform.Find("ItemIcon").GetComponent<Image>();
        Text itemAmountText = transform.Find("ItemAmount").GetComponent<Text>();
        //Debug.Log("Item When updating slot info" + item.ItemName);

        if(item && displayImage)
        {
            displayImage.sprite = item.Icon;
            displayImage.enabled = true;
        }
        else
        {
            displayImage.sprite = null;
            displayImage.enabled = false;
        }
    } 
    
    public void Use()
    {
        if (item)
        {
            item.Use();
        }
    }
}