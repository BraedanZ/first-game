using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public static GameController instance = null;

    public Text timeCounter;

    private float startTime, elapsedTime;

    TimeSpan timePlaying;

    public bool gamePlaying { get; private set; }

    private void Awake() {
        instance = this;
    }

    private void Start() {
        PlayGame();
    }

    public void PlayGame() {
        gamePlaying = true;
        startTime = Time.time;
    }

    public void LoadGameScene() {
        SceneManager.LoadScene("VersionOne");
    }

    private void Update() {
        if (gamePlaying) {
            elapsedTime = Time.time - startTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);

            string timePlayingStr = timePlaying.ToString("hh':'mm':'ss'.'ff");
            timeCounter.text = timePlayingStr;
        }
    }

    public void StopTimer() {
        gamePlaying = false;
    }
}
