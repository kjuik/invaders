using UnityEngine;
public class GameStateMachine : MonoBehaviour
{
    [SerializeField] private GameState startingState = GameState.InGame;
    
    private GameState _currentState;

    public delegate void StateChangedDelegate(GameState oldState, GameState newState);
    public event StateChangedDelegate OnStateChanged;
    
    public GameState CurrentState
    {
        get => _currentState;
        set
        {
            if (_currentState == value) 
                return;
            
            var oldState = _currentState;
            _currentState = value;
            OnStateChanged?.Invoke(oldState, _currentState);
        }
    }

    private void Start()
    {
        CurrentState = startingState;
    }
}
