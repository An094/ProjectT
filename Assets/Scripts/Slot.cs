using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
    public float Size;
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        if(dropped.CompareTag(transform.tag))
        {
            DraggableItem draggable = dropped.GetComponent<DraggableItem>();
            if (draggable != null && draggable.Size == this.Size)
            {
                draggable.parentAfterDrag = transform;
            }
        }
        
    }
}
