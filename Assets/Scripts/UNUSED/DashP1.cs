using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashP1 : MonoBehaviour
{
    public float dashSpeedIncrease = 10f;
    public float dashDuration = 5f;
    public float dashCooldownTime = 20f;

    private Movement movementScript;

    private void Start()
    {
        movementScript = GetComponent<Movement>();

        if (movementScript == null)
        {
            Debug.LogError("Movement script not found on the GameObject or is not properly initialized.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(DashCoroutine());
    }

    private IEnumerator DashCoroutine()
    {
        // Store the current direction before starting the coroutine
        Vector2 currentDirection = movementScript.currentDirection;

        // Start the Dash coroutine
        StartCoroutine(Dash());

        // Wait for the Dash coroutine to complete
        yield return new WaitForSeconds(dashDuration);

        // After Dash is completed, set the velocity based on the stored direction
        GetComponent<Rigidbody2D>().velocity = currentDirection * movementScript.speed;

        // Start the DashCooldown coroutine
        StartCoroutine(DashCooldown());
    }

    private IEnumerator Dash()
    {
        movementScript.speed += dashSpeedIncrease;

        yield return new WaitForSeconds(dashDuration);

        movementScript.speed -= dashSpeedIncrease;
    }

    private IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(dashCooldownTime);
    }
}
