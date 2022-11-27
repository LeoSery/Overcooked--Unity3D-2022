using UnityEngine;

public class Food : MonoBehaviour
{
    public enum State
    {
        Uundistributed,
        CanBeCut,
        CanBePutInPan,
        InPan,
        CanBeBake,
        Baked,
        CanBePutInPlate,
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
    public bool foodInPan = false;

    public GameObject cuttedModel;
    public GameObject coockedModel;
    public GameObject currentCuttingBoard;
    public GameObject currentGasCoocker;

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
            cuttingBoardScript.foodIsCut = true;
            currentState = State.CanBePutInPan;
        }
    }

    //public void Bake()
    //{
    //    coockedModel = gameManager.coockedItems[currentFoodIndex];
    //    var gazCoockerScirpt = currentGasCoocker.GetComponent<GasCoocker>();

    //    if (currentState == State.CanBeBake)
    //    {
    //        DestroyImmediate(Model);
    //        Instantiate(coockedModel, gazCoockerScirpt.foodAttachementPoint.transform.position, Quaternion.identity, gameObject.transform);
    //        gazCoockerScirpt.foodIsBake = true;
    //        currentState = State.CanBeServed;
    //    }
    //}
}
