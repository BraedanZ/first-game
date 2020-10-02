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

    void Start() {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        thisPosition = new Vector2(this.transform.position.x, this.transform.position.y);
    }

    void Update() {
        clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        SetDistance();
        if (Input.GetMouseButtonDown(0)) {
            if (distance <= clickableRadius) {
                isClicking = true;
                playerScript.SetIsPushingTrue();
            }
        } else if (Input.GetMouseButtonUp(0)) {
            isClicking = false;
            playerScript.SetIsPushingFalse();
        }
        if (isClicking) {
            playerScript.Push(thisPosition);
        }
    }

    void SetDistance() {
        float distanceX = Mathf.Pow(Mathf.Abs(thisPosition.x - clickPosition.x), 2);
        float distanceY = Mathf.Pow(Mathf.Abs(thisPosition.y - clickPosition.y), 2);
        distance = Mathf.Sqrt(distanceX + distanceY);
    }
}
