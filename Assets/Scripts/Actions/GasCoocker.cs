using UnityEngine;

public class GasCoocker : MonoBehaviour
{
    //[Header("GameObjects :")]
    //public GameObject currentFood;

    //[HideInInspector] public bool foodIsBake = false;
    //private bool foodWaitingToBeBake = false;
    //private bool playerIsHere = false;
    //private bool CoockerInUse = false;

    //private float startTime = 0f;
    //private float holdTime = 3.0f;

    //[HideInInspector] public GameObject foodAttachementPoint;
    //private GameManager gameManager;
    //private PickObject pickObject;
    //private GameObject Player;
    //private Food foodScript;

    //void Awake()
    //{
    //    gameManager = GetComponent<GameManager>();
    //    Player = GameObject.FindGameObjectWithTag("Player");
    //    pickObject = Player.GetComponent<PickObject>();
    //    foodAttachementPoint = transform.GetChild(2).gameObject;
    //}

    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.F) && playerIsHere && pickObject.ObjectIsGrab)
    //    {
    //        GetTheFoodToBake();
    //        foodWaitingToBeBake = true;
    //    }

    //    if (Input.GetKey(KeyCode.F) && playerIsHere && foodWaitingToBeBake)
    //    {
    //        if (KeyPressedLongEnough(KeyCode.F, holdTime))
    //        {
    //            var foodScript = currentFood.GetComponent<Food>();
    //            foodScript.currentGasCoocker = gameObject;
    //            foodScript.Bake();
    //        }
    //    }
    //}

    //void GetTheFoodToBake()
    //{
    //    currentFood = pickObject.objectPicked;
    //    currentFood.transform.SetParent(foodAttachementPoint.transform);
    //    currentFood.transform.position = foodAttachementPoint.transform.position;
    //    foodScript = currentFood.GetComponent<Food>();

    //    pickObject.objectPicked = null;
    //    pickObject.ObjectIsGrab = false;
    //}

    //bool KeyPressedLongEnough(KeyCode key, float holdTime)
    //{
    //    if (Input.GetKeyDown(key))
    //        startTime = Time.time;

    //    if (Input.GetKey(key))
    //        if (startTime + holdTime <= Time.time)
    //            return true;
    //        else
    //            return false;
    //    else
    //        return false;
    //}

    //public void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        Debug.LogWarning("Player is here");
    //        foreach (GameObject gazCoocker in gameManager.gazCoockers)
    //        {
    //            if (gazCoocker == transform.gameObject)
    //            {
    //                playerIsHere = true;
    //            }
    //            else
    //            {
    //                var gazCoockerScript = gazCoocker.GetComponent<GasCoocker>();
    //                gazCoockerScript.playerIsHere = false;
    //            }
    //        }
    //    }
    //}

    //public void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        foreach (GameObject gazCoocker in gameManager.gazCoockers)
    //        {
    //            if (gazCoocker == transform.gameObject)
    //            {
    //                playerIsHere = false;
    //            }
    //        }
    //    }
    //}
}
