using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseAbilityPickup : MonoBehaviour
{
    [SerializeField] private float phaseDuration = 5f;
    [SerializeField] private float flashInterval = 0.5f;

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("Player1") || otherCollider.CompareTag("Player2"))
        {
            Movement playerMovement = otherCollider.GetComponent<Movement>();

            if (playerMovement != null)
            {
                //activate phase ability.
                ActivatePhaseAbility(playerMovement);
            }

            //destroy the pickup object.
            Destroy(gameObject);
        }
    }

    private void ActivatePhaseAbility(Movement playerMovement)
    {
        //check if the phase ability is not already active.
        if (!playerMovement.IsPhasing)
        {
            playerMovement.StartPhase();
            //set a timer to end the phasing ability after the specified duration.
            playerMovement.Invoke("EndPhase", phaseDuration);

            //disable the collider and start flashing the sprite.
            playerMovement.SetPhaseState(true, flashInterval);
        }
    }
}
