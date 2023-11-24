using System.Collections;
using UnityEngine;

public class PhaseP1 : MonoBehaviour
{
    [SerializeField] private KeyCode activateKey;
    [SerializeField] private float flashInterval = 0.5f;
    [SerializeField] private float phaseDuration = 3f;        
    [SerializeField] private float cooldownTime = 15f;        

    private Collider2D playerCollider;
    private SpriteRenderer playerRenderer;
    private bool isPhasing = false;
    private float cooldownTimer = 0f;


    //ref to PhaseUI
    private PhaseUIP1 phaseUI;


    private void Start()
    {
        playerCollider = GetComponent<Collider2D>();
        playerRenderer = GetComponent<SpriteRenderer>();
        phaseUI = FindObjectOfType<PhaseUIP1>();
    }

    private void Update()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
        else if (Input.GetKeyDown(activateKey))
        {
            StartPhase();
            phaseUI.UsePhaseP1();
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
            //disable the collider
            playerCollider.enabled = false;
            //start flashing the sprite
            InvokeRepeating("FlashSprite", 0f, flashInterval);
        }
        else
        {
            //re-enable collider
            playerCollider.enabled = true;
            //stop flashing
            CancelInvoke("FlashSprite");
            //visible sprite
            playerRenderer.enabled = true;
        }
    }

    private void FlashSprite()
    {
        playerRenderer.enabled = !playerRenderer.enabled;
    }
}
