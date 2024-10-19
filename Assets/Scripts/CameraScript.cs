using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    private Vector3 constant = new Vector3(-15.9f,13.7f,-16.15f);
    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = player.transform.position + constant ;
    }
}
