
using UnityEngine;

[CreateAssetMenu(menuName = "Player/Interaction")]
public class PlayerInteraction : Interaction
{
    
    
    public override void InteractWithItem(Inventory inventory, GameObject item)
    {
        if (Input.GetButtonDown("Interact"))
        {
            //inventory.AddItem(item);
            //inventory.AddItem(item);
            UnityEngine.Debug.Log("Interaction Successful!");
   
        }
    }

    
}