using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    // Initialize the public enums
    public enum states { IDLE, WANDER, CHASE, DASH }

    // Initialize the public variables
    public states enemyState;

    public float wanderSpeed;
    public float chaseSpeed;
    public float dashSpeed;

    public float maxHealth;

    [HideInInspector]
    public float health;

    public float chaseTurnSpeed;

    public float meleeDistance;

    public int meleeDuration;

    public int idleDuration;
    public int wanderDuration;
    public int dashDuration;

    public int dashCooldown;

    public GameObject playerObject;

    [HideInInspector]
    public Transform playerTransform;

    public Transform chaseRangeTransform;
    public Transform dashRangeTransform;

    public GameObject bullet;

    [HideInInspector]
    public Rigidbody enemyRigidbody;

    // Initialize the private variables
    int alarm;
    int dashAlarm;

    // Run this code once at the start
    void Start()
    {
        // Get the player transform component
        playerTransform = playerObject.transform;

        // Get the enemy rigidbody component
        enemyRigidbody = GetComponent<Rigidbody>();

        // Reset the health variable to equal the max health
        health = maxHealth;
    }

    // Run the dash cooldown
    public void DashCooldown()
    {
        dashAlarm--;
    }

    // Stay in the idle state for a defined time, before switching into the wander state
    public void IdleAlarm()
    {
        if (alarm >= 0)
            alarm--;
        else
        {
            alarm = wanderDuration;

            float rotY = Random.Range(0f, 360f);
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, rotY, transform.eulerAngles.z);

            enemyState = states.WANDER;
        }
    }

    // Stay in the wander state for a defined time, before switching into the idle state
    public void WanderAlarm()
    {
        if (alarm >= 0)
        {
            Move(wanderSpeed);
            alarm--;
        }
        else
        {
            alarm = idleDuration;
            enemyState = states.IDLE;
        }
    }

    // Wait for a defined time, before activating the dash and going back into the chase state
    public void DashAlarm()
    {
        if (alarm >= 0)
            alarm--;
        else
        {
            Dash(); // Dash forward
            MeleeAttack(); // Instantiate a projectile damage collider
            enemyState = states.CHASE;
        }
    }

    // Check if the player is in range, and if so switch over to the chase state
    public void CheckForChase()
    {
        float dist = Vector3.Distance(transform.position, playerTransform.position);

        if (dist <= (chaseRangeTransform.localScale.x / 2f))
            enemyState = states.CHASE;
    }

    // Check if the player is in range, and if so switch over to the dash state
    public void CheckForDash()
    {
        float dist = Vector3.Distance(transform.position, playerTransform.position);

        if (dist <= (dashRangeTransform.localScale.x / 2f) && dashAlarm <= 0)
        {
            dashAlarm = dashCooldown;
            alarm = dashDuration;
            enemyState = states.DASH;
        }
    }

    // Rotate towards the player object
    public void LookAt(Transform target)
    {
        Vector3 targetDir = playerTransform.position - transform.position;

        float step = chaseTurnSpeed * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0f);

        newDir.y = 0f;

        transform.rotation = Quaternion.LookRotation(newDir);
    }

    // Instantiate a projectile damage collider
    public void MeleeAttack()
    {
        var target = (GameObject)Instantiate(bullet, transform.position + (transform.forward * meleeDistance), transform.rotation);
        target.transform.parent = transform;
        target.GetComponent<Projectile>().creatorObject = this.gameObject;
        target.GetComponent<Projectile>().movementSpeed = 0f;
        target.GetComponent<Projectile>().range = meleeDuration;
    }

    // Dash forward
    public void Dash()
    {
        enemyRigidbody.AddForce((transform.forward * dashSpeed) * Time.deltaTime, ForceMode.Impulse);
    }

    // Move forward
    public void Move(float speed)
    {
        transform.position += (transform.forward * speed) * Time.deltaTime;
    }

    // Check if the health has reached zero or below, and if so destroy the enemy object
    public void CanDie()
    {
        if (health <= 0f)
            Destroy(this.gameObject);
    }
}
