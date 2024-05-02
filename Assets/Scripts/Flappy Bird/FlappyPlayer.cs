using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyPlayer : MonoBehaviour
{
    [Header("Sprites")]
    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    private int spriteIndex;

    private Vector3 direction;

    [Header("Physics")]
    public float gravity = -9.8f;
    public float strength = 5f;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        // Runs the AnimateSprite method repeatedly every 0.15s
        InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f);
    }

    private void Update() {
        // Bird goes up if spacebar or mousebutton is pressed
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
            direction = Vector3.up * strength;
        }

        // Makes the bird constantly move downwards
        direction.y += gravity * Time.deltaTime;
        transform.position += direction * Time.deltaTime;
    }

    private void AnimateSprite() {
        // Loops through the sprite array so the bird seems like it is flapping
        spriteIndex++;

        // Resets the loop when reaches the end of the sprites array
        if (spriteIndex >= sprites.Length) {
            spriteIndex = 0;
        }

        spriteRenderer.sprite = sprites[spriteIndex];
    }
}
