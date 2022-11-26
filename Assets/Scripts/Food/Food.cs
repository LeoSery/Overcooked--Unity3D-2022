using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public enum State
    {
        Uundistributed,
        CanBeCut,
        CanBeBake,
        CanBeServed,
        Served
    }

    [Header("GameObjects :")]
    public GameObject currentFood;
    public GameObject Model;

    public Mesh newFoodAppearance;
    public Material newFoodRenderer;

    [Header("Settings :")]
    public State currentState;

    public GameObject cuttedModel;
    public GameObject currentCuttingBoard;

    private GameManager gameManager;
    private int currentFoodIndex;

    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Model = transform.GetChild(0).gameObject;
    }

    void Start()
    {
        currentFoodIndex = System.Array.IndexOf(gameManager.uncutItems, currentFood);
    }

    public void Cut()
    {
        cuttedModel = gameManager.cutedModels[currentFoodIndex];
        var cuttingBoardScript = currentCuttingBoard.GetComponent<CuttingBoard>();

        if (currentState == State.CanBeCut)
        {
            DestroyImmediate(Model);
            Instantiate(cuttedModel, cuttingBoardScript.foodAttachementPoint.transform.position, Quaternion.identity, gameObject.transform);
            currentState = State.CanBeBake;
        }
    }

    public void Bake()
    {

    }
}
