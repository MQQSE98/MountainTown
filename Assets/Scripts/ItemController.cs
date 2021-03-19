using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{

    public ItemSO ItemData;
    protected SpriteRenderer myRenderer;
    protected Collider2D myCollider;

    // Start is called before the first frame update
    void Start()
    {
        myRenderer = GetComponent<SpriteRenderer>();
        myCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PerformInteraction()
    {
        UnityEngine.Debug.Log("Working on NEW!!");
        gameObject.SetActive(false);
    }

}
