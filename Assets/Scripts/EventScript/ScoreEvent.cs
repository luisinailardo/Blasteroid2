using UnityEngine;
using TMPro;

public class ScoreEvent : MonoBehaviour
{
    [SerializeField] private int score;
    [SerializeField] private TMP_Text scoreText;

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        string scoreTextString = score.ToString();
        scoreTextString = scoreTextString.PadLeft(6, '0');
        scoreText.text = scoreTextString;
    }

    public int GetScore()
    {
        return score;
    }
}