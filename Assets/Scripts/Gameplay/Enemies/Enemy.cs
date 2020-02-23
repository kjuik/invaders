 using System;
 using UnityEngine;
 using Zenject;

 public class Enemy : MonoBehaviour
 {
     [Inject] private ObjectPool _pool;

     public event Action<Enemy> OnDied;
     private void OnCollisionEnter(Collision _) => Die();
     
     private void Die()
     {
         _pool.Return(this);
         OnDied?.Invoke(this);
     }
 }
