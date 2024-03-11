using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class LetterSquare : MonoBehaviour
{
    [HideInInspector]
    public Transform parentAfterDrag;

    public Image image;

    public TextMeshProUGUI text;
    
    public char letter { get; set; }

    public bool moving;

    private int index;

    private float x;

    //public float gridSize = .5F;

    public Vector3 targetPos;

    private void Start() {
        x = transform.position.x;
    }

    private void Awake() {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Sets the object at a certain Y level so it can only move left and right
    private void Update() {
        transform.position = new Vector3(
            Mathf.Round(targetPos.x), 
            Mathf.Round(targetPos.y), 
            Mathf.Round(targetPos.z));
    }

    // Sets the tile to that letter
    public void SetLetter(char letter) {
        this.letter = letter;
        text.text = letter.ToString();
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
        moving = true;
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
        LetterSquare square = dropped.GetComponent<LetterSquare>();
        square.parentAfterDrag = transform;
        moving = false;

        Debug.Log("dropped");
    }

    /*public void LockInPosition() {

    }*/
    // float RoundToNearestGrid(float pos) {
    //     float xDiff = pos % gridSize;
    //     bool isPositive = pos > 0 ? true : false;
    //     pos -= xDiff;
    //     if (Mathf.Abs(xDiff) > (gridSize / 2))
    //     {
    //         if(isPositive)
    //         {
    //             pos += gridSize;
    //         }
    //         else
    //         {
    //             pos -= gridSize;
    //         }
    //     }
    //     return pos;
    // }
}
