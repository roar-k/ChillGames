using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyPlayer : MonoBehaviour
{
    public GameManager_Flappy gameManager;

    [Header("Sprites")]
    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    private int spriteIndex;

    private Vector3 direction;

    [Header("Physics")]
    public float gravity = -9.8f;
    public float strength = 5f;
    public float tilt = 5f;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        // Runs the AnimateSprite method repeatedly every 0.15s
        InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f);
    }

    private void OnEnable() {
        Vector3 position = transform.position;
        position.y = 0f;
        transform.position = position;
        direction = Vector3.zero;
    }

    private void Update() {
        // Bird goes up if spacebar or mousebutton is pressed
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
            direction = Vector3.up * strength;
        }

        // Makes the bird constantly move downwards
        direction.y += gravity * Time.deltaTime;
        transform.position += direction * Time.deltaTime;

        // Tilts the bird base on the direction
        Vector3 rotation = transform.eulerAngles;
        rotation.z = direction.y * tilt;
        transform.eulerAngles = rotation;
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

    private void OnTriggerEnter2D(Collider2D other) {
        // Triggers Game Over when player hits an obstacle
        if (other.gameObject.tag == "Obstacle") {
            gameManager.GameOver();
        }

        // Increases score when player goes through a pipe
        else if (other.gameObject.tag == "Scoring") {
            gameManager.IncreaseScore();
        }
    }
}
