using UnityEngine;
using static Interfaces;

public class testEnemyScript : MonoBehaviour,IEnemyable,IDamageable
{
    public enemySO data;
    private Rigidbody2D rb;
    //[SerializeField] private float moveSpeed = 50;
    //[SerializeField] private float dmg=1;
    private Transform player;
    private float HP;
    private float powerUp;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
    }

    

    private void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position,player.position,data.speed);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject == player.gameObject)
        {
            collision.gameObject.GetComponent<IDamageable>().Takedamage(data.damage * powerUp * Time.deltaTime);
            //Takedamage(data.damage);//
        } 
    }

    public void applySO(enemySO SO,int wave)
    {
        data = SO;
        powerUp = Mathf.Pow(data.powerIncrease,wave);
        HP = data.health*powerUp;
        gameObject.GetComponent<SpriteRenderer>().color = SO.Color;
    }

    public void Takedamage(float damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            Die();
        }
    }

    public void Takedamage(float damage, Vector2 Knockback)
    {
        throw new System.NotImplementedException();
    }

    public void Die()
    {
        int dropAmount = (int)(data.dropRatio*powerUp);
        dropAmount= Random.Range(dropAmount/2,dropAmount*2);
        for (int i = 0; i < dropAmount; i++)
        {
            GameObject newDrop = PoolManager.instance.DeQueuPooledItem("levelCurrency");
            newDrop.GetComponent<IGrabbable>().OnSpawn(transform.position);
        }
        if (data.dropChance > Random.Range(1, 100))
        {
            GameObject newDrop = PoolManager.instance.DeQueuPooledItem("levelCurrency");
            newDrop.GetComponent<IGrabbable>().OnSpawn(transform.position);
        }
        
        PoolManager.instance.EnqueuPooledItem(gameObject);
    }
}
