using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TileDrag : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;

    private RectTransform rectTransform;
    private Image image;

    private void Start() {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
    }

    private void Update() {
        transform.position = new Vector3(transform.position.x, 410, transform.position.z);
    }

    public void OnBeginDrag(BaseEventData data) {
        Debug.Log("BeginDrag");
        PointerEventData eventData = (PointerEventData) data;

        image.color = new Color32(255, 255, 255, 170);
    }

    public void OnDrag(BaseEventData data) {
        Debug.Log("Dragging");
        PointerEventData eventData = (PointerEventData) data;

        rectTransform.position = Input.mousePosition;
    }

    public void OnEndDrag(BaseEventData data) {
        Debug.Log("EndDrag");
        PointerEventData eventData = (PointerEventData) data;

        image.color = new Color(255, 255, 255, 255);
    }

   /*public void DragHandler(BaseEventData data) {
    PointerEventData pointerData = (PointerEventData) data;

    Vector2 position;
    RectTransformUtility.ScreenPointToLocalPointInRectangle(
        (RectTransform) canvas.transform, pointerData.position,
        canvas.worldCamera, out position);

        transform.position = canvas.transform.TransformPoint(position);
   }*/

}
