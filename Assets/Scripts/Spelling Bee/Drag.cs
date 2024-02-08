using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
    private bool dragging = false;
    private Vector3 offset;

    // Update is called once per frame
    void Update() {
        //Move object, take into account original offset
        if (dragging) {
            Vector3 Temp = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, 0f, 0f);

            transform.position = Temp + offset;
        }
    }

    private void OnMouseDown() {
        //Record the difference betweem the objects center, and the clicked point on the camera plane.
        Vector3 Temp2 = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, 0f, 0f);
        offset = transform.position - Temp2;
        dragging = true;
    }

    private void OnMouseUp() {
        //Stop Dragging
        dragging = false;
    }

}
