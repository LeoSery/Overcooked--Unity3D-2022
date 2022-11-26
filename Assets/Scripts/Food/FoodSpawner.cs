using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public enum SpawnerType
    {
        PotatoSpawner,
        SteakSpawner,
        SaladSpawner
    };

    [Header("Settings : ")]
    public SpawnerType spawnerType;

    [Header("GameObjects : ")]
    public GameObject ObjectToSpawn;

    private bool playerIsHere = false;
    private GameManager gameManager;
    private PickObject pickObject;

    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        pickObject = GameObject.Find("Player").GetComponent<PickObject>();
        CheckSpawnerType();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIsHere && pickObject.ObjectIsGrab == false)
        {
            GameObject distributedObject = Instantiate(ObjectToSpawn, pickObject.objectPickedPos.transform.position, Quaternion.identity, pickObject.transform);
            pickObject.objectPicked = distributedObject;
            pickObject.ObjectIsGrab = true;
            SetObjectSetings(distributedObject);
        }
    }

    void CheckSpawnerType()
    {
        switch (spawnerType)
        {
            case SpawnerType.PotatoSpawner:
                ObjectToSpawn = gameManager.uncutItems[0];
                break;
            case SpawnerType.SteakSpawner:
                ObjectToSpawn = gameManager.uncutItems[1];
                break;
            case SpawnerType.SaladSpawner:
                ObjectToSpawn = gameManager.uncutItems[2];
                break;
        }
    }

    void SetObjectSetings(GameObject distributedObject)
    {
        var foodScript = distributedObject.GetComponent<Food>();
        foodScript.currentFood = ObjectToSpawn;
        foodScript.currentState = Food.State.CanBeCut;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            foreach (GameObject Spawner in gameManager.foodSpawners)
            {
                if (Spawner == transform.gameObject)
                {
                    playerIsHere = true;
                }
                else
                {
                    var foodScript = Spawner.GetComponent<FoodSpawner>();
                    foodScript.playerIsHere = false;
                }
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            foreach (GameObject Spawner in gameManager.foodSpawners)
            {
                if (Spawner == transform.gameObject)
                {
                    playerIsHere = false;
                }
            }
        }
    }
}
