using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public Enemy enemyPrefab;
        public float spawnInterval = 2;
        public int maxEnemies = 20;
    }

    public Transform[] waypoints;
    public Wave[] waves;
    public int currentWaveIndex = 0;
    public int timeBetweenWaves = 5;

    // events
    public UnityEvent OnWaveStarted = new UnityEvent();
    public UnityEvent OnGameWon = new UnityEvent();

    private List<GameObject> activeEnemies = new List<GameObject>();

    IEnumerator Start ()
    {
        activeEnemies.Clear();

        // Current wave index will be incremented once we've spawned all the enemies for this wave.
        while (currentWaveIndex < waves.Length)
        {
            OnWaveStarted?.Invoke();

            for (var i = 0; i < waves[currentWaveIndex].maxEnemies; i++)
            {
                // Spawn an enemy, then wait for the spawn interval before continuing.
                SpawnEnemy();

                yield return new WaitForSeconds(waves[currentWaveIndex].spawnInterval);
            }
            // Increment the current wave index. You could also write a for loop rather than a while loop if you prefer.
            currentWaveIndex++;

            // Check if all waves have been completed
            if (currentWaveIndex >= waves.Length && activeEnemies.Count == 0)
            {
                //invoke the gamewon event, the gamemanager will listen to this, invoke the same event, and then the userinterface will listen to the gamemanager
                OnGameWon?.Invoke();
            }
            else
            {
                yield return new WaitForSeconds(timeBetweenWaves);
            }
        }
    }

    //this method will deal enemies that have just been destroyed, removes them from the list
    //it also checks if all waves are completed and if so invoke gamewon
    //this function already exists above however this method still needs this to work
    private void HandleEnemyDefeat(Enemy enemy)
    {
        //activeEnemies.Remove(enemy);
        activeEnemies.Remove(enemy.gameObject);
        if (currentWaveIndex >= waves.Length && activeEnemies.Count == 0)
        {
            OnGameWon?.Invoke();
        }
    }

    public void SpawnEnemy()
    {
        // Looks at the current wave to determine which enemy we should spawn.
        Enemy newEnemy = Instantiate(waves[currentWaveIndex].enemyPrefab, transform.position, Quaternion.identity);
        newEnemy.waypoints = waypoints;

        //listens for an invoke of enemydefeated,
        //these invokes should either come from an enemy being defeated by a monster
        //or an enemy reaching the cookie and destroying itself
        newEnemy.EnemyDefeated.AddListener(HandleEnemyDefeat);

        //add spawned enemy to the list
        activeEnemies.Add(newEnemy.gameObject);
    }
}