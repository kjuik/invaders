using UnityEngine;
public class Projectile : MonoBehaviour
{
    [SerializeField] private Vector2 speed;
    protected void Update()
    {
        transform.Translate(speed * Time.deltaTime);
    }
}