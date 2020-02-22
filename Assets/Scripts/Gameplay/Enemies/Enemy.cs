 using System;
 using UnityEngine;

 public class Enemy : MonoBehaviour
 {
     public event Action<Enemy> OnDied;
     public void Kill() => OnDied?.Invoke(this);
 }
