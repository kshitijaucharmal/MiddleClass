using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowMech : MonoBehaviour
{
    public GameObject hand;
    public float throwForce = 1000f;
    private GameObject heldObject = null;
    private Rigidbody heldRb;
    private SpringJoint joint = null;
    bool canPick = false;
    bool pickedup = false;
    void throwobj()
    {
        if (joint != null)
        {
            Destroy(joint);
            joint = null;
        }
        if (heldObject != null)
        {

            heldObject.GetComponent<Rigidbody>().isKinematic = false;
            heldRb.AddForce(transform.forward * throwForce);
            Physics.IgnoreLayerCollision(3, 6, false);
            heldObject = null;
        }
        heldObject = null;
        heldRb = null;
    }
    void pick()
    {
        Physics.IgnoreLayerCollision(6, 3, true);
        heldRb = heldObject.GetComponent<Rigidbody>();
        joint = heldObject.AddComponent<SpringJoint>();
                

        heldObject.GetComponent<Rigidbody>().isKinematic = true;
        
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
        // Reset the pickable state when the object exits the trigger
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
            if (canPick){
                pick();
            }


        }
    }
}
