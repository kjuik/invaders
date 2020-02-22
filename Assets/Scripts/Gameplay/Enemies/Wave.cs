using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    private List<Enemy> _members;
    public IReadOnlyList<Enemy> Members => _members;

    public void Init(IEnumerable<Enemy> enemies)
    {
        _members = new List<Enemy>(enemies);
        foreach (var e in _members)
        {
            e.transform.SetParent(transform);
            e.OnDied += OnEnemyDied;
        }
    }

    private void OnEnemyDied(Enemy e) => _members.Remove(e);
}
