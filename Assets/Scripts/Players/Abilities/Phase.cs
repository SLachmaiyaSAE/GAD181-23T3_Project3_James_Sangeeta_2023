using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase : MonoBehaviour
{
    public KeyCode activate; 
    public float flashInterval = 0.2f;
    public float phaseDuration = 3f;        
    public float cooldownTime = 5f;


    private Collider2D playerCollider;
    private SpriteRenderer playerRenderer;
    private bool isPhasing = false;
    private float cooldownTimer = 0f;

    private void Start()
    {
        playerCollider = GetComponent<Collider2D>();
        playerRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
        else if (Input.GetKeyDown(activate))
        {
            StartPhase();
        }
    }

    private void StartPhase()
    {
        if (cooldownTimer <= 0)
        {
            isPhasing = true;
            SetPhaseState(isPhasing);

            //set a timer to end the phasing ability after the specified duration
            Invoke("EndPhase", phaseDuration);
            cooldownTimer = cooldownTime;
        }
    }

    private void EndPhase()
    {
        isPhasing = false;
        SetPhaseState(isPhasing);
    }

    private void SetPhaseState(bool isPhasing)
    {
        if (isPhasing)
        {
            playerCollider.enabled = false;
            InvokeRepeating("FlashSprite", 0f, flashInterval);
        }
        else
        {
           
            playerCollider.enabled = true;
            CancelInvoke("FlashSprite");
            playerRenderer.enabled = true;
        }
    }

    private void FlashSprite()
    {
        //toggle the visibility of the sprite
        playerRenderer.enabled = !playerRenderer.enabled;
    }

}
