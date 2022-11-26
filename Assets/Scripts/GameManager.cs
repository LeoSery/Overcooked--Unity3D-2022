using System.Collections;
using System.Collections.Generic;
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

    [Header("Spawners :")]
    public GameObject[] foodSpawners;
    public GameObject[] cuttingBoards;

    void Start()
    {
        foodSpawners = GameObject.FindGameObjectsWithTag("FoodSpawner");
        cuttingBoards = GameObject.FindGameObjectsWithTag("CuttingBoard");

        //if (uncutItems.Length == cutedItems.Length)
        //{
        //    for (int i = 0; i < uncutItems.Length; i++)
        //    {
        //        uncutModels[i] = uncutItems[i].transform.GetChild(0).gameObject;
        //        cutedModels[i] = cutedItems[i].transform.GetChild(0).gameObject;
        //    }
        //} 
        //else
        //{
        //    #if UNITY_EDITOR
        //        Debug.LogError("GameManager > GameObject arrays: 'uncutItems' and its equivalent 'cuttingItems' do not have the same size.");
        //    #endif
        //}
    }
}