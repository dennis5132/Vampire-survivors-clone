using System;
using UnityEngine;

public class Interfaces
{
    public interface IDamageable
    {
        void Takedamage(float damage);
        void Takedamage(float damage, Vector2 Knockback);
        void Die();
    }
    
    public interface IWeaponable
    {
        string Name { get; }
        void DoAttack(int Level);
        int GetAttackSpeed();

    };
    public interface IEnemyable
    {
        public void applySO(enemySO data,int wave);
    }
    public interface IGrabbable 
    {
        public void OnSpawn(Vector2 location);
        public void OnGrab();
    }
}
