using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Recipie
{
    public List<GameObject> Ingredients = new();
}

public class RecipesManager : MonoBehaviour
{
    public List<Recipie> listRecipies = new();

    private GameManager gameManager;
    private UIManager uiManager;

    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    void Start()
    {
        GenerateRecipe(3);
        uiManager.UpdateRecipieListUI();
    }

    public void GenerateRecipe(int nbReceipes)
    {
        for (int i = 0; i < nbReceipes; i++)
        {
            Recipie NewReceipie = new();
            int nbIngredientsInDish = Random.Range(1, 4);
            for (int j = 0; j < nbIngredientsInDish; j++)
            {
                GameObject NewIngredient = TakeRandomIngredient();
                NewReceipie.Ingredients.Add(NewIngredient);
            }
            listRecipies.Add(NewReceipie);
        }
    }

    public void GenerateNewRecipe(int nbRecipies)
    {
        listRecipies.RemoveAt(0);
        GenerateRecipe(nbRecipies);
        uiManager.UpdateRecipieListUI();
    }

    GameObject TakeRandomIngredient()
    {
        GameObject Ingredient = gameManager.uncutItems[Random.Range(0, gameManager.uncutItems.Length)];
        return Ingredient;
    }

    public Recipie GetNextDishToDeliver()
    {
        Recipie Receipie = listRecipies.First();
        return Receipie;
    }

    public List<GameObject> GetAllIngredients(Recipie dishToPrepare)
    {
        return dishToPrepare.Ingredients;
    }
}
