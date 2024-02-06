using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

/*
This script manages all "game" elements. Currently this is only keeping track of the score.
TODO: countdown timer for timed option.
Has vars for score display text, and scores for each player.
Has a method to update score.
*/
public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;

    public int scoreP1;
    public int scoreP2;

    MainManager mainManager;

    void Start()
    {
        mainManager = MainManager.Instance;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)
                && !mainManager.isGamePaused)
        {
            mainManager.PauseManager();
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
    }
}
