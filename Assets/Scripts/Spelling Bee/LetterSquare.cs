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


    private void Start() {
        x = transform.position.x;
    }

    private void Awake() {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

//transform.position.x, 383, transform.position.z
    // Sets the object at a certain Y level so it can only move left and right
    private void Update() {
        //     transform.position = new Vector3(
        //     (transform.position.x-transform.position.x%50), 383, transform.position.z);
    }

    // Sets the tile to that letter
    public void SetLetter(char letter) {
        this.letter = letter;
        text.text = letter.ToString();
    }

    /*public void OnBeginDrag(BaseEventData data) {
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
        // float xDiff;
        // float roundedPos;
        transform.position = Input.mousePosition;
             if (transform.position.x >510) {
                 transform.position = new Vector3(510,381,0);
             } else if (transform.position.x <360) {
                 transform.position = new Vector3(360,381,0);
             }else {
            //     xDiff = transform.position.x%50;
            //     roundedPos = transform.position.x - xDiff;
            //     if (xDiff > (50/2)) {
            //         roundedPos += 50;
            //     }
                 transform.position = new Vector3(transform.position.x, 381, 0);
            }
        Debug.Log(transform.position.x);
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
        float xDiff;
        float roundedPos;

        xDiff = transform.position.x%50;
        roundedPos = transform.position.x - xDiff;
        if (xDiff > (50/2)) {
            roundedPos += 50;
        }
        transform.position = new Vector3(roundedPos, 381, 0);


        GameObject dropped = eventData.pointerDrag;
        LetterSquare square = dropped.GetComponent<LetterSquare>();
        square.parentAfterDrag = transform;
        moving = false;

        Debug.Log("dropped");
    }
    */
    //public void LockInPosition() {

 
}
