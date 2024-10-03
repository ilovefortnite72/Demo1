using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{


    public Transform PlayerPos;
    public Vector3 offset;
    public float CameraPos;
    public float CameraSpeed = 100f;

    bool bounds;
    public Vector3 MinCameraPos;
    public Vector3 MaxCameraPos;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            
    }
}
