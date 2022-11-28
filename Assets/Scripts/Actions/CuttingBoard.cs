using UnityEngine;

public class CuttingBoard : MonoBehaviour
{
    public enum TypeOfObjectInCutting
    {
        None,
        Food,
        CoockingTool
    }

    [Header("GameObjects :")]
    public TypeOfObjectInCutting typeOfObjectInCutting;
    public GameObject currentFood;

    [HideInInspector] public GameObject foodAttachementPoint;
    [HideInInspector] public bool foodIsCut = false;


    private bool foodWaitingToBeCut = false;
    private bool playerIsHere = false;
    private bool boardInUse = false;

    private GameManager gameManager;
    private PickObject pickObject;
    private GameObject Player;
    private Food foodScript;

    private float startTime = 0f;
    private float holdTime = 3.0f;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Player = GameObject.FindGameObjectWithTag("Player");
        pickObject = Player.GetComponent<PickObject>();
        foodAttachementPoint = transform.GetChild(2).gameObject;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIsHere && pickObject.ObjectIsGrab && !boardInUse)
        {
            GetTheFoodToCut();
            foodWaitingToBeCut = true;
            boardInUse = true;
        }

        if (Input.GetKey(KeyCode.R) && playerIsHere && foodWaitingToBeCut)
        {
            if (KeyPressedLongEnough(KeyCode.R, holdTime))
            {
                if (typeOfObjectInCutting == TypeOfObjectInCutting.Food)
                {
                    if (foodScript.canCut == Food.CanCut.Yes)
                    {
                        foodScript.currentCuttingBoard = gameObject;
                        foodScript.Cut();
                    } 
                    else
                    {
                        Debug.LogWarning("This ingredient cannot be cut !");
                        //TODO: Show UI message with this text.
                    }
                }
                else
                {
                    Debug.LogWarning("A kitchen utensil cannot be cut !");
                    //TODO: Show UI message with this text. 
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.F) && playerIsHere && pickObject.ObjectIsGrab == false && boardInUse)
        {
            pickObject.objectPicked = currentFood;
            currentFood.transform.SetParent(pickObject.objectPickedPos.transform);
            pickObject.objectPicked.transform.position = pickObject.objectPickedPos.transform.position;
            typeOfObjectInCutting = TypeOfObjectInCutting.None;
            pickObject.ObjectIsGrab = true;
            currentFood = null;
            boardInUse = false;
        }
    }

    void GetTheFoodToCut()
    {
        var objectPlaced = pickObject.objectPicked;
        GetInfosOfObject(objectPlaced);

        currentFood = pickObject.objectPicked;
        currentFood.transform.SetParent(foodAttachementPoint.transform);
        currentFood.transform.position = foodAttachementPoint.transform.position;
        foodScript = currentFood.GetComponent<Food>();

        pickObject.objectPicked = null;
        pickObject.ObjectIsGrab = false;
    }

    void GetInfosOfObject(GameObject objectPlaced)
    {
        var PlacedfoodScript = objectPlaced.GetComponent<Food>();
        var PlacedPanScript = objectPlaced.GetComponent<FryingPan>();

        if (PlacedfoodScript != null && PlacedPanScript == null)
            typeOfObjectInCutting = TypeOfObjectInCutting.Food;
        else if (PlacedfoodScript == null && PlacedPanScript != null)
            typeOfObjectInCutting = TypeOfObjectInCutting.CoockingTool;
    }

    bool KeyPressedLongEnough(KeyCode key, float holdTime)
    {
        if (Input.GetKeyDown(key))
            startTime = Time.time;

        if (Input.GetKey(key))
            if (startTime + holdTime <= Time.time)
                return true;
            else
                return false;
        else
            return false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            foreach (GameObject cuttingBoard in gameManager.cuttingBoards)
            {
                if (cuttingBoard == transform.gameObject)
                {
                    playerIsHere = true;
                }
                else
                {
                    var cuttingBoardScript = cuttingBoard.GetComponent<CuttingBoard>();
                    cuttingBoardScript.playerIsHere = false;
                }
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            foreach (GameObject cuttingBoard in gameManager.cuttingBoards)
            {
                if (cuttingBoard == transform.gameObject)
                {
                    playerIsHere = false;
                }
            }
        }
    }
}
