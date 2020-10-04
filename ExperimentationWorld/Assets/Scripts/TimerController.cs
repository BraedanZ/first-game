using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class TimerController : MonoBehaviour {
    
    public static TimerController instance;

    public Text timeCounter;

    private TimeSpan timePlaying;

    private bool timerGoing;

    private float elapsedTime;

    private float startTime;


    private void Awake() {
        instance = this;
    }

    private void Start() {
        timeCounter.text = "00:00:00";
        timerGoing = false;
    }

    public void BeginTimer() {
        timerGoing = true;
        startTime = Time.time;
        elapsedTime = 0f;

        StartCoroutine(UpdateTimer());
    }

    public void EndTimer() {
        timerGoing = false;
    }

    private IEnumerator UpdateTimer() {
        while (timerGoing) {
            elapsedTime += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);
            string timePlayingStr = timePlaying.ToString("hh':'mm':'ss'.'ff");
            timeCounter.text = timePlayingStr;

            yield return null;
        }
    }
}
