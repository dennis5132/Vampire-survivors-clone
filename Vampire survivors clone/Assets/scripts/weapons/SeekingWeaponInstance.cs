using UnityEngine;
using static Interfaces;

public class SeekingWeaponInstance : MonoBehaviour
{
    public SeekingWeapon parent;
    private int liveTime;

    public float Damage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        parent = transform.parent.GetComponent<SeekingWeapon>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Vector2.Distance(parent.transform.position,transform.position)>parent.SO.distance)
        {
            PoolManager.instance.EnqueuPooledItem(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable x = collision.gameObject.GetComponent<IDamageable>();
        if (x != null)
        {
            x.Takedamage(Damage);
            PoolManager.instance.EnqueuPooledItem(gameObject);
        }
    }
}
