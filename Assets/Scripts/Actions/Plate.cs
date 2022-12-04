using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Plate : MonoBehaviour
{
    public enum TypeOfObjectInPlate
    {
        None,
        Food,
        CoockingTool
    }

    public TypeOfObjectInPlate typeOfObjectInPlate;
    public List<GameObject> dishIngredients;
    public bool playerIsHere = false;
    public bool plateIsEmpty = true;

    private GameObject foodLocation;
    private GameObject lastFoodPut;

    private GameManager gameManager;
    private PickObject pickObject;
    private Food lastFoodScript;
    private GameObject Player;

    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Player = GameObject.FindGameObjectWithTag("Player");
        pickObject = Player.GetComponent<PickObject>();
        foodLocation = transform.GetChild(1).gameObject;
    }

    void Update()
    {
        if (playerIsHere)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (pickObject.ObjectIsGrab)
                {
                    PutContentInPlate();
                }
            }
            else if (Input.GetKeyDown(KeyCode.F))
            {
                if (!pickObject.ObjectIsGrab && !plateIsEmpty)
                {
                    TakeLastContentOfPlate();
                }
            }
        }
    }

    void PutContentInPlate()
    {
        var currentContent = pickObject.objectPicked;
        CheckInfosOfObject(currentContent);

        if (typeOfObjectInPlate == TypeOfObjectInPlate.Food)
        {
            lastFoodPut = currentContent;
            lastFoodScript = lastFoodPut.GetComponent<Food>();

            if (lastFoodScript.currentState == Food.State.CanBePutInPlate)
            {
                lastFoodPut.transform.SetParent(foodLocation.transform);
                lastFoodPut.transform.position = foodLocation.transform.position;
                var currentCollider = lastFoodPut.GetComponent<Collider>();
                currentCollider.enabled = false;

                pickObject.objectPicked = null;
                pickObject.ObjectIsGrab = false;
                lastFoodScript.currentState = Food.State.CanBeServed;
                dishIngredients.Add(lastFoodPut);
                plateIsEmpty = false;
                currentContent = null;
                lastFoodPut = null;
                lastFoodScript = null;
            }
            else
            {
                currentContent = null;
                lastFoodPut = null;
                lastFoodScript = null;
            }
        }
        else
        {
            currentContent = null;
        }
    }

    void TakeLastContentOfPlate()
    {
        GameObject elementToGet = dishIngredients.Last();
        int indexElementToget = dishIngredients.IndexOf(elementToGet);
        Food foodScriptElementToGet = elementToGet.GetComponent<Food>();
        var currentCollider = elementToGet.GetComponent<Collider>();
        currentCollider.enabled = true;
        pickObject.objectPicked = elementToGet;
        pickObject.objectPicked.transform.SetParent(pickObject.objectPickedPos.transform);
        pickObject.objectPicked.transform.position = pickObject.objectPickedPos.transform.position;
        foodScriptElementToGet.currentState = Food.State.CanBePutInPlate;
        pickObject.ObjectIsGrab = true;
        dishIngredients.RemoveAt(indexElementToget);
    }

    void CheckInfosOfObject(GameObject objectPlaced)
    {
        var placedFoodScript = objectPlaced.GetComponent<Food>();
        var placedPanScript = objectPlaced.GetComponent<FryingPan>();
        var placedPlateScript = objectPlaced.GetComponent<Plate>();

        if (placedFoodScript != null && placedPanScript == null && placedPlateScript == null)
            typeOfObjectInPlate = TypeOfObjectInPlate.Food;
        else if (placedFoodScript == null && (placedPanScript != null || placedPlateScript != null))
            typeOfObjectInPlate = TypeOfObjectInPlate.CoockingTool;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            foreach (GameObject Plate in gameManager.plates)
            {
                if (Plate == transform.gameObject)
                {
                    playerIsHere = true;
                }
                else
                {
                    var plateScript = Plate.GetComponent<Plate>();
                    plateScript.playerIsHere = false;
                }
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            foreach (GameObject Plate in gameManager.plates)
            {
                if (Plate == transform.gameObject)
                {
                    playerIsHere = false;
                }
            }
        }
    }
}
