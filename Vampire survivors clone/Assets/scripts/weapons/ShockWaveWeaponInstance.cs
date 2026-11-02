using UnityEngine;
using static Interfaces;

public class ShockWaveWeaponInstance : MonoBehaviour
{
    private ShockWaveWeapon parent;
    public float Damage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        parent = transform.parent.GetComponent<ShockWaveWeapon>();
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
            collide.Takedamage(Damage * parent.visibility);
        }
    }
}
