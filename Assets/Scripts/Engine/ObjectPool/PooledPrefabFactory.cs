using UnityEngine;
using Zenject;

public class PooledPrefabFactory : IFactory<Object, PooledObject>
{
    [Inject] private DiContainer _container;
    public PooledObject Create(Object prefab) => _container.InstantiatePrefab(prefab).AddComponent<PooledObject>();
}
