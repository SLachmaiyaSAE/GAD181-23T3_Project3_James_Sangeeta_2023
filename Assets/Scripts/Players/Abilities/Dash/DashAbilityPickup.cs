using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DashAbilityPickup : MonoBehaviour
{
    public float speedMultiplier = 1.5f;
    public float duration = 5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            Movement playerMovement = other.GetComponent<Movement>();

            if (playerMovement != null)
            {
                playerMovement.ActivateDashAbility(duration, speedMultiplier);
            }


            // Inform DashUIManager about the dash pickup
            int playerNumber = other.CompareTag("Player1") ? 1 : 2;
            DashUIManager.Instance.UseDash(playerNumber);

            // Destroy the pickup object.
            Destroy(gameObject);
        }
    }
}

