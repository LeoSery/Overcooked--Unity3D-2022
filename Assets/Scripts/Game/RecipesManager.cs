using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class Receipies
{
    public List<GameObject> Ingredients = new();
}

public class RecipesManager : MonoBehaviour
{
    public List<Receipies> listReceipies = new();

    private GameManager gameManager;

    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            GenerateReceipe(2);
        }
    }

    void Start()
    {
        //GenerateReceipe(3);
    }

    void GenerateReceipe(int nbReceipes)
    {
        for (int i = 0; i < nbReceipes; i++)
        {
            Receipies NewReceipie = new();
            int nbIngredientsInDish = Random.Range(1, 4);
            for (int j = 0; j < nbIngredientsInDish; j++)
            {
                GameObject NewIngredient = TakeRandomIngredient();
                NewReceipie.Ingredients.Add(NewIngredient);
            }
            listReceipies.Add(NewReceipie);
        }
    }

    GameObject TakeRandomIngredient()
    {
        GameObject Ingredient = gameManager.uncutItems[Random.Range(0, gameManager.uncutItems.Length)];
        return Ingredient;
    }
}
