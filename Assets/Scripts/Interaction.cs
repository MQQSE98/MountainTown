using UnityEngine;

public abstract class Interaction : ScriptableObject
{
    public abstract void InteractWithItem(Inventory inventory, GameObject item);
    //public abstract bool IsInteractable();
}
