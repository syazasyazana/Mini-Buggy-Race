using UnityEngine;
using System.Collections;

public class BuggyMovement : MonoBehaviour
{
    public FixedJoystick joystick; // Joystick input from Canvas
    public float maxSpeed = 10f;   // Maximum speed of the buggy (forward & backward at the same speed)
    public float turnSpeed = 50f;  // Turning speed

    public AudioSource engineSound;  // AudioSource component to control the engine sound
    public float speedThreshold = 0.1f;  // Minimum movement speed to trigger sound

    public float conePushBack = 2f;   // Distance pushed back when hitting a cone
    public float rockStopTime = 2f;   // Time to stop when hitting a rock
    public float mudSlowDown = 0.5f;  // Speed reduction in mud
    public float slowDuration = 2f;   // Duration of slow effect

    private bool isSlowed = false;
    private bool isStopped = false;

    private void Update()
    {
        if (isStopped) return; // Stop movement when hitting a rock

        // Get joystick input
        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;

        // Adjust speed if slowed down
        float currentSpeed = isSlowed ? maxSpeed * mudSlowDown : maxSpeed;

        // Forward & backward movement (no brakes)
        Vector3 forwardMovement = transform.forward * vertical * currentSpeed * Time.deltaTime;

        // Turning left & right
        float turn = horizontal * turnSpeed * Time.deltaTime;
        transform.Rotate(0, turn, 0);

        // Move the buggy
        transform.position += forwardMovement;

        // Play sound when the car is moving, stop when it's stationary
        HandleEngineSound(vertical, horizontal);
    }

    // Handle engine sound based on movement
    void HandleEngineSound(float verticalInput, float horizontalInput)
    {
        float movementSpeed = Mathf.Abs(verticalInput) + Mathf.Abs(horizontalInput);

        if (movementSpeed > speedThreshold && !engineSound.isPlaying)
        {
            engineSound.Play();
        }
        else if (movementSpeed <= speedThreshold && engineSound.isPlaying)
        {
            engineSound.Stop();
        }
    }

    // Detect collision with obstacles (Cone & Rock)
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Cone")) // Hit cone → push back a little
        {
            transform.position -= transform.forward * conePushBack;
        }
        else if (collision.gameObject.CompareTag("Rock")) // Hit rock → stop for a while
        {
            StartCoroutine(StopBuggy());
        }
    }

    // Detect trigger collision (Mud)
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Mud")) // Enter mud → slow down
        {
            if (!isSlowed)
            {
                StartCoroutine(SlowDown());
            }
        }
    }

    IEnumerator StopBuggy()
    {
        isStopped = true;
        yield return new WaitForSeconds(rockStopTime);
        isStopped = false;
    }

    IEnumerator SlowDown()
    {
        isSlowed = true;
        yield return new WaitForSeconds(slowDuration);
        isSlowed = false;
    }
}

