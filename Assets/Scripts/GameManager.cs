using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] uncutItems;
    public GameObject[] cutedItems;
    public GameObject[] foodSpawners;
    public GameObject[] cuttingBoards;

    void Start()
    {
        foodSpawners = GameObject.FindGameObjectsWithTag("FoodSpawner");
        cuttingBoards = GameObject.FindGameObjectsWithTag("CuttingBoard");
    }
}