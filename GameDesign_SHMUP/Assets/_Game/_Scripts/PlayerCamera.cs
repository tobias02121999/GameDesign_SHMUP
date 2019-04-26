using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    // Intialize the public variables
    public GameObject playerObject;
    public float smoothTime;

    // Initialize the private variables
    Transform playerTransform;
    Vector3 velocity = Vector3.zero;

    // Run this code once at the start
    void Start()
    {
        // Get the players transform component
        playerTransform = playerObject.transform;
    }

    // Run this code every single frame
    void FixedUpdate()
    {
        Follow(); // Follow the player transform
    }

    // Follow the player transform
    void Follow()
    {
        Vector3 targetPos = playerTransform.TransformPoint(new Vector3(0f, -.5f, 0f));
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
    }
}
