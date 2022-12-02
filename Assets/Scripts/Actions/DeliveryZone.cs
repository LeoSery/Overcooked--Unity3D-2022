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
    private Plate plateScript;

    private GameObject dishLocation;
    private GameObject Player;

    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
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
        GameObject currentPlate = pickObject.objectPicked;
        CheckInfosOfDish(currentPlate);

        // if the ingredient is the same than the recipies, give points
        // else loose game

        //temp
        currentPlate.transform.position = dishLocation.transform.position;
        currentPlate.transform.SetParent(dishLocation.transform);
        pickObject.objectPicked = null;
        pickObject.ObjectIsGrab = false;

        plateScript = currentPlate.GetComponent<Plate>();
        List<GameObject> dishIngredients = plateScript.dishIngredients;
        foreach (GameObject Ingredient in dishIngredients)
        {
            foodScript = Ingredient.GetComponent<Food>();
            foodScript.currentState = Food.State.Served;
        }
        DestroyImmediate(currentPlate); //temp action
    }

    void CheckInfosOfDish(GameObject currentPlate)
    {
        List<GameObject> dishIngredients = currentPlate.GetComponent<Plate>().dishIngredients;

        foreach (GameObject Ingredient in dishIngredients)
        {
            Debug.Log(Ingredient.gameObject.name);
        }
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
