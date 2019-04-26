using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    // Intialize the public variables
    public Transform[] playerTransform;
    public Transform cameraTransform;
    public float smoothTime;
    public float zoomAmplifier;

    // Initialize the private variables
    float cameraStartPosY;

    // Initialize the private variables
    Vector3 velocity = Vector3.zero;

    // Run this code once at the start
    void Start()
    {
        // Get the starting Y position of the camera
        cameraStartPosY = cameraTransform.position.y;
    }

    // Run this code every single frame
    void FixedUpdate()
    {
        Follow(); // Follow the player transform
        Zoom(); // Zoom the camera in and out based on the distance between the players
    }

    // Follow the player transform
    void Follow()
    {
        Vector3 targetPos = playerTransform[0].position + (playerTransform[1].position - playerTransform[0].position) / 2;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
    }

    // Zoom the camera in and out based on the distance between the players
    void Zoom()
    {
        float dist = Vector3.Distance(playerTransform[0].position, playerTransform[1].position);
        cameraTransform.position = new Vector3(cameraTransform.position.x, cameraStartPosY + (dist * zoomAmplifier), cameraTransform.position.z);
    }
}
