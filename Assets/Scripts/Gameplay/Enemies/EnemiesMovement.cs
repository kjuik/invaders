using UnityEngine;
using Zenject;

public class EnemiesMovement: MonoBehaviour
{
    [SerializeField] private float period;
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float verticalSpeed;
    
    private float _swapDirTimer;
    private Vector3 _currentHorizontalDir = Vector3.right;
    
    [Inject] private GameStateMachine _gameStateMachine;
    
    protected void Awake()
    {
        _swapDirTimer = period / 2f;
        _currentHorizontalDir = Vector3.right;
    }

    protected void Update()
    {
        if (_gameStateMachine.CurrentState == GameState.InGame)
            UpdateMovement();
    }

    private void UpdateMovement()
    {
        transform.Translate(
            ((_currentHorizontalDir * horizontalSpeed) + (Vector3.down * verticalSpeed)) * Time.deltaTime);
        
        _swapDirTimer += Time.deltaTime;
        if (_swapDirTimer >= period)
        {
            _currentHorizontalDir = -_currentHorizontalDir;
            _swapDirTimer -= period;
        }
    }
}
