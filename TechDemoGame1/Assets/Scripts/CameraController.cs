using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform player;
    private Camera cam;
    


    private void Start()
    {
        cam = GetComponent<Camera>();
        

    }

    //check if player is out of camera view and move camera to player position with the player being at centre of the camera
    private void LateUpdate()
    {
        Vector3 playerViewportPosition = cam.WorldToViewportPoint(player.position);
        if (playerViewportPosition.x <0 || playerViewportPosition.x > 1 || playerViewportPosition.y < 0 || playerViewportPosition.y > 1)
        {
            Vector3 newCameraPosition = new Vector3(player.position.x, player.position.y, transform.position.z);
            transform.position = newCameraPosition;
        }
    }



}
