using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Shake : MonoBehaviour
{
    public float duration = 1f;
    public bool start = false;
    public AnimationCurve curve;

    private void Update()
    {
        if (start)
        {
            start = false;
            StartCoroutine(Shaking());
        }
    }

    IEnumerator Shaking()
    {
        Vector2 startPosition = transform.localPosition; // Use localPosition for local space
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime / duration);

            // Multiply the random offset by the strength
            Vector2 offset = Random.insideUnitCircle * strength;

            // Apply the offset to the local position
            transform.localPosition = startPosition + offset;

            yield return null;
        }

        // Reset to the original position
        transform.localPosition = startPosition;
    }
}