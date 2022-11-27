using UnityEngine;

public class FryingPan : MonoBehaviour
{
    [Header("GameObjects :")]
    public GameObject currentFood;

    [Header("Infos :")]
    public bool playerIsHere = false;
    public bool panIsFull = false;

    private GameManager gameManager;
    private PickObject pickObject;
    private Food foodScript;

    private GameObject foodLocation;
    private GameObject Player;

    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Player = GameObject.FindGameObjectWithTag("Player");
        pickObject = Player.GetComponent<PickObject>();
        foodLocation = transform.GetChild(1).gameObject;
    }

    void Update()
    {
        if (playerIsHere)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (pickObject.ObjectIsGrab && !panIsFull)
                {
                    PutContentInPan();
                }
            } 
            else if (Input.GetKeyDown(KeyCode.F))
            {
                if (!pickObject.ObjectIsGrab && panIsFull)
                {
                    TakeContentOfPan();
                }
            }
        }
    }

    void PutContentInPan()
    {
        currentFood = pickObject.objectPicked;
        foodScript = currentFood.GetComponent<Food>();

        if (foodScript.currentState == Food.State.CanBePutInPan)
        {
            currentFood.transform.SetParent(foodLocation.transform);
            currentFood.transform.position = foodLocation.transform.position;

            foodScript.foodInPan = true;
            var currentFoodModel = currentFood.transform.GetChild(0).gameObject;
            var currentCollider = currentFoodModel.GetComponent<Collider>();
            currentCollider.enabled = false;

            pickObject.objectPicked = null;
            pickObject.ObjectIsGrab = false;
            foodScript.currentState = Food.State.InPan;
            panIsFull = true;
        }
        else
        {
            currentFood = null;
            foodScript = null;
        }
    }

    void TakeContentOfPan()
    {
        pickObject.objectPicked = currentFood;
        pickObject.objectPicked.transform.SetParent(pickObject.objectPickedPos.transform);
        pickObject.objectPicked.transform.position = pickObject.objectPickedPos.transform.position;
        foodScript = currentFood.GetComponent<Food>();

        foodScript.foodInPan = false;
        var currentFoodModel = currentFood.transform.GetChild(0).gameObject;
        var currentCollider = currentFoodModel.GetComponent<Collider>();
        currentCollider.enabled = true;

        pickObject.ObjectIsGrab = true;
        foodScript.foodInPan = false;

        if (foodScript.currentState == Food.State.Baked)
            foodScript.currentState = Food.State.CanBePutInPlate;
        else
            foodScript.currentState = Food.State.CanBePutInPan;

        panIsFull = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            foreach (GameObject Pan in gameManager.fryingPans)
            {
                Debug.LogWarning("Player is near : " + gameObject.name);
                if (Pan == transform.gameObject)
                {
                    playerIsHere = true;
                }
                else
                {
                    var fryingPanScript = Pan.GetComponent<FryingPan>();
                    fryingPanScript.playerIsHere = false;
                }
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.LogWarning("Player is near : " + gameObject.name);
            foreach (GameObject Pan in gameManager.fryingPans)
            {
                if (Pan == transform.gameObject)
                {
                    playerIsHere = false;
                }
            }
        }
    }
}
