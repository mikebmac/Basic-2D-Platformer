using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Vector3 startLocation;
    private bool movingRight = true;
    public float speed = 2f;

    public Transform groundDetection;

    void Start() {
        startLocation = transform.position;
    }

    private void FixedUpdate() {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        // Check if touching ground
        bool isTouchingGround = Physics2D.Linecast(transform.position, groundDetection.position, 1 << LayerMask.NameToLayer("Ground"));

        // Touching Ground?
        if (isTouchingGround == false) {
            if (movingRight == true) {
                transform.eulerAngles = new Vector3(0f, -180f, 0f);
                movingRight = false;
            } else {
                transform.eulerAngles = new Vector3(0f, 0f, 0f);
                movingRight = true;
            }
        }
    }
}
