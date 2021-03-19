using UnityEngine;
using UnityEditor;

public enum ItemType
{
    Consumable,
    Equipment,
    Default
}

[CreateAssetMenu]
public abstract class Item : ScriptableObject
{

    [SerializeField] string id;
    public string ID { get { return id; } }

   

    public ItemType type;
    public string ItemName;
    public Sprite Icon;
    //public GameObject prefab;
    [TextArea(15,20)]
    public string Description;
   

    //gets unity unique asset id that it generates when an object is created and sets it to our ID 
    //This is so we don't have to set a unique ID for each item.
    private void OnValidate()
    {
        string path = AssetDatabase.GetAssetPath(this);
        id = AssetDatabase.AssetPathToGUID(path);
    }

}

