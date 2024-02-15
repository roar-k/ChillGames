using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileSlot : MonoBehaviour
{
    public void OnDrop(BaseEventData data) {
        PointerEventData eventData = (PointerEventData) data;

        GameObject dropped = eventData.pointerDrag;
        TileDrag tileDrag = dropped.GetComponent<TileDrag>();
        tileDrag.parentAfterDrag = transform;
    }
}
