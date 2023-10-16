using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;

    public int scoreP1;
    public int scoreP2;

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
