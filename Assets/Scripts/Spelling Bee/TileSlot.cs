using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileSlot : MonoBehaviour
{
    public void OnDrop(BaseEventData data) {
        Debug.Log("OnDrop");
        PointerEventData pointerData = (PointerEventData) data;

        if (pointerData.pointerDrag != null) {
            pointerData.pointerDrag.GetComponent<RectTransform>().anchoredPosition =
            GetComponent<RectTransform>().anchoredPosition;
        }
    }
}
