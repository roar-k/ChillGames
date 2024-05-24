using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    private Camera mainCamera;
    private Collider bladeCollider;
    private TrailRenderer bladeTrail;

    private bool slicing;

    public Vector3 direction { get; private set; }
    public float sliceForce = 5f;
    public float minSliceVelocity = 0.01f;

    private void Awake() {
        mainCamera = Camera.main;
        bladeCollider = GetComponent<Collider>();
        bladeTrail = GetComponentInChildren<TrailRenderer>();
    }

    private void OnEnable() {
        StopSlicing();
    }

    private void OnDisable() {
        StopSlicing();
    }

    private void Update() {
        // Slices fruits when mouse button is held down
        if (Input.GetMouseButtonDown(0)) {
            StartSlicing();
        }

        // Stops slicing fruit when mouse button is not held down
        else if (Input.GetMouseButtonUp(0)) {
            StopSlicing();
        }

        else if (slicing) {
            ContinueSlicing();
        }
    }

    private void StartSlicing() {
        Vector3 position = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        position.z = 0f;
        transform.position = position;

        slicing = true;
        bladeCollider.enabled = true;
        bladeTrail.enabled = true;
        bladeTrail.Clear();
    }

    private void StopSlicing() {
        slicing = false;
        bladeCollider.enabled = false;
        bladeTrail.enabled = false;
    }

    // Blade follows the mouse cursor
    private void ContinueSlicing() {
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0f;

        direction = newPosition - transform.position;

        float velocity = direction.magnitude / Time.deltaTime;
        bladeCollider.enabled = velocity > minSliceVelocity;

        transform.position = newPosition;
    }


}
