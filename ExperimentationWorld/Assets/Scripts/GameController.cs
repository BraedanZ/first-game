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

    public GameObject winPanel;

    public GameObject gameOverlay;

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

            string timePlayingStr = timePlaying.ToString("mm':'ss'.'ff");
            timeCounter.text = timePlayingStr;
        }
    }

    public void StopTimer() {
        gamePlaying = false;
    }

    public void ShowGameOverScreen() {
        GameController.instance.StopTimer();
        string timePlayingStr = timePlaying.ToString("mm':'ss'.'ff");
        winPanel.transform.Find("FinalTime").GetComponent<Text>().text = timePlayingStr;
        winPanel.SetActive(true);
        gameOverlay.SetActive(false);
    }
}
