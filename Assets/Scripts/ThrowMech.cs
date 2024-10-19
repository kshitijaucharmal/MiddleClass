using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowMech : MonoBehaviour
{
    [SerializeField] private Transform hand;
    public float throwForce = 70f;
    public float upWardThrowForce = 0f;
    private GameObject heldObject = null;
    private Rigidbody heldRb;
    [SerializeField] private Transform pickPoint;
    bool canPick = false;
    bool pickedup = false;

    private Vector3 forceToAdd;

    void throwobj()
    {
        Physics.IgnoreLayerCollision(3, 6, false);

        heldObject.transform.SetParent(null);
        forceToAdd = hand.forward * throwForce;
        heldRb.isKinematic = false;
        heldRb.useGravity = true;
        heldRb.AddForce(forceToAdd, ForceMode.Impulse);
        heldObject = null;
        pickedup = false;
    }
    void pick()
    {
        if (!canPick) return;
        Physics.IgnoreLayerCollision(3, 6, true);
        heldRb = heldObject.GetComponent<Rigidbody>();
        heldObject.transform.SetParent(transform);
        heldObject.transform.localPosition = pickPoint.localPosition;
        heldRb.isKinematic = true;
        heldRb.useGravity = false;
        pickedup = true;
        print("Picked up");
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Throwable"))
        {
            canPick = true;
            heldObject = other.gameObject;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Throwable"))
        {
            canPick = false; // Reset pickable flag
            // heldObject = null; // Clear held object
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Grab"))
        {
            if (pickedup)
            {
                throwobj();
            }
            if (canPick)
            {
                pick();
            }
        }
    }
}
