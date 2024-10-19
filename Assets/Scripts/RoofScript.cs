using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofScript : MonoBehaviour
{
    private GameObject roofobject;

    private void Start()
    {
        roofobject = GameObject.FindGameObjectWithTag("Roof");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (roofobject)
            {
                roofobject.SetActive(false);
            }
            Debug.Log("Entered");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (roofobject)
            {
                roofobject.SetActive(true);
            }
            Debug.Log("Exited");
        }
    }

    void Update()
    {
    }
}
