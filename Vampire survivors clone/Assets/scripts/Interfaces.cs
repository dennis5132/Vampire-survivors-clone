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
        string Name { get;}
        //int Lvl { get; set; }
        void DoAttack(int Level);
        float GetAttackSpeed();

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
