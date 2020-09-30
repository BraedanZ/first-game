using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushable : MonoBehaviour {

    Player playerScript;
    bool isLeftButtonDown;

    void Start() {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void OnMouseDown() {
        isLeftButtonDown = true;
    }

    void OnMouseUp() {
        isLeftButtonDown = false;

    }

    void Update() {
        Vector2 thisPosition = new Vector2(this.transform.position.x, this.transform.position.y);
        if (isLeftButtonDown) {
            playerScript.Push(thisPosition);
        }
    }
}
