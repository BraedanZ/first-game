using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Winner : MonoBehaviour {

    Player playerScript;

    void Start() {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void OnTriggerEnter2D(Collider2D hitObject) {
        if (hitObject.tag == "Player") {
            // playerScript.Win();
            GameController.instance.ShowGameOverScreen();
        }
    }
}
