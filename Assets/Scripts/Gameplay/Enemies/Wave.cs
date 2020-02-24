using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Wave : MonoBehaviour
{
    private List<Enemy> _members;
    public IReadOnlyList<Enemy> Members => _members;

    public void Awake()
    {
        _members = GetComponentsInChildren<Enemy>(true).ToList();
        foreach (var e in _members)
            e.OnDied += OnEnemyDied;
    }

    private void OnEnemyDied(Enemy e) => _members.Remove(e);
}
