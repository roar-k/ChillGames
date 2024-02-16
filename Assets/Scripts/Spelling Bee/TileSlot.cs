using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileSlot : MonoBehaviour
{
    public void OnDrop(BaseEventData data) {
        PointerEventData eventData = (PointerEventData) data;

        /* GameObject dropped = eventData.pointerDrag;
        LetterSquare square = dropped.GetComponent<LetterSquare>();
        square.parentAfterDrag = transform; */

        if (eventData.pointerDrag != null) {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            Debug.Log("dropped");
        }
        // Debug.Log("dropped");
    }
}
