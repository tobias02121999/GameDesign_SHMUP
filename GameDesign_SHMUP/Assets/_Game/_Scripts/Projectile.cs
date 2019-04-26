using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Initialize the public variables
    public float movementSpeed;
    public int range = -1;

    public GameObject creatorObject;

	// Run this code every single frame
	void FixedUpdate ()
    {
        Move(); // Move the projectile forwards
        ReduceRange();
    }

    // Move the projectile forwards
    void Move()
    {
        transform.position += (transform.forward * movementSpeed) * Time.deltaTime;
    }

    void ReduceRange()
    {
        if (range > 0)
            range--;

        if (range == 0)
            Destroy(this.gameObject);
    }

    // Destroy the projectile
    void OnTriggerStay(Collider other)
    {
        if (!GameObject.ReferenceEquals(other.gameObject, creatorObject))
        {
            if (other.GetComponent<Player>() != null)
                other.GetComponent<Player>().health--;

            if (other.GetComponent<EnemyBehaviour>() != null)
                other.GetComponent<EnemyBehaviour>().health--;

            Destroy(this.gameObject);
        }
    }
}
