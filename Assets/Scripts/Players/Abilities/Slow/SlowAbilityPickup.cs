using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SlowAbilityPickup : MonoBehaviour
{
    [SerializeField] private float slowSpeedDecrease = 10f;
    [SerializeField] private float slowDuration = 5f;

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("Player1") || otherCollider.CompareTag("Player2"))
        {
            Movement playerMovement = otherCollider.GetComponent<Movement>();

            if (playerMovement != null)
            {
                //slow down the other player.
                StartCoroutine(SlowOpponent(playerMovement));
            }
            int playerNumber = otherCollider.CompareTag("Player1") ? 1 : 2;
            SlowUIManager.Instance.UseSlow(playerNumber);

            //destroy the pickup object.
            Destroy(gameObject);
        }
    }

    private IEnumerator SlowOpponent(Movement playerMovement)
    {
        //find the other player.
        Movement[] allPlayers = FindObjectsOfType<Movement>();
        Movement otherPlayer = allPlayers.FirstOrDefault(p => p != playerMovement);

        if (otherPlayer != null && !otherPlayer.IsSlowing)
        {
            //slow down the other player.
            otherPlayer.SlowOpponent(slowSpeedDecrease, slowDuration);
        }

        yield return null; //ensure that the coroutine completes immediately.
    }
}