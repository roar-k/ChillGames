using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyBackground : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    public float animationSpeed = 0.05f;

    private void Awake() {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update() {
        // Constantly increases the offset of the background material making it seem like it is moving right
        meshRenderer.material.mainTextureOffset += new Vector2(animationSpeed * Time.deltaTime, 0);
    }
}
