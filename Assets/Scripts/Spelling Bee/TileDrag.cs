using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TileDrag : MonoBehaviour
{
    public Image image;
    [HideInInspector]
    public Transform parentAfterDrag;

    // Sets the object at a certain Y level so it can only move left and right
    private void Update() {
        transform.position = new Vector3(transform.position.x, 383, transform.position.z);
    }

    public void OnBeginDrag(BaseEventData data) {
        PointerEventData eventData = (PointerEventData) data;

        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();

        // When object begins dragging, image fades a little
        image.color = new Color32(255, 255, 255, 170);
        image.raycastTarget = false;
    }

    public void OnDrag(BaseEventData data) {
        PointerEventData eventData = (PointerEventData) data;

        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(BaseEventData data) {
        PointerEventData eventData = (PointerEventData) data;

        transform.SetParent(parentAfterDrag);

        // When object finishes dragging, image fades back to its normal color
        image.color = new Color(255, 255, 255, 255);
        image.raycastTarget = true;
    }

    public void OnDrop(BaseEventData data) {
        PointerEventData eventData = (PointerEventData) data;

        GameObject dropped = eventData.pointerDrag;
        TileDrag tileDrag = dropped.GetComponent<TileDrag>();
        tileDrag.parentAfterDrag = transform;
        
    }

}
