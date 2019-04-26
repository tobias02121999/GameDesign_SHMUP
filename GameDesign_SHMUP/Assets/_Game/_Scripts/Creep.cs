using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creep : MonoBehaviour
{
    // Intialize the private variables
    EnemyBehaviour behaviour;

    // Rum this code once at the start
    void Start()
    {
        behaviour = GetComponent<EnemyBehaviour>();
    }

    // Run this code every single frame
    void Update()
    {
        RunState(); // Run the current enemy state
    }

    // Run the current enemy state
    void RunState()
    {
        switch (behaviour.enemyState)
        {
            // The idle enemy state
            case EnemyBehaviour.states.IDLE:
                behaviour.GetTarget(); // Find the nearest player target transform
                behaviour.IdleAlarm(); // Stay in the idle state for a defined time, before switching into the wander state
                behaviour.CheckForChase(); // Check if the player is in range, and if so switch over to the chase state
                behaviour.DashCooldown(); // Run the dash cooldown
                behaviour.CanDie(); // Check if the health has reached zero or below, and if so destroy the enemy object
                break;

            // The wander enemy state
            case EnemyBehaviour.states.WANDER:
                behaviour.GetTarget(); // Find the nearest player target transform
                behaviour.WanderAlarm(); // Stay in the wander state for a defined time, before switching into the idle state
                behaviour.CheckForChase(); // Check if the player is in range, and if so switch over to the chase state
                behaviour.DashCooldown(); // Run the dash cooldown
                behaviour.CanDie(); // Check if the health has reached zero or below, and if so destroy the enemy object
                break;

            // The chase enemy state
            case EnemyBehaviour.states.CHASE:
                behaviour.GetTarget(); // Find the nearest player target transform
                behaviour.LookAt(behaviour.targetTransform); // Rotate towards the player object
                behaviour.Move(behaviour.chaseSpeed); // Move forward
                behaviour.CheckForDash(); // Check if the player is in range, and if so switch over to the dash state
                behaviour.DashCooldown(); // Run the dash cooldown
                behaviour.CanDie(); // Check if the health has reached zero or below, and if so destroy the enemy object
                break;

            // The dash enemy state
            case EnemyBehaviour.states.DASH:
                behaviour.GetTarget(); // Find the nearest player target transform
                behaviour.LookAt(behaviour.targetTransform); // Rotate towards the player object
                behaviour.DashAlarm(); // Wait for a defined time, before activating the dash and going back into the chase state
                behaviour.CanDie(); // Check if the health has reached zero or below, and if so destroy the enemy object
                break;
        }
    }
}
