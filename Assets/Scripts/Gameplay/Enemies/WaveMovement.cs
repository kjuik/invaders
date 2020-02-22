using UnityEngine;

public class WaveMovement: MonoBehaviour
{
    private const float ChangeDirTime = 5f;
    private float _swapDirTimer = ChangeDirTime / 2f;
    
    private float _horizontalSpeed;
    private float _verticalSpeed;
    private Vector3 _currentHorizontalDir = Vector3.right;
    
    private GameStateMachine _gameStateMachine;

    public void Init(GameStateMachine gameStateMachine, float horizontalSpeed, float verticalSpeed)
    {
        _gameStateMachine = gameStateMachine;
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
