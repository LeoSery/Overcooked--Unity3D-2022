using UnityEngine;

public class PlateSpawner : MonoBehaviour
{
    [Header("GameObjets :")]
    public GameObject prefabToSpawn;

    private GameObject plateAttachementPoint;
    private Transform parentTransform;
    private GameManager gameManager;

    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        parentTransform = GameObject.Find("KitchenTools").transform;
        plateAttachementPoint = transform.GetChild(1).gameObject;
    }

    public void InstanciateNewPlate()
    {
        for (int i = 0; i < gameManager.plates.Length; i++)
        {
            gameManager.plates[i] = null;
        }
        GameObject newPlate = Instantiate(prefabToSpawn, plateAttachementPoint.transform.position, Quaternion.identity, parentTransform);
        gameManager.plates[0] = newPlate;
    }
}
