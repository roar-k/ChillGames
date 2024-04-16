using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class DinoPlayer : MonoBehaviour
{
    private CharacterController character;
    private Vector3 direction;

    public float gravity = 20f;
    public float jumpForce = 8f;

    private void Awake() {
        character = GetComponent<CharacterController>();
    }

    private void OnEnable() {
        direction = Vector3.zero;
    }

    private void Update() {
        direction += Vector3.down * gravity * Time.deltaTime;

        if (character.isGrounded) {
            direction = Vector3.down;

            if (Input.GetButton("Jump")) {
                direction = Vector3.up * jumpForce;
            }
        }

        character.Move(direction * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Obstacle")) {
            GameManager_DinoGame.Instance.GameOver();
        }
    }
}
