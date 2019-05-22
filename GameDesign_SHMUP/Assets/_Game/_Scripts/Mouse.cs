using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    // Initialize the public variables
    public bool useGamepad;
    public GameObject playerObject;

    // Initialize the private variables
    Transform playerTransform;

    // Run this code once at the start
    void Start()
    {
        // Get the player transform component
        playerTransform = playerObject.transform;
    }

    // Run this code every single frame
    void Update ()
    {
        Move(); // Move the mouse to the correct position
    }

    // Move the mouse to the correct position
    void Move()
    {
        if (!useGamepad)
        {
            int layerMask = 1 << 13;


            Vector3 mouse = Input.mousePosition;
            Ray castPoint = Camera.main.ScreenPointToRay(mouse);
            RaycastHit hit;

            if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, layerMask))
                transform.position = new Vector3(hit.point.x, 0f, hit.point.z);
        }
        else
        {
            float inputHor = Input.GetAxis("Horizontal G R");
            float inputVer = Input.GetAxis("Vertical G R");
            transform.position = playerTransform.position + (inputHor * transform.right) + (inputVer * transform.forward);
        }
    }
}
