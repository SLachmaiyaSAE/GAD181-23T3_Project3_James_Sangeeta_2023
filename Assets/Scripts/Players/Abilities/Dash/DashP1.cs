using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashP1 : MonoBehaviour
{
    public KeyCode dashKey;
    public float dashSpeedIncrease = 10f;
    public float dashDuration = 5f;
    public float dashCooldownTime = 20f;

    private Movement movementScript;
    private bool isDashing = false;

    //ref to DashUIP1
    private DashUIP1 dashUI;

    private void Start()
    {
        movementScript = GetComponent<Movement>();

        dashUI = FindObjectOfType<DashUIP1>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(dashKey) && !isDashing)
        {
            StartCoroutine(Dash());
            GetComponent<Rigidbody2D>().velocity = movementScript.currentDirection * movementScript.speed;
            dashUI.UseDashP1();
        }
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        movementScript.speed += dashSpeedIncrease;

        yield return new WaitForSeconds(dashDuration);

        movementScript.speed -= dashSpeedIncrease;
        StartCoroutine(DashCooldown());
    }

    private IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(dashCooldownTime);
        isDashing = false;
    }
}
