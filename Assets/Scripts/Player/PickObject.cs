using System.Collections;
using System.Collections.Generic;
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
        IsNearPickableObject();
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (ObjectIsGrab)
                DropObject();
            else
                PickUpObject();
        }
    }

    // Pick Object version collider
    //public void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Pickable"))
    //    {
    //        objectPicked = other.gameObject;
    //    }
    //}

    // Pick Object version Raycast
    public void IsNearPickableObject()
    {
        RaycastHit hitHead;
        RaycastHit hitBody;
        RaycastHit hitFoot;

        Vector3 RayHead = new Vector3(transform.position.x, transform.position.y + heightRayHead, transform.position.z);
        Debug.DrawRay(RayHead, transform.TransformDirection(Vector3.forward), Color.red, 2f);

        Vector3 RayBody = new Vector3(transform.position.x, transform.position.y + heightRayBody, transform.position.z);
        Debug.DrawRay(RayBody, transform.TransformDirection(Vector3.forward), Color.green, 2f);

        Vector3 RayFoot = new Vector3(transform.position.x, transform.position.y + heightRayFoot, transform.position.z);
        Debug.DrawRay(RayFoot, transform.TransformDirection(Vector3.forward), Color.blue, 2f);

        if (!ObjectIsGrab)
        {
            if (Physics.Raycast(RayHead, transform.TransformDirection(Vector3.forward), out hitHead, detectionDistance))
            {
                Debug.DrawRay(RayHead, transform.TransformDirection(Vector3.forward) * hitHead.distance, Color.red, 2f);
                if (hitHead.collider != null)
                {
                    objectPicked = hitHead.transform.parent.gameObject;
                }
            }
            else if (Physics.Raycast(RayBody, transform.TransformDirection(Vector3.forward), out hitBody, detectionDistance, LayerMask.GetMask("Pickable")))
            {
                Debug.DrawRay(RayBody, transform.TransformDirection(Vector3.forward) * hitBody.distance, Color.green, 2f);
                if (hitBody.collider != null)
                {
                    objectPicked = hitBody.transform.parent.gameObject;
                }
            }
            else if (Physics.Raycast(RayFoot, transform.TransformDirection(Vector3.forward), out hitFoot, detectionDistance, LayerMask.GetMask("Pickable")))
            {
                Debug.DrawRay(RayFoot, transform.TransformDirection(Vector3.forward) * hitFoot.distance, Color.blue, 2f);
                if (hitFoot.collider != null)
                {
                    objectPicked = hitFoot.transform.parent.gameObject;
                }
            }
            else if (hitHead.collider == null && hitBody.collider == null && hitBody.collider == null)
            {
                objectPicked = null;
            }
        }
    }

    public void PickUpObject()
    {
        objectPicked.transform.SetParent(objectPickedPos.transform);
        objectPicked.transform.position = objectPickedPos.transform.position;
        ObjectIsGrab = true;
    }

    public void DropObject()
    {
        objectPicked.transform.SetParent(null);
        objectPicked = null;
        ObjectIsGrab = false;
    }
}
