using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float velocity;
    private Camera _mainCamera;

    protected void Awake()
    {
        _mainCamera = Camera.main;
        Cursor.visible = false;
    }

    protected void Update()
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
