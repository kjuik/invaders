using UnityEngine;
using Zenject;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private float rechargeTime;
    [SerializeField] private float projectileLifetime;
    
    [Inject] private GameStateMachine _gameStateMachine;
    [Inject] private ObjectPool _pool;

    private float _rechargeTimer;
    protected void Update()
    {
        if (_gameStateMachine.CurrentState == GameState.InGame)
            UpdateShooting();
    }

    private void UpdateShooting()
    {
        if (_rechargeTimer > 0f)
            _rechargeTimer -= Time.deltaTime;
        
        if (Input.GetMouseButton(0) && _rechargeTimer <= 0f)
            Shoot();
    }

    private void Shoot()
    {
        var projectile = _pool.Rent(projectilePrefab, projectileLifetime);
        projectile.transform.SetPositionAndRotation(transform.position, transform.rotation);
        _rechargeTimer += rechargeTime;
    }
}
