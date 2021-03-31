using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public Item item;

    //private Sprite icon;

    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = item.Icon;
    }
        
    //public DefaultItem defaultItem;
}
