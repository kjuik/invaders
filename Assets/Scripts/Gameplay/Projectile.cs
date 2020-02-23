using UnityEngine;
using Zenject;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Vector2 speed;

    [Inject] private ObjectPool _pool;

    private Rigidbody _cachedRigidbody;
    private Rigidbody Rigidbody => _cachedRigidbody ? _cachedRigidbody : _cachedRigidbody = GetComponent<Rigidbody>();

    protected void Update()
    {
        Rigidbody.velocity = speed;
    }

    private void OnCollisionEnter(Collision _) => _pool.Return(this);

}