using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{

    private Transform _camera;

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(-_camera.position);
    }
}
