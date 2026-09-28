using UnityEngine;
using static Interfaces;

public class testWeapon3 : MonoBehaviour, IWeaponable
{
    public string Name => "third weapon";

    public void DoAttack(int Level)
    {
        if (Level != 0)
        {
            Debug.Log("weapon3 attack");
        }
        //throw new System.NotImplementedException();
    }

    public int GetAttackSpeed()
    {
        return 50;
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
