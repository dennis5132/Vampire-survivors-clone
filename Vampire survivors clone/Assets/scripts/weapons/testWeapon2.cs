using UnityEngine;
using static Interfaces;

public class testWeapon2 : MonoBehaviour, IWeaponable
{
    public string Name => "second weapon";

    public void DoAttack(int Level)
    {
        if (Level != 0)
        {
            Debug.Log("weapon2 attack");
        }
        //throw new System.NotImplementedException();
    }

    public float GetAttackSpeed(int Level)
    {
        return 150;
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
