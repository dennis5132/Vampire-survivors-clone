using UnityEngine;
using static Interfaces;


public class testWeapon1 : MonoBehaviour, IWeaponable
{

    public string Name => "first weapon";

    public void DoAttack(int Level)
    {
        if (Level != 0)
        {
            Debug.Log("weapon1 attack");
        }
        
        //throw new System.NotImplementedException();
    }

    public float GetAttackSpeed()
    {
        return 100;
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
