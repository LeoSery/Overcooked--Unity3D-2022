using UnityEngine;

public class DeliveryZone : MonoBehaviour
{
    public enum TypeOfObjectInZone
    {
        None,
        Plate
    }

    public bool playerIsHere = false;

    private GameManager gameManager;

    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            foreach (GameObject deliveryZone in gameManager.deliveryZones)
            {
                if (deliveryZone == transform.gameObject)
                {
                    playerIsHere = true;
                }
                else
                {
                    var deliveryZoneScript = deliveryZone.GetComponent<DeliveryZone>();
                    deliveryZoneScript.playerIsHere = false;
                }
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            foreach (GameObject deliveryZone in gameManager.deliveryZones)
            {
                if (deliveryZone == transform.gameObject)
                {
                    playerIsHere = false;
                }
            }
        }
    }
}
