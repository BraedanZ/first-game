using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rigidBody;
    float input;
    bool facingRight;

    public float speed;

    bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    public float jumpForce;

    bool isToucingFront;
    public Transform frontCheck;
    bool wallSliding;
    public float wallSlidingSpeed;
    float timeSinceWallSlide;
    public float startTimeSinceWallSlide;
    public float stopSlideSpeed;
    bool wallDirectionRight;

    bool wallJumping;
    public float xWallForce;
    public float yWallForce;
    public float wallJumpTime;

    bool isLeftButtonDown;
    bool isRightButtonDown;

    float mouseXValue;
    float mouseYValue;

    // Start is called before the first frame update
    void Start() {
        rigidBody = GetComponent<Rigidbody2D>();  
    }

    // Update is called once per frame
    void Update() {
        Walk();
        Jump();
        WallSlide();
        WallJump();
        MousePosition();
        SetMouseButtons();
        Push();
    }

    private void Walk() {
        input = Input.GetAxisRaw("Horizontal");
        rigidBody.velocity = new Vector2(input * speed, rigidBody.velocity.y);

        if (input < 0 && facingRight) {
            Flip();
        } else if (input > 0 && !facingRight) {
            Flip();
        }
    }

    private void Jump() {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded) {
            rigidBody.velocity = Vector2.up * jumpForce;
        }
    }

    private void WallSlide() {
        isToucingFront = Physics2D.OverlapCircle(frontCheck.position, checkRadius, whatIsGround);
        if ((isToucingFront && !isGrounded && input != 0) || (wallSliding && isToucingFront && !isGrounded)) {
            wallSliding = true;
            wallDirectionRight = facingRight;
            timeSinceWallSlide = startTimeSinceWallSlide;
            
        } else if (timeSinceWallSlide > 0) {
                timeSinceWallSlide -= stopSlideSpeed * Time.deltaTime;
                wallSliding = true;
        } else {
            wallSliding = false;
        }
        if (wallSliding) {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, Mathf.Clamp(rigidBody.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
    }

    private void WallJump() {
        if (Input.GetKeyDown(KeyCode.UpArrow) && wallSliding) {
            wallJumping = true;
            Invoke("SetWallJumpingToFalse", wallJumpTime);
        }


        if (wallJumping) {
            if (wallDirectionRight) {
                rigidBody.velocity = new Vector2(xWallForce * -1, yWallForce);
            } else {
                rigidBody.velocity = new Vector2(xWallForce, yWallForce);
            }
        }
    }

    private void Flip() {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        facingRight = !facingRight;
    }

    private void SetWallJumpingToFalse() {
        wallJumping = false;
    }

    private void MousePosition() {
        mouseXValue = transform.position.x;
        mouseYValue = transform.position.y;
        // Debug.Log("X" + mouseXValue);
        // Debug.Log("Y" + mouseYValue);
    }

    private void SetMouseButtons() {
        isLeftButtonDown = Input.GetMouseButtonDown(0);
        isRightButtonDown = Input.GetMouseButtonDown(1);
    }

    private void Push() {

    }
}
