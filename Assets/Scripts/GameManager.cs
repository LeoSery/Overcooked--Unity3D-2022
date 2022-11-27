using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("GamesObjects :")]
    [Header("Uncut Items :")]
    public GameObject[] uncutItems;
    public GameObject[] uncutModels;

    [Header("Cuted Items :")]
    public GameObject[] cutedItems;
    public GameObject[] cutedModels;

    [Header("Coocked Items :")]
    public GameObject[] coockedItems;
    public GameObject[] coockedModels;

    [Header("Spawners :")]
    public GameObject[] foodSpawners;
    public GameObject[] cuttingBoards;
    public GameObject[] gazCoockers;

    [Header("Coocking tools :")]
    public GameObject[] fryingPans;

    void Start()
    {
        foodSpawners = GameObject.FindGameObjectsWithTag("FoodSpawner");
        cuttingBoards = GameObject.FindGameObjectsWithTag("CuttingBoard");
        gazCoockers = GameObject.FindGameObjectsWithTag("GazCoocker");

        var tempFryingPans = GameObject.FindGameObjectsWithTag("FryingPan");
        for(int i = 0; i < tempFryingPans.Length; i++)
        {
            fryingPans[i] = tempFryingPans[i].transform.parent.gameObject;
        }
    }
}