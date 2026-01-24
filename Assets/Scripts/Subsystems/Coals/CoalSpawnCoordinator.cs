using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoalSpawnCoordinator : MonoBehaviour
{
    [SerializeField] private List<CoalSpawner> _spawners;
    [SerializeField] private bool _isSpawn = true;
    [SerializeField] private float _spawnInterval = 2f;

    private Coroutine _spawnCoroutine;

    private void Start()
    {
        _spawnCoroutine = StartCoroutine(SpawnCoalRoutine());
    }

    private IEnumerator SpawnCoalRoutine()
    {
        var wait = new WaitForSeconds(_spawnInterval);

        while (_isSpawn)
        {
            yield return wait;

            int currentSpawnerIndex = Random.Range(0, _spawners.Count);
            int firstSpawnerIndex = currentSpawnerIndex;

            do
            {
                var currentSpawner = _spawners[currentSpawnerIndex];

                if (currentSpawner.CanSpawn())
                {
                    currentSpawner.Spawn();
                    break;
                }

                currentSpawnerIndex = (currentSpawnerIndex + 1) % _spawners.Count;

            } while (currentSpawnerIndex == firstSpawnerIndex);
        }
    }
}
