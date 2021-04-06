using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDragHandler : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{

    public Item item;

    
    [SerializeField] Canvas canvas;
    public RectTransform rectTransform;
   
    private Transform originalParent;

    private bool isDragging;

    public CharacterSheet characterSheet;

    

    

    public void Awake()
    {
        
       rectTransform = GetComponent<RectTransform>();
       if(characterSheet.bag.Count -1 > rectTransform.parent.GetSiblingIndex())
        {
            if(characterSheet.bag[rectTransform.parent.GetSiblingIndex()] != null)
            {
                
                item = characterSheet.bag[rectTransform.parent.GetSiblingIndex()];
            }
            
        }
        else
        {
            Debug.Log("some sort of error");
        }
         
    }
    public void OnDrag(PointerEventData eventData)
    {

        if(eventData.button == PointerEventData.InputButton.Left)
        {
            //Debug.Log("Item Being dragged: " + item.ItemName);
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;

        }
        
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
            item = null;
            rectTransform.localPosition = Vector3.zero;
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            isDragging = true;
            originalParent = rectTransform.parent;
            rectTransform.SetParent(rectTransform.parent.parent);
            GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            rectTransform.parent.GetComponent<InventorySlotController>().item.Use();
            
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            isDragging = false;

            rectTransform.SetParent(originalParent);

            rectTransform.localPosition = Vector3.zero;
            GetComponent<CanvasGroup>().blocksRaycasts = true;

        }
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            return;
        }
         
        
    }
}