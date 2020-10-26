using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private Rigidbody2D rigidbody;
    private bool isGrounded;

    [Range(1, 10)]
    public float speed;

    [Range(1, 10)]
    public float jumpVelocity;

    [Range(1, 5)]
    public float fallingMultiplier;

    void Start() {
        rigidbody = GetComponent<Rigidbody2D>();
        isGrounded = true;
    }

    void Update() { }

    void FixedUpdate() {
        float horizontalMovement = Input.GetAxis("Horizontal");
        
        if(Input.GetKey(KeyCode.Space) && isGrounded == true) {
            rigidbody.velocity += Vector2.up * jumpVelocity;
            isGrounded = false;
        }

        if (rigidbody.velocity.y < 0) {
            rigidbody.velocity += Vector2.up * Physics2D.gravity.y * (fallingMultiplier - 1) * Time.fixedDeltaTime;
        }
        else if (rigidbody.velocity.y > 0 && !Input.GetKey(KeyCode.Space)) {
            rigidbody.velocity += Vector2.up * Physics2D.gravity.y * (fallingMultiplier - 1) * Time.fixedDeltaTime;
        }

        rigidbody.velocity = new Vector2(horizontalMovement * speed, rigidbody.velocity.y);

        if(rigidbody.position.y < -10.0f) {
            rigidbody.position = new Vector2(0.0f, 1.0f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.name == "Ground" || collision.collider.name == "Platforms") {
            isGrounded = true;
        }
        if(collision.collider.name == "Traps") {
            rigidbody.position = new Vector2(0.0f, 1.0f);
        }
    }

}
