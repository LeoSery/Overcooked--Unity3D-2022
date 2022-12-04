using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Timer
    [Header("Timer :")]
    public float gameDuration;

    private UIManager uiManager;

    private float currentTime = 0f;
    private int Minutes;
    private int Seconds;
    #endregion

    #region Objects references
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

    [Header("Actions :")]
    public GameObject[] cuttingBoards;
    public GameObject[] gazCoockers;
    public GameObject[] deliveryZones;

    [Header("Coocking tools :")]
    public GameObject[] fryingPans;
    public GameObject[] plates;
    #endregion

    void Awake()
    {
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        currentTime = gameDuration;
    }

    void Start()
    {
        foodSpawners = GameObject.FindGameObjectsWithTag("FoodSpawner");
        cuttingBoards = GameObject.FindGameObjectsWithTag("CuttingBoard");
        gazCoockers = GameObject.FindGameObjectsWithTag("GazCoocker");
        deliveryZones = GameObject.FindGameObjectsWithTag("DeliveryZone");
    }

    void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= 1 * Time.deltaTime;
            Minutes = Mathf.FloorToInt(currentTime / 60);
            Seconds = Mathf.FloorToInt(currentTime % 60);
            uiManager.UpdateTimerUI(Minutes, Seconds);
        }
        else
        {
            currentTime = 0;
            uiManager.UpdateTimerUI(0, 0);
        }
    }
}