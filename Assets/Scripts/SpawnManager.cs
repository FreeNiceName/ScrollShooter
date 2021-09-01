using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _enemyPrefabs;
    [SerializeField] private GameObject _powerupPrefab;

    private float _xSpawnRange = 25;
    private float _zSpawnPos = 15;
    private float _ySpawnPos = 0.5f;

    private float _enemySpawnInterval = 5;
    private float _powerupSpawnInterval = 50;
    private float _spawnDelay = 3;

    void Start()
    {
        InvokeRepeating("SpawnEnemy", _spawnDelay, _enemySpawnInterval);
        InvokeRepeating("SpawnPowerup", _spawnDelay, _powerupSpawnInterval);
    }

    private void SpawnEnemy()
    {
        var enemyIndex = Random.Range(0, _enemyPrefabs.Length);
        var enemy = _enemyPrefabs[enemyIndex];
        var enemyScript = enemy.GetComponentInChildren<Enemy>();

        switch (enemyScript)
        {
            case SwarmEnemy swarm:
                {
                    var swarmSize = Random.Range(swarm.MinSwarmSize, swarm.MaxSwarmSize + 1);
                    StartCoroutine(SpawnSwarmEnemies(enemy, swarmSize, swarm.SpawnInterval));
                    break;
                }
            default:
                {
                    Instantiate(enemy, NextSpawnPosition(), enemy.transform.rotation);
                    break;
                }

        }
    }

    private void SpawnPowerup()
    {
        Instantiate(_powerupPrefab, NextSpawnPosition(), _powerupPrefab.transform.rotation);
    }

    private Vector3 NextSpawnPosition()
    {
        var xSpawnPos = Random.Range(-_xSpawnRange, _xSpawnRange);
        var spawnPos = new Vector3(xSpawnPos, _ySpawnPos, _zSpawnPos);
        return spawnPos;
    }

    private IEnumerator SpawnSwarmEnemies(GameObject enemy, int swarmSize, float spawnInterval)
    {
        var spawnPos = NextSpawnPosition();
        for (var i = 0; i < swarmSize; ++i)
        {
            Instantiate(enemy, spawnPos, enemy.transform.rotation);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
