using UnityEngine;

public class PooledObject : MonoBehaviour
{
    public int PrefabId { get; private set; }
    private ObjectPool _pool;

    private float _lifeTimer;

    public void Init(int prefabId, ObjectPool pool)
    {
        gameObject.SetActive(false);
        transform.SetParent(pool.transform);
        
        PrefabId = prefabId;
        _pool = pool;
    }
    
    public void OnRent(float maxLifetime)
    {
        gameObject.SetActive(true);
        transform.SetParent(null);
        
        _lifeTimer = maxLifetime;
    }
    
    protected void Update()
    {
        _lifeTimer -= Time.deltaTime;
        if (_lifeTimer <= 0f)
            _pool.Return(this);
    }
    
    public void OnReturn()
    {
        gameObject.SetActive(false);
        transform.SetParent(_pool.transform);
    }
}