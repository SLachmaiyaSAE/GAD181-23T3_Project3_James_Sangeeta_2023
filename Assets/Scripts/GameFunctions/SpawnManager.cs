using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] spawnPoints;
    public GameObject[] spawnObjects;

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
            Instantiate(objectToSpawn, spawnPoint.position, Quaternion.identity);

            //delay between spawns.
            yield return new WaitForSeconds(8f);
        }
    }
}