using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowP1 : MonoBehaviour
{
    public KeyCode slowKey;
    public float slowSpeedDecrease = 15f;
    public float slowDuration = 5f;
    public float slowCooldown = 30f;

    private Movement otherPlayerMovement;
    private bool isSlowing = false;

    //ref to SlowUIP1
    private SlowUIP1 slowUI;

    private void Start()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player2");
        otherPlayerMovement = players[0].GetComponent<Movement>();

        slowUI = FindObjectOfType<SlowUIP1>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(slowKey) && !isSlowing)
        {
            StartCoroutine(SlowOpponent());
        

            slowUI.UseSlowP1();
        }
    }

    private IEnumerator SlowOpponent()
    {
        isSlowing = true;
        otherPlayerMovement.speed -= slowSpeedDecrease;

        yield return new WaitForSeconds(slowDuration);

        otherPlayerMovement.speed += slowSpeedDecrease;
        StartCoroutine(SlowCooldown());
    }

    private IEnumerator SlowCooldown()
    {
        yield return new WaitForSeconds(slowCooldown);
        isSlowing = false;
    }
}