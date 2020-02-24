 using System;
 using UnityEngine;

 public class Enemy : MonoBehaviour
 {
     public event Action<Enemy> OnDied;
     private void OnCollisionEnter(Collision _) => Die();
     
     private void Die()
     {
         OnDied?.Invoke(this);
         Destroy(gameObject);
     }
 }
