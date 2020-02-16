using UnityEngine;
using Zenject;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float velocity;
    
    [Inject] private GameManager _gameManager;
    [Inject] private Camera _mainCamera;

    protected void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }
    protected void Update()
    {
        if (_gameManager.State == GameManager.GameState.InGame)
            UpdateMovement();
    }

    private void UpdateMovement()
    {
        var mousePosition = Input.mousePosition;
        mousePosition = _mainCamera.ScreenToWorldPoint(mousePosition);
        
        var playerPosition = transform.position;
        transform.position = new Vector3(
            Mathf.Lerp(playerPosition.x, mousePosition.x, velocity * Time.deltaTime),
            playerPosition.y,
            playerPosition.z);
    }
}
