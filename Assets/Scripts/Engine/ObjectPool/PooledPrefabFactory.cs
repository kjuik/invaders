using UnityEngine;
using Zenject;

public class PooledPrefabFactory : IFactory<Object, PooledObject>
{
    readonly DiContainer _container;

    public PooledPrefabFactory(DiContainer container)
    {
        _container = container;
    }

    public PooledObject Create(Object prefab) => _container.InstantiatePrefab(prefab).AddComponent<PooledObject>();
}
