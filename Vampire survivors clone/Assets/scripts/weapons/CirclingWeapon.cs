using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using static Interfaces;

public class CirclingWeapon : MonoBehaviour, IWeaponable
{
    public WeaponSO SO;
    public Transform player;

    private List<CirclingWeaponInstance> active = new List<CirclingWeaponInstance>();
    public float radiusMax;
    public float endTime;
    public float timesAround;
    public string Name => "circling";

    //int Lvl { set => throw new System.NotImplementedException(); }


    public void DoAttack(int Level)
    {
        //Lvl = Level;
        transform.position = player.position;
        if (Level == 0) { return; }
        //dequeu weaponsprite
        GameObject x = PoolManager.instance.DeQueuPooledItem("circlingInstance");
        //add to active
        active.Add(x.GetComponent<CirclingWeaponInstance>());
        //parent=this
        x.GetComponent<CirclingWeaponInstance>().parent = this;
        x.GetComponent<CirclingWeaponInstance>().Begin();
    }

    public float GetAttackSpeed()
    {
        return SO.attackSpeed;
        //throw new System.NotImplementedException();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
