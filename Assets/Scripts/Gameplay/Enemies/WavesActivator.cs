using System;
using UnityEngine;
using Zenject;

public class WavesActivator : MonoBehaviour
{
    [SerializeField] private Wave[] waves;

    [Inject] private GameStateMachine _gameStateMachine;
    
    private int _nextWaveIndex = 0;
    private Wave _currentWave;

    protected void Start()
    {
        foreach (var wave in waves)
            wave.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (_gameStateMachine.CurrentState == GameState.InGame &&
            (_currentWave == null || _currentWave.Members.Count == 0))
        {
            if (_nextWaveIndex < waves.Length)
                ActivateNextWave();
            else
                _gameStateMachine.CurrentState = GameState.Won;
        }
    }

    private void ActivateNextWave()
    {
        if (_currentWave)
            _currentWave.gameObject.SetActive(false);

        _currentWave = waves[_nextWaveIndex];
        _currentWave.gameObject.SetActive(true);
        _nextWaveIndex++;
    }
}
