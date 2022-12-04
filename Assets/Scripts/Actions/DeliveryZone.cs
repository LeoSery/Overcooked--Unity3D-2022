using System.Collections.Generic;
using UnityEngine;

public class DeliveryZone : MonoBehaviour
{
    public enum TypeOfObjectInZone
    {
        None,
        Plate
    }

    public bool playerIsHere = false;

    private GameManager gameManager;
    private PickObject pickObject;
    private Food foodScript;
    private FryingPan fryingPan;
    private Plate plateScript;
    private RecipesManager recipesManager;
    private PlateSpawner plateSpawner;
    private UIManager uiManager;

    private GameObject dishLocation;
    private GameObject Player;
    private bool sameNumberOfIngredients = false;

    //debug
    public List<GameObject> currentDishIngredients;
    public List<GameObject> ingredientsDishToPrepare;
    public GameObject testedObject;
    public GameObject refObject;

    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        recipesManager = GameObject.Find("ReceipiesManager").GetComponent<RecipesManager>();
        plateSpawner = GameObject.FindGameObjectWithTag("PlateSpawner").GetComponent<PlateSpawner>();
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        Player = GameObject.FindGameObjectWithTag("Player");
        pickObject = Player.GetComponent<PickObject>();
        dishLocation = transform.GetChild(1).gameObject;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIsHere && pickObject.ObjectIsGrab)
        {
            DeliverTheDish();
        }
    }

    void DeliverTheDish()
    {
        GameObject currentObject = pickObject.objectPicked;
        if (CheckIfObjectIsADish(currentObject))
        {
            if (CheckInfosOfDish(currentObject))
            {
                currentObject.transform.position = dishLocation.transform.position;
                currentObject.transform.SetParent(dishLocation.transform);
                pickObject.objectPicked = null;
                pickObject.ObjectIsGrab = false;

                IngrementPlayerPoints();
                DestroyImmediate(currentObject);
                plateSpawner.InstanciateNewPlate();

            }
            else
            {
                if (!sameNumberOfIngredients)
                {
                    Debug.LogWarning("DeliveryZone > You must deliver the same number of ingredients as in the recipe.");
                    //TODO: Show UI message with this text.
                    Loose();
                }
                else
                {
                    Loose();
                    DestroyImmediate(currentObject);
                    plateSpawner.InstanciateNewPlate();
                }
            }
        }
        else
        {
            Debug.LogWarning("DeliveryZone > Only a plate containing ingredients can be delivered.");
            //TODO: Show UI message with this text.
        }
    }

    bool CheckIfObjectIsADish(GameObject currentObject)
    {
        if (currentObject.TryGetComponent<Food>(out foodScript)) { return false; }
        else if (currentObject.TryGetComponent<FryingPan>(out fryingPan)) { return false; }
        else if (currentObject.TryGetComponent<Plate>(out plateScript)) { return true; }
        return false;
    }

    bool CheckInfosOfDish(GameObject currentPlate)
    {
        Recipie dishToPrepare = recipesManager.GetNextDishToDeliver();

        List<GameObject> currentDishIngredients = new List<GameObject>();
        currentDishIngredients.AddRange(currentPlate.GetComponent<Plate>().dishIngredients);

        ingredientsDishToPrepare = recipesManager.GetAllIngredients(dishToPrepare);

        if (ingredientsDishToPrepare.Count != currentDishIngredients.Count)
        {
            sameNumberOfIngredients = false;
            return false;
        }
        sameNumberOfIngredients = true;

        for (int i = 0; i < currentDishIngredients.Count; i++)
        {
            for (int j = 0; j < ingredientsDishToPrepare.Count; j++)
            {
                if (currentDishIngredients[i].name.Contains(ingredientsDishToPrepare[j].name))
                {
                    currentDishIngredients.RemoveAt(i);
                    i = -1;
                    break;
                }
            }
        }
        return currentDishIngredients.Count == 0;
    }

    void IngrementPlayerPoints()
    {
        uiManager.IncreaseScore(100);
        recipesManager.GenerateNewRecipe(1);
    }

    void Loose()
    {
        uiManager.ShowLoseScreen();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            foreach (GameObject deliveryZone in gameManager.deliveryZones)
            {
                if (deliveryZone == transform.gameObject)
                {
                    playerIsHere = true;
                }
                else
                {
                    var deliveryZoneScript = deliveryZone.GetComponent<DeliveryZone>();
                    deliveryZoneScript.playerIsHere = false;
                }
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            foreach (GameObject deliveryZone in gameManager.deliveryZones)
            {
                if (deliveryZone == transform.gameObject)
                {
                    playerIsHere = false;
                }
            }
        }
    }
}
