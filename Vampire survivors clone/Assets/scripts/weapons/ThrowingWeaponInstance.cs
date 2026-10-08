using UnityEngine;
using static Interfaces;

public class ThrowingWeaponInstance : MonoBehaviour
{
    [HideInInspector] public float weaponDmg;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable collide = collision.gameObject.GetComponent<IDamageable>();
        if (collide != null)
        {
            collide.Takedamage(weaponDmg);
        }
    }
}
