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

    void Start() {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Update() {
        thisPosition = new Vector2(this.transform.position.x, this.transform.position.y);
        clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0)) {
            float distance = Mathf.Sqrt(Mathf.Pow(Mathf.Abs(thisPosition.x - clickPosition.x), 2) + Mathf.Pow(Mathf.Abs(thisPosition.x - clickPosition.x), 2));
            if (distance <= clickableRadius) {
                isClicking = true;
            }
        } else if (Input.GetMouseButtonUp(0)) {
            isClicking = false;
        }
        if (isClicking) {
            playerScript.Push(thisPosition);
        }
    }
}
