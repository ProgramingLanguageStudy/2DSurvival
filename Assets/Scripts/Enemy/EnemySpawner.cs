using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Hero _hero;

    [SerializeField] GameObject _enemyPrefab;
    [SerializeField] Transform _spawnPoint;
    [SerializeField] float spawnTime = 3.0f;

    float _spawnTimer;

    private void Start()
    {
        _spawnTimer = spawnTime;
    }

    private void Update()
    {
        _spawnTimer -= Time.deltaTime;
        if (_spawnTimer <= 0f)
        {
            SpawnEnemy();
            _spawnTimer = spawnTime;
        }
    }

    void SpawnEnemy()
    {
        GameObject enemyObj = Instantiate(_enemyPrefab, _spawnPoint.position, Quaternion.identity);
        Enemy enemy = enemyObj.GetComponent<Enemy>();
        enemy.Initialize(_hero.transform); // ÇÊ¿ä ½Ã
    }
}
