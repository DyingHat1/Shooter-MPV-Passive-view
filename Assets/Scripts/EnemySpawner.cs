using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private int _defaultEnemyCount;
    [SerializeField] private Tilemap _tileMap;
    [SerializeField] private PresentersFactory _factory;
    [SerializeField] private PlayerPresenter _player;

    private List<Enemy> _enemies = new List<Enemy>();
    private float _spawnRadiusX;
    private float _spawnRadiusY;

    public event UnityAction AllEnemiesDied;

    private void Awake()
    {
        _tileMap.CompressBounds();
        _spawnRadiusX = _tileMap.size.x / 2;
        _spawnRadiusY = _tileMap.size.y / 2;

        for (int i = 0; i < _defaultEnemyCount; i++)
            _enemies.Add(SpawnDefaultEnemy());
    }

    private void OnEnable()
    {
        foreach (Enemy enemy in _enemies)
            enemy.Died += OnEnemyDied;
    }

    private void OnDisable()
    {
        foreach (Enemy enemy in _enemies)
            enemy.Died -= OnEnemyDied;
    }

    private void OnEnemyDied(Creature enemy)
    {
        _defaultEnemyCount--;
        _factory.CreateRandomItem(enemy.Position);
        enemy.Died -= OnEnemyDied;

        if (_defaultEnemyCount == 0)
            AllEnemiesDied?.Invoke();
    }

    private DefaultEnemy SpawnDefaultEnemy()
    {
        Vector2 position = GetRandomSpawnPosition(_spawnRadiusX, _spawnRadiusY);
        DefaultEnemy enemy = new DefaultEnemy(Config.DefaultEnemyHealth, position, 0, Config.DefaultMaxAttackDistance, 
            Config.DefaultEnemyDamage, Config.DefaultEnemySpeed, _player.Model);
        _factory.CreateDefaultEnemy(enemy);
        return enemy;
    }

    private Vector2 GetRandomSpawnPosition(float radiusX, float radiusY)
    {
        return new Vector2(_tileMap.transform.position.x + Random.Range(-radiusX, radiusX), 
            _tileMap.transform.position.y + Random.Range(-radiusY, radiusY));
    }
}
