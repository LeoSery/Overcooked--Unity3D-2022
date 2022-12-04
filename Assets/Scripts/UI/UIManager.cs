using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public int gameScore;

    void Start()
    {
        scoreText.text = "Score : " + gameScore + "$";
    }

    void UpdateScoreUI()
    {
        scoreText.text = "Score : " + gameScore + "$";
    }

    public void IncreaseScore(int scoreToAdd)
    {
        gameScore += scoreToAdd;
        UpdateScoreUI();
    }

    public void UpdateTimerUI(int Minutes, int Seconds)
    {
        string formatedSeconds;
        if (Seconds < 10)
            formatedSeconds = "0" + Seconds.ToString();
        else
            formatedSeconds = Seconds.ToString();

        timerText.text = Minutes.ToString() + ":" + formatedSeconds;
    }
}
