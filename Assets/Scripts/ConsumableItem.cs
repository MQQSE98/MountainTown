using UnityEngine;

[CreateAssetMenu(menuName = "Item/Consumable")]
public class ConsumableItem : Item
{
    public string ItemTag;
    public bool IsConsumableItem = true; 
}