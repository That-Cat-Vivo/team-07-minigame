/* Ethan Gapic-Kott */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniGameCollection.Games2025.Team07
{
    public class PowerupSpawner : MonoBehaviour
    {
        [Header("Spawn Points (Assign 2 empty GameObjects)")]
        [SerializeField] private Transform[] spawnPoints;

        [Header("Powerup Prefabs")]
        [SerializeField] private GameObject[] powerupPrefabs; // e.g., BarrierPowerup, SpeedPowerup

        [Header("Spawn Timing")]
        [SerializeField] private float spawnInterval = 8f; // how often to spawn
        [SerializeField] private float firstSpawnDelay = 2f;

        private GameObject activePowerup; // track the current spawned one

        private void Start()
        {
            StartCoroutine(SpawnLoop());
        }

        private IEnumerator SpawnLoop()
        {
            yield return new WaitForSeconds(firstSpawnDelay);

            while (true)
            {
                if (activePowerup == null)
                {
                    SpawnRandomPowerup();
                }
                yield return new WaitForSeconds(spawnInterval);
            }
        }

        private void SpawnRandomPowerup()
        {
            if (spawnPoints.Length == 0 || powerupPrefabs.Length == 0)
                return;

            // Pick random spawn point and random powerup type
            Transform spawnPos = spawnPoints[Random.Range(0, spawnPoints.Length)];
            GameObject prefab = powerupPrefabs[Random.Range(0, powerupPrefabs.Length)];

            activePowerup = Instantiate(prefab, spawnPos.position, Quaternion.identity);
        }
    }
}
