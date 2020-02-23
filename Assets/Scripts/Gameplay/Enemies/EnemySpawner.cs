using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [Serializable]
    public struct EnemyWaveConfig
    {
        [Serializable]
        public struct EnemyConfig
        {
            public Enemy enemy;
            public float probability;
        }

        public int width;
        public int height;
        
        public float horizontalSpeed;
        public float verticalSpeed;
        public float maxLifetime;

        public List<EnemyConfig> enemyTypes;
    }

    [Inject] private GameStateMachine _gameStateMachine;
    [Inject] private ObjectPool _pool;
    [Inject] private EnemiesMovement _enemiesMovement;
    [Inject] private Wave NewWave { get; set; }
    
    [SerializeField] private float enemySpacing;
    [SerializeField] private List<EnemyWaveConfig> waveConfigs;
    
    private int _nextWaveIndex = 0;
    private Wave _currentWave;
    
    protected void Update()
    {
        if (_gameStateMachine.CurrentState == GameState.InGame)
            UpdateSpawning();
    }

    private void UpdateSpawning()
    {
        if (_currentWave == null || _currentWave.Members.Count == 0)
        {
            if (_nextWaveIndex < waveConfigs.Count)
                SpawnNewWave();
            else
                _gameStateMachine.CurrentState = GameState.Won;
        }
    }
    
    private void SpawnNewWave()
    {
        var config = waveConfigs[_nextWaveIndex];

        var spawnedEnemies = new List<Enemy>();
        for (var i = 0; i < config.width; i++)
        {
            for (var j = 0; j < config.height; j++)
            {
                var enemyPosition = GenerateEnemyPosition(i, j, config.width - 1, config.height - 1);
                spawnedEnemies.Add(SpawnNewEnemy(config.enemyTypes, enemyPosition, config.maxLifetime));
            }        
        }

        if (_currentWave)
        {
            Destroy(_currentWave.gameObject);
        }
        
        _currentWave = NewWave;
        var waveTransform = _currentWave.transform;
        waveTransform.SetParent(_enemiesMovement.transform);
        waveTransform.localPosition = Vector3.zero;
        
        _currentWave.Init(spawnedEnemies);

        _enemiesMovement.OnNewWave(config.horizontalSpeed, config.verticalSpeed);
        
        _nextWaveIndex++;
    }

    private Vector3 GenerateEnemyPosition(int horizIndex, int vertIndex, int horizCount, int vertCount) =>
        transform.position + new Vector3(
            enemySpacing * (horizCount * 0.5f - horizIndex),
            enemySpacing * (vertCount * 0.5f - vertIndex),
            0f);

    private Enemy SpawnNewEnemy(List<EnemyWaveConfig.EnemyConfig> enemyConfigs, Vector3 position, float maxLifetime)
    {
        var sumOfProbabilities = enemyConfigs.Sum(p => p.probability);
        var random = Random.Range(0f, sumOfProbabilities);

        var counter = 0f;
        foreach (var ec in enemyConfigs)
        {
            if (counter + ec.probability >= random)
            {
                var enemy = _pool.Rent(ec.enemy, maxLifetime);
                
                enemy.transform.position = position;
                return enemy;
            }
            
            counter += ec.probability;
        }
        
        throw new Exception("Enemy choice failed. Did you set the wave member probabilities correctly?");
    }
}
