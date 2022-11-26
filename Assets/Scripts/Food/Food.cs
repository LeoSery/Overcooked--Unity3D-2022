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

    public Mesh newFoodAppearance;
    public Material newFoodRenderer;

    [Header("Settings :")]
    public State currentState;

    public GameObject cuttedObject;
    public GameObject currentCuttingBoard;

    private GameManager gameManager;
    private int currentFoodIndex;

    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Start()
    {
        currentFoodIndex = System.Array.IndexOf(gameManager.uncutItems, currentFood);
    }

    public void Cut()
    {
        cuttedObject = gameManager.cutedItems[currentFoodIndex];

        if (cuttedObject.transform.childCount == 0)
        {
            newFoodAppearance = cuttedObject.GetComponent<MeshFilter>().sharedMesh;
            gameObject.GetComponent<MeshFilter>().sharedMesh = newFoodAppearance;
            newFoodRenderer = cuttedObject.GetComponent<MeshRenderer>().sharedMaterial;
            gameObject.GetComponent<MeshRenderer>().sharedMaterial = newFoodRenderer;
            gameObject.transform.localScale = cuttedObject.transform.localScale;
        }
        else
        {
            var cuttingBoardScript = currentCuttingBoard.GetComponent<CuttingBoard>();
            Instantiate(cuttedObject, cuttingBoardScript.foodAttachementPoint.transform.position, Quaternion.identity, currentCuttingBoard.transform);
        }
        currentState = State.CanBeBake;
    }

    public void Bake()
    {

    }
}
