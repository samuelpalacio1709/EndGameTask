using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles entity spawn positions
/// </summary>
public class SpawnManager : Singleton<SpawnManager>
{
    [SerializeField] private List<Transform> spawnPoints = new List<Transform>();
    [SerializeField] private int timeToSpawn;
    [SerializeField] private ParticleSystem respawnPS;
    private List<Transform> usedSpawnPoinst = new List<Transform>();
    public void SpawnEntity(Transform entityTransform)
    {
        entityTransform.gameObject.SetActive(false);
        StartCoroutine(Spawn(timeToSpawn, entityTransform));

    }

    private IEnumerator Spawn(float time, Transform entityTransform)
    {
        if (spawnPoints.Count > 0)
        {
            int randomSpawnPointIndex = Random.Range(0, spawnPoints.Count);
            var spawnPoint = spawnPoints[randomSpawnPointIndex];
            usedSpawnPoinst.Add(spawnPoint);
            spawnPoints.Remove(spawnPoint);
            yield return new WaitForSeconds(time);
            respawnPS.gameObject.transform.position = spawnPoint.position;
            respawnPS.Play();
            yield return new WaitForSeconds(respawnPS.main.duration / 2);

            entityTransform.transform.position = spawnPoint.position;
            entityTransform.gameObject.SetActive(true);
            usedSpawnPoinst.Remove(spawnPoint);
            spawnPoints.Add(spawnPoint);

        }



    }
}




