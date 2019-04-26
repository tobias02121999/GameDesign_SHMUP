using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
	// Update is called once per frame
	void Update ()
    {
        Move(); // Move the mouse to the correct position
    }

    // Move the mouse to the correct position
    void Move()
    {
        Vector3 mouse = Input.mousePosition;
        Ray castPoint = Camera.main.ScreenPointToRay(mouse);
        RaycastHit hit;

        if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
            transform.position = new Vector3(hit.point.x, 0f, hit.point.z);
    }
}
