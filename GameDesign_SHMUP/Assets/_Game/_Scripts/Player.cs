using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Initialize the public enums
    public enum states { DEFAULT }

    // Initialize the public variables
    public states playerState;

    public float movementSpeed;
    public float aimSpeed;

    public float bulletDistance;

    public float maxHealth;

    [HideInInspector]
    public float health;

    public Transform gunTransform;
    public Transform cursorTransform;

    public GameObject bullet;

    public bool useGamepad;

    // Initialize the private variables
    float iHorizontal;
    float iVertical;

    bool iShoot;

    Rigidbody playerRigidbody;

    // Run this code once at the start
    void Start()
    {
        // Get the players rigidbody component
        playerRigidbody = GetComponent<Rigidbody>();

        health = maxHealth;
    }

    // Run this code every single frame
    void FixedUpdate()
    {
        // Switch through the different player states and run the corresponding code
        switch (playerState)
        {
            // The default playerstate
            case states.DEFAULT:
                GetInput(); // Get the user input
                Move(); // Move the player around based on the user input
                Aim(); // Aim the gun at the mouse
                Shoot(); // Fire a projectile towards the current aim
                CanDie(); // Destroy the player object when their health reaches zero
                break;
        }
    }
    
    // Get the user input
    void GetInput()
    {
        // Store the user input in variables
        if (!useGamepad)
        {
            iHorizontal = Input.GetAxis("Horizontal");
            iVertical = Input.GetAxis("Vertical");
            iShoot = Input.GetButtonDown("Shoot");
        }
        else
        {
            iHorizontal = Input.GetAxis("Horizontal G");
            iVertical = Input.GetAxis("Vertical G");
            iShoot = Input.GetButtonDown("Shoot G");
        }
    }

    // Move the player around based on the user input
    void Move()
    {
        playerRigidbody.velocity = (((transform.right * iHorizontal) * movementSpeed) + ((transform.forward * iVertical) * movementSpeed)) * Time.deltaTime;
    }

    // Aim the gun at the mouse
    void Aim()
    {
        Vector3 targetDir = cursorTransform.position - transform.position;

        float step = aimSpeed * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(gunTransform.forward, targetDir, step, 0f);

        newDir.y = 0f;

        gunTransform.rotation = Quaternion.LookRotation(newDir);
    }

    // Fire a projectile towards the current aim
    void Shoot()
    {
        if (iShoot)
        {
            var target = (GameObject)Instantiate(bullet, gunTransform.position + (gunTransform.forward * bulletDistance), gunTransform.rotation);
            target.transform.parent = null;
            target.GetComponent<Projectile>().creatorObject = this.gameObject;
        }
    }

    // Destroy the player object when their health reaches zero
    void CanDie()
    {
        if (health <= 0f)
            Destroy(this.gameObject);
    }
}
