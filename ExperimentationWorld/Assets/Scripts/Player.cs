﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Player player;

    public GameObject winPanel;

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

    bool isPushing;
    public float pushForce;

    float playerXValue;
    float playerYValue;

    public float momentumDuration;
    private float momemtumRemaining;
    bool hasMomentum;

    void Start() {
        player = this;
        rigidBody = GetComponent<Rigidbody2D>();  
    }

    void Update() {
        // Jump();
        // WallSlide();
        // WallJump();
        GroundCheck();
        if (input < 0 && facingRight) {
            Flip();
        } else if (input > 0 && !facingRight) {
            Flip();
        }
        CheckMomentum();
        DecrementMomentumRemaining();
    }

    void FixedUpdate() {
        Walk();
        PlayerPosition();
    }

    private void GroundCheck() {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
    }

    private void Walk() {
        input = Input.GetAxisRaw("Horizontal");
        
        if (!isGrounded && hasMomentum) {
            rigidBody.velocity = new Vector2(input * speed / 4 + rigidBody.velocity.x, rigidBody.velocity.y);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, -5 * input), 15.0f * Time.deltaTime);
        } else if(!isGrounded) {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, rigidBody.velocity.y);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, 0), 15.0f * Time.deltaTime);
        } 
        else {
            rigidBody.velocity = new Vector2(input * speed, rigidBody.velocity.y);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, 0), 15.0f * Time.deltaTime);
        }
    }

    private void Flip() {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        facingRight = !facingRight;
    }

    private void SetWallJumpingToFalse() {
        wallJumping = false;
    }

    private void PlayerPosition() {
        playerXValue = player.transform.position.x;
        playerYValue = player.transform.position.y;
    }

    public void Push(Vector2 pushablePosition) {
        float pushableDistanceXSquared = Mathf.Pow((playerXValue - pushablePosition.x), 2);
        float pushableDistanceYSquared = Mathf.Pow((playerYValue - pushablePosition.y), 2);
        float pushableDistanceTotal = Mathf.Sqrt(pushableDistanceXSquared + pushableDistanceYSquared);
        pushableDistanceTotal = Mathf.Max(pushableDistanceTotal, 2.5f);
        Vector2 pushDirection = new Vector2(playerXValue - pushablePosition.x, playerYValue - pushablePosition.y);
        pushDirection.Normalize();
        Vector2 pushVector = pushDirection * pushForce / Mathf.Pow(pushableDistanceTotal, 2);
        rigidBody.AddForce(pushVector);
    }

    public void SetIsPushingTrue() {
        isPushing = true;
        momemtumRemaining = momentumDuration;
    }

    public void SetIsPushingFalse() {
        isPushing = false;
    }

    public Pusher GetClosestPushable(Pusher[] pushers) {
        Pusher curMin = null;
        float minDist = Mathf.Infinity;
        Vector2 currentPos = transform.position;
        for (int i = 0; i < pushers.Length; i++) {
            Pusher cur = pushers[i];
            float dist = Vector2.Distance(cur.transform.position, currentPos);
            if (dist < minDist) {
                curMin = cur;
                minDist = dist;
            }
        }
        return curMin;
    }

    private void CheckMomentum() {
        if (momemtumRemaining > 0) {
            hasMomentum = true;
        } else {
            hasMomentum = false;
        }
    }

    private void DecrementMomentumRemaining() {
        if (momemtumRemaining > 0) {
            momemtumRemaining -= Time.deltaTime;
        }
    }

    // private void Jump() {
    //     if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && isGrounded) {
    //         rigidBody.velocity = Vector2.up * jumpForce;
    //     }
    // }

    // private void WallSlide() {
    //     isToucingFront = Physics2D.OverlapCircle(frontCheck.position, checkRadius, whatIsGround);
    //     if ((isToucingFront && !isGrounded && input != 0) || (wallSliding && isToucingFront && !isGrounded)) {
    //         wallSliding = true;
    //         wallDirectionRight = facingRight;
    //         timeSinceWallSlide = startTimeSinceWallSlide;
            
    //     } else if (timeSinceWallSlide > 0) {
    //             timeSinceWallSlide -= stopSlideSpeed * Time.deltaTime;
    //             wallSliding = true;
    //     } else {
    //         wallSliding = false;
    //     }
    //     if (wallSliding) {
    //         rigidBody.velocity = new Vector2(rigidBody.velocity.x, Mathf.Clamp(rigidBody.velocity.y, -wallSlidingSpeed, float.MaxValue));
    //     }
    // }

    // private void WallJump() {
    //     if (Input.GetKeyDown(KeyCode.UpArrow) && wallSliding) {
    //         wallJumping = true;
    //         Invoke("SetWallJumpingToFalse", wallJumpTime);
    //     }


    //     if (wallJumping) {
    //         if (wallDirectionRight) {
    //             rigidBody.velocity = new Vector2(xWallForce * -1, yWallForce);
    //         } else {
    //             rigidBody.velocity = new Vector2(xWallForce, yWallForce);
    //         }
    //     }
    // }
}