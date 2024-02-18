using UnityEngine;
using TMPro;
using System;

/*
This script manages all "game" elements. Currently this is only keeping track of the score.
Has vars for score display text, and scores for each player.
Has a method to update score.
*/
public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI gameWinnerText;

    public int scoreP1;
    public int scoreP2;
    float currGameTime;

    MainManager mainManager;

    void Start()
    {
        mainManager = MainManager.Instance;
        currGameTime = mainManager.gameTime;
        mainManager.isGameOver = mainManager.isGamePaused = false;
        Time.timeScale = 1;

        timerText.text = $"{(int)currGameTime}";
        if (!mainManager.isGameTimed) { timerText.gameObject.SetActive(false); }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)
                && !mainManager.isGamePaused && !mainManager.isGameOver)
        {
            mainManager.PauseManager();
        }

        if (!mainManager.isGamePaused && mainManager.isGameTimed && !mainManager.isGameOver)
        {
            UpdateTime();
        }
    }

    void UpdateTime()
    {
        currGameTime -= Time.deltaTime;
        timerText.text = $"{(int)Math.Ceiling(currGameTime)}";
        if (currGameTime < 0)
        {
            GameOver();
        }
    }

    //Adds int "scoreToAdd" to the "playerInt" player and updates the score text.
    public void UpdateScore(int scoreToAdd, int playerInt)
    {
        if (playerInt == 1)
        {
            scoreP1 += scoreToAdd;
        }
        else
        {
            scoreP2 += scoreToAdd;
        }

        scoreText.text = $"{scoreP1:D2} : {scoreP2:D2}";

        if ((scoreP1 >= mainManager.maxScore || scoreP2 >= mainManager.maxScore) && !mainManager.isGameTimed)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        mainManager.isGameOver = true;
        GameObject.FindGameObjectWithTag("Canvas").GetComponent<MenuManager>().GameOverScreenToggler();
        int winner = 1;
        if (scoreP1 < scoreP2)
        {
            winner = 2;
        }
        else if (scoreP1 == scoreP2)
        {
            gameWinnerText.text = "Draw!";
            Time.timeScale = 0;
            return;
        }
        gameWinnerText.text = $"Player {winner} wins!";

        Time.timeScale = 0;
    }
}
