using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PusherManager : MonoBehaviour
{

    Player playerScript;

    GameObject[] pusherGameObjects;
    Pusher[] pushers;

    Pusher nearestPusher;

    void Start() {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        pusherGameObjects = GameObject.FindGameObjectsWithTag("Pusher");
        pushers = new Pusher[pusherGameObjects.Length];
        for (int i = 0; i < pusherGameObjects.Length; i++) {
            pushers[i] = pusherGameObjects[i].GetComponent<Pusher>();
        }
    }

    void Update() {
        foreach (Pusher pusher in pushers) {
            pusher.UnsetClosest();
        }
        nearestPusher = playerScript.GetClosestPushable(pushers);
        nearestPusher.SetClosest();
    }
}
