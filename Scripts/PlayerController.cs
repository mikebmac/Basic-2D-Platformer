using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float xSpeed = 10f;
    private float movementSmoothing = 0.3f;
    public float jumpForce;
    private float xInput = 0f;
    private bool facingRight = true;
    private Vector3 refVelocity = Vector3.zero;

    private bool isGrounded;
    public Transform groundCheck;

    public SpriteRenderer sprite;
    private Rigidbody2D rb;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        isGrounded = Physics2D.Linecast(transform.position, groundCheck.transform.position, 1 << LayerMask.NameToLayer("Ground"));

        xInput = Input.GetAxis("Horizontal");

        // Move the character by finding the target velocity
        Vector3 targetVelocity = new Vector2(xInput * xSpeed, rb.velocity.y);
        // And then smoothing it out and applying it to the character
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref refVelocity, movementSmoothing);

        // Direction
        if (facingRight == false && xInput > 0f) {
            Flip();
        } else if (facingRight == true && xInput < 0f) {
            Flip();
        }

        // Jump
        if (Input.GetAxis("Jump") > 0 && isGrounded) {
            // Remove any velocity before a jump
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce);
        }
    }

    private void Flip() {
        facingRight = !facingRight;

        sprite.flipX = !facingRight;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Enemy") {
            transform.position = new Vector3(0f, 0f, 0f);
        }
    }
}
