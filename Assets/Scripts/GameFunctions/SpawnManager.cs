using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] spawnPoints;
    public GameObject[] spawnObjects;

    public float abilityLifetime = 10f;
    public float flashStartTime = 5f;
    public float flashInterval = 0.5f;

    void Start()
    {
        StartCoroutine(SpawnObjects());
    }

    IEnumerator SpawnObjects()
    {
        while (true)
        {
            //choose a random spawn point.
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[spawnPointIndex].transform;

            //choose a random ability to spawn.
            int objectIndex = Random.Range(0, spawnObjects.Length);
            GameObject objectToSpawn = spawnObjects[objectIndex];

            //instantiate object at the spawn point.
            GameObject spawnedObject = Instantiate(objectToSpawn, spawnPoint.position, Quaternion.identity);

            //start the countdown and flashing coroutine.
            StartCoroutine(CountdownAndDestroy(spawnedObject));

            //delay between spawns.
            yield return new WaitForSeconds(8f);
        }
    }

    IEnumerator CountdownAndDestroy(GameObject spawnedObject)
    {
        float countdown = abilityLifetime;
        SpriteRenderer spriteRenderer = spawnedObject.GetComponent<SpriteRenderer>();

        while (countdown > 0)
        {
            //if it's time to start flashing, toggle the visibility of the sprite.
            if (countdown <= flashStartTime)
            {
                StartCoroutine(FlashSprite(spriteRenderer, flashInterval, spawnedObject));
            }

            //decrease the countdown.
            countdown -= Time.deltaTime;

            yield return null;
        }

        //destroy the object after the countdown.
        Destroy(spawnedObject);
    }

    IEnumerator FlashSprite(SpriteRenderer spriteRenderer, float interval, GameObject spawnedObject)
    {
        if (spriteRenderer == null)
        {
            yield break; //exit the coroutine if the SpriteRenderer is null.
        }

        float timer = flashInterval;
        Color originalColor = spriteRenderer.color;

        while (timer > 0)
        {
            if (spawnedObject == null || spriteRenderer == null)
            {
                yield break; //exit the coroutine if the object or SpriteRenderer is null.
            }

            //smoothly transition the transparency of the sprite.
            float alpha = Mathf.PingPong(Time.time / interval, 1f);
            Color newColor = originalColor;
            newColor.a = alpha;

            //check if the SpriteRenderer is not destroyed.
            if (spriteRenderer != null)
            {
                spriteRenderer.color = newColor;
            }

            timer -= Time.deltaTime;

            //additional check to exit the coroutine if the object is destroyed.
            if (spawnedObject == null)
            {
                yield break;
            }

            yield return null;
        }

        //ensure the original color when the object is destroyed.
        if (spriteRenderer != null)
        {
            spriteRenderer.color = originalColor;
        }
    }
}
