using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowMech : MonoBehaviour
{
    public GameObject hand;
    public float throwForce = 1000f;
    public float upWardThrowForce = 1000f;
    private GameObject heldObject = null;
    private Rigidbody heldRb;
    private CharacterJoint joint = null;
    bool canPick = false;
    bool pickedup = false;

    void throwobj()
    {
        Physics.IgnoreLayerCollision(3, 6, false);

        Destroy(joint);
       



        Vector3 forceToAdd = gameObject.transform.up * upWardThrowForce + hand.transform.forward * throwForce;
        heldRb.AddForce(forceToAdd, ForceMode.Force);


        heldObject = null;
        heldRb = null;
        pickedup = false;
    }
    void pick()
    {
        Physics.IgnoreLayerCollision(3, 6, true);
        heldRb = heldObject.GetComponent<Rigidbody>();
        heldObject.transform.position = hand.transform.position + new Vector3(0, -0.5f, 0);
        joint = hand.AddComponent<CharacterJoint>();
        joint.connectedBody = heldRb;
        joint.anchor = hand.transform.localPosition;
        joint.projectionDistance = 0.5f;
        joint.projectionAngle = 90;
        joint.swingAxis = new Vector3(1, 1, 1);
        print("Physics Disabled");
        pickedup = true;
        print("Picked up");
    }
    void OnTriggerEnter(Collider other)
    {
        if (heldObject == null && other.gameObject.CompareTag("Throwable"))
        {
            canPick = true;
            heldObject = other.gameObject;
            print("Trigered");

        }
    }
    void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Throwable"))
        {
            canPick = false; // Reset pickable flag
            heldObject = null; // Clear held object
            print("Exit");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
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
