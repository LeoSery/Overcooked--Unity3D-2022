using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public List<TextMeshProUGUI> ingredientsTexts;
    public GameObject LoseMenuUI;
    public int gameScore;

    private RecipesManager recipiesManager;

    void Awake()
    {
        recipiesManager = GameObject.Find("ReceipiesManager").GetComponent<RecipesManager>();
    }

    void Start()
    {
        scoreText.text = "Score : " + gameScore + "$";
    }

    public void IncreaseScore(int scoreToAdd)
    {
        gameScore += scoreToAdd;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        scoreText.text = "Score : " + gameScore + "$";
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

    public void UpdateRecipieListUI()
    {
        List<GameObject> currentRecipeIngredients = recipiesManager.GetAllIngredients(recipiesManager.GetNextDishToDeliver());
        for (int i = 0; i < currentRecipeIngredients.Count; i++)
        {
            ingredientsTexts[i].text = currentRecipeIngredients[i].name;
        }
    }

    public void ShowLoseScreen()
    {
        LoseMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }
}
