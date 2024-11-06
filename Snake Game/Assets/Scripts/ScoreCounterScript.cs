using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreCounterScript : MonoBehaviour
{
    int score = 0;

    [SerializeField] TextMeshProUGUI scoreText;

    public void IncreaseScore()
    {
        score += 10;
        scoreText.text = score.ToString();
    }

    public int GetScore()
    {
        return score;
    }
}
