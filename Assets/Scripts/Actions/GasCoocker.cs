using UnityEngine;

public class GasCoocker : MonoBehaviour
{
    public enum TypeOfObjectInCoocker
    {
        None,
        Food,
        CoockingTool
    }

    [Header("GameObjects :")]
    public GameObject currentFood;
    [HideInInspector] public GameObject foodAttachementPoint;
    [HideInInspector] public GameObject currentFoodContainer;
    public TypeOfObjectInCoocker typeOfObjectInCoocker;

    [HideInInspector] public bool foodIsBake = false;
    private bool foodWaitingToBeBake = false;
    private bool playerIsHere = false;
    private bool coockerInUse = false;

    private GameObject Player;
    private GameManager gameManager;
    private PickObject pickObject;
    private Food foodScript;
    private FryingPan currentContainerScript;

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
        if (Input.GetKeyDown(KeyCode.E) && playerIsHere && pickObject.ObjectIsGrab && !coockerInUse)
        {
            GetTheFoodToBake();
            foodWaitingToBeBake = true;
            coockerInUse = true;
        }

        if (Input.GetKey(KeyCode.R) && playerIsHere && foodWaitingToBeBake)
        {
            if (KeyPressedLongEnough(KeyCode.R, holdTime))
            {
                if (typeOfObjectInCoocker == TypeOfObjectInCoocker.CoockingTool)
                {
                    if (currentContainerScript.currentFood != null)
                    {
                        if (foodScript.canBake == Food.CanBake.Yes)
                        {
                            foodScript.currentGasCoocker = gameObject;
                            foodScript.Bake();
                        }
                        else
                        {
                            Debug.LogWarning("GazCoocker > This ingredient cannot be cooked !");
                            //TODO: Show UI message with this text.
                        }
                    }
                    else
                    {
                        Debug.LogWarning("GazCoocker > Put ingredients in your pan before you start cooking");
                        //TODO: Show UI message with this text.
                    }
                }
                else if (typeOfObjectInCoocker == TypeOfObjectInCoocker.Food)
                {
                    Debug.LogWarning("GazCoocker > Put the ingredient in a frying pan before cooking it");
                    //TODO: Show UI message with this text.
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.F) && playerIsHere && pickObject.ObjectIsGrab == false && coockerInUse)
        {
            if (typeOfObjectInCoocker == TypeOfObjectInCoocker.CoockingTool)
            {
                pickObject.objectPicked = currentFoodContainer;
                currentContainerScript = currentFoodContainer.GetComponent<FryingPan>();
                currentContainerScript.panInCoocker = false;
                currentFoodContainer = null;
                currentFood = null;
            }
            else if (typeOfObjectInCoocker == TypeOfObjectInCoocker.Food)
            {
                pickObject.objectPicked = currentFood;
                currentFood = null;
            }
            pickObject.objectPicked.transform.SetParent(pickObject.objectPickedPos.transform);
            pickObject.objectPicked.transform.position = pickObject.objectPickedPos.transform.position;
            typeOfObjectInCoocker = TypeOfObjectInCoocker.None;
            pickObject.ObjectIsGrab = true;
            coockerInUse = false;
        }
    }

    void GetTheFoodToBake()
    {
        var objectPlaced = pickObject.objectPicked;
        GetInfosOfObject(objectPlaced);

        if (typeOfObjectInCoocker == TypeOfObjectInCoocker.CoockingTool)
        {
            currentFoodContainer = pickObject.objectPicked;
            currentContainerScript = currentFoodContainer.GetComponent<FryingPan>();
            currentContainerScript.panInCoocker = true;
            currentFood = currentContainerScript.currentFood;
            currentFoodContainer.transform.SetParent(foodAttachementPoint.transform);
            currentFoodContainer.transform.position = foodAttachementPoint.transform.position;

            if (currentFood != null)
                foodScript = currentFood.GetComponent<Food>();
        }
        else if (typeOfObjectInCoocker == TypeOfObjectInCoocker.Food)
        {
            currentFood = pickObject.objectPicked;
            currentFood.transform.SetParent(foodAttachementPoint.transform);
            currentFood.transform.position = foodAttachementPoint.transform.position;
            foodScript = currentFood.GetComponent<Food>();
        }

        if (currentFoodContainer != null && currentFood != null && foodScript.currentState == Food.State.InPan)
            foodScript.currentState = Food.State.CanBeBake;

        pickObject.objectPicked = null;
        pickObject.ObjectIsGrab = false;
    }

    void GetInfosOfObject(GameObject objectPlaced)
    {
        var placedFoodScript = objectPlaced.GetComponent<Food>();
        var placedPanScript = objectPlaced.GetComponent<FryingPan>();

        if (placedFoodScript != null && placedPanScript == null)
            typeOfObjectInCoocker = TypeOfObjectInCoocker.Food;
        else if (placedFoodScript == null && placedPanScript != null)
            typeOfObjectInCoocker = TypeOfObjectInCoocker.CoockingTool;
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
            foreach (GameObject gasCoocker in gameManager.gazCoockers)
            {
                if (gasCoocker == transform.gameObject)
                {
                    playerIsHere = true;
                }
                else
                {
                    var gasCoockerScript = gasCoocker.GetComponent<GasCoocker>();
                    gasCoockerScript.playerIsHere = false;
                }
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            foreach (GameObject gasCoocker in gameManager.gazCoockers)
            {
                if (gasCoocker == transform.gameObject)
                {
                    playerIsHere = false;
                }
            }
        }
    }
}
