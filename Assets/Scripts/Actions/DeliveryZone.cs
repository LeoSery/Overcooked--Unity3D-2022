using System.Collections.Generic;
using System.Collections;
using System.Linq;
using Unity.VisualScripting;
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
    private UIManager uiManager;

    private GameObject dishLocation;
    private GameObject Player;

    //debug
    public List<GameObject> currentDishIngredients;
    public List<GameObject> ingredientsDishToPrepare;
    public GameObject testedObject;
    public GameObject refObject;

    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        recipesManager = GameObject.Find("ReceipiesManager").GetComponent<RecipesManager>();
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
                IngrementPlayerPoints();
            else
                Loose();

            currentObject.transform.position = dishLocation.transform.position;
            currentObject.transform.SetParent(dishLocation.transform);
            pickObject.objectPicked = null;
            pickObject.ObjectIsGrab = false;
            plateScript = currentObject.GetComponent<Plate>();
            DestroyImmediate(currentObject);
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
            Debug.LogWarning("DeliveryZone > Your dish does not contain the same number of ingredients as in the requested recipe.");
            //TODO: Show UI message with this text.
            return false;
        }

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
    }

    void Loose()
    {
        Debug.LogWarning("You loose");
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
