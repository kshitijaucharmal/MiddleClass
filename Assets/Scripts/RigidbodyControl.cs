using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyControl : MonoBehaviour
{
    // Variable to store the object the player interacts with
    private Rigidbody grabbedRigidbody;

    // Trigger to make the object static
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object has a Rigidbody component
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Disable the Rigidbody and make the object static
            rb.isKinematic = true;
            rb.useGravity = false;

            // Reset the rotation to make it erect
            other.transform.rotation = Quaternion.Euler(0f, 0f, 0f);  // Erect rotation on all axes

            Debug.Log("Object Static");
        }
    }

    // When the object leaves the trigger area (optional, if needed)
    private void OnTriggerExit(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null && rb == grabbedRigidbody)
        {
            // Re-enable the Rigidbody when the player grabs it again
            rb.isKinematic = false;
            rb.useGravity = true;

            Debug.Log("Object Dynamic");
        }
    }

    //// Method to grab and release the object (can be used when the player grabs the object)
    //public void GrabObject(GameObject obj)
    //{
    //    Rigidbody rb = obj.GetComponent<Rigidbody>();
    //    if (rb != null)
    //    {
    //        grabbedRigidbody = rb;
    //        rb.isKinematic = false;  // Make it dynamic again

    //        Debug.Log("Object Dynamic");
    //    }
    //}
}
