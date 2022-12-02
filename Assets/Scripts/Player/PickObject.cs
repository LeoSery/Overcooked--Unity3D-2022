using UnityEngine;

public class PickObject : MonoBehaviour
{
    public GameObject objectPicked;
    public GameObject objectPickedPos;
    public float detectionDistance;

    public float heightRayHead;
    public float heightRayBody;
    public float heightRayFoot;

    [HideInInspector] public bool ObjectIsGrab = false;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (ObjectIsGrab)
                DropObject();
            else
                PickUpObject();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!ObjectIsGrab)
        {
            if (other.CompareTag("Pickable"))
            {
                objectPicked = other.gameObject;
            }
        }
    }

    public void PickUpObject()
    {
        if (objectPicked != null)
        {
            objectPicked.transform.SetParent(objectPickedPos.transform);
            objectPicked.transform.position = objectPickedPos.transform.position;
            ObjectIsGrab = true;
        }
        else
            Debug.LogWarning("PickObject > No object to catch in front of you, try to get closer to an object.");
    }

    public void DropObject()
    {
        objectPicked.transform.SetParent(null);
        objectPicked = null;
        ObjectIsGrab = false;
    }
}
