using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private Rigidbody2D rb2d;
    private bool isGrounded;

    [Range(1, 10)]
    public float speed;

    [Range(1, 10)]
    public float jumpVelocity;

    [Range(1, 5)]
    public float fallingMultiplier;

    public Score score;

    void Start() {
        rb2d = GetComponent<Rigidbody2D>();
        isGrounded = true;
    }

    void Update() { }

    void FixedUpdate() {
        float horizontalMovement = Input.GetAxis("Horizontal");
        
        if(Input.GetKey(KeyCode.Space) && isGrounded == true) {
            rb2d.velocity += Vector2.up * jumpVelocity;
            isGrounded = false;
        }

        if (rb2d.velocity.y < 0) {
            rb2d.velocity += Vector2.up * Physics2D.gravity.y * (fallingMultiplier - 1) * Time.fixedDeltaTime;
        }
        else if (rb2d.velocity.y > 0 && !Input.GetKey(KeyCode.Space)) {
            rb2d.velocity += Vector2.up * Physics2D.gravity.y * (fallingMultiplier - 1) * Time.fixedDeltaTime;
        }

        rb2d.velocity = new Vector2(horizontalMovement * speed, rb2d.velocity.y);

        if(rb2d.position.y < -10.0f) {
            rb2d.position = new Vector2(0.0f, 1.0f);
            score.Reset();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.name == "Ground" || collision.collider.name == "Platforms") {
            isGrounded = true;
        }
        if(collision.collider.name == "Traps") {
            rb2d.position = new Vector2(0.0f, 1.0f);
            score.Reset();
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.name == "Trophy") {
            Destroy(collider.gameObject);
            score.BankScore();
            GameController.NextLevel();
        }
    }

}
