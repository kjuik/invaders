using System;
using UnityEngine;
using Zenject;

public class EnemiesMovement: MonoBehaviour
{
    private const float ChangeDirTime = 5f; //TODO calculate
    
    private float _swapDirTimer = ChangeDirTime / 2f;
    private float _horizontalSpeed;
    private float _verticalSpeed;
    private Vector3 _currentHorizontalDir = Vector3.right;
    
    [Inject] private GameStateMachine _gameStateMachine;

    private Vector3 _initialPosition;
    
    protected void Awake()
    {
        _initialPosition = transform.position;
    }

    public void OnNewWave(float horizontalSpeed, float verticalSpeed)
    {
        transform.position = _initialPosition;
        _currentHorizontalDir = Vector3.right;
        
        _horizontalSpeed = horizontalSpeed;
        _verticalSpeed = verticalSpeed;
    }
    
    protected void Update()
    {
        if (_gameStateMachine.CurrentState == GameState.InGame)
            UpdateMovement();
    }

    private void UpdateMovement()
    {
        transform.Translate(
            ((_currentHorizontalDir * _horizontalSpeed) + (Vector3.down * _verticalSpeed)) * Time.deltaTime);
        
        _swapDirTimer += Time.deltaTime;
        if (_swapDirTimer >= ChangeDirTime)
        {
            _currentHorizontalDir = -_currentHorizontalDir;
            _swapDirTimer -= ChangeDirTime;
        }
    }
}
