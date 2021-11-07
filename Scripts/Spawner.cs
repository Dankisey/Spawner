using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoints;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private int _amount;
    [SerializeField] private int _radius;

    private Transform[] _spawners;

    private void Start()
    {
        int angleStep = 360 / _amount;

        _spawners = new Transform[_spawnPoints.childCount];

        for (int i = 0; i < _spawnPoints.childCount; i++)
        {
            _spawners[i] = _spawnPoints.GetChild(i);
        }

        StartCoroutine(SpawnEnemies(angleStep));
    }

    private IEnumerator SpawnEnemies(int angleStep)
    {
        var waitForTwoSeconds = new WaitForSeconds(2);

        for (int currentSpawner = 0; currentSpawner < _spawners.Length; currentSpawner++)
        {
            for (int i = 0; i < _amount; i++)
            {
                GameObject newEnemy = Instantiate(_enemyPrefab, _spawners[currentSpawner].position, Quaternion.identity);
                Vector3 radiusDistance= new Vector3(_radius * Mathf.Cos(angleStep * (i + 1) * Mathf.Deg2Rad), _radius * Mathf.Sin(angleStep * (i + 1) * Mathf.Deg2Rad), 0);

                newEnemy.transform.position += radiusDistance;
            }

            yield return waitForTwoSeconds;
        }  
    }
}