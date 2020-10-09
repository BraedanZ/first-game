using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pusher : MonoBehaviour
{
    Player playerScript;
    bool isClicking;
    Vector2 thisPosition;
    Vector2 clickPosition;
    public int clickableRadius;
    float distance;

    public float pushDuration;
    private float pushRemaining;

    public float timeBetweenPushes;
    private float timeUntilPush;

    bool isClosest;

    void Start() {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        thisPosition = new Vector2(this.transform.position.x, this.transform.position.y);
        timeUntilPush = 0;
        pushRemaining = 0;
    }

    void Update() {
        clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        SetDistance();
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (isClosest) {
                PushAvailible();
            }
        }
        if (pushRemaining <= 0) {
            SetNotPushing();
        }
        DecrementPushRemaining();
        DecrementTimeUntilPush();
    }

    void FixedUpdate() {
        TriggerPush();
    }

    private void SetDistance() {
        float distanceX = Mathf.Pow(Mathf.Abs(thisPosition.x - clickPosition.x), 2);
        float distanceY = Mathf.Pow(Mathf.Abs(thisPosition.y - clickPosition.y), 2);
        distance = Mathf.Sqrt(distanceX + distanceY);
    }

    private void PushAvailible() {
        Debug.Log(timeUntilPush);
        if (timeUntilPush <= 0) {
            timeUntilPush = timeBetweenPushes;
            pushRemaining = pushDuration;
        }
    }

    private void TriggerPush() {
        if (pushRemaining > 0) {
            playerScript.SetIsPushingTrue();
            playerScript.Push(thisPosition);
        }
    }

    private void DecrementPushRemaining() {
        if (pushRemaining > 0) {
            pushRemaining -= Time.deltaTime;
        }
    }

    private void DecrementTimeUntilPush() {
        if (timeUntilPush > 0) {
            timeUntilPush -= Time.deltaTime;
        }
    }

    private void SetNotPushing() {
        playerScript.SetIsPushingFalse();
    }

    public void SetClosest() {
        isClosest = true;
    }

    public void UnsetClosest() {
        isClosest = false;
    }
}
