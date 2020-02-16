using UnityEngine;
public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        BeforeGame = 0,
        InGame = 1,
        Paused = 2,
        Lost = 3,
        Won = 4
    }

    public GameState State { get; private set; } = GameState.InGame;
}
