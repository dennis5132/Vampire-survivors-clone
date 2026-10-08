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
        Debug.Log("dropped");
        int x = (int)(data.dropRatio*powerUp);
        for (int i = 0; i < x; i++)
        {
            GameObject newDrop = PoolManager.instance.DeQueuPooledItem("levelCurrency");
            newDrop.GetComponent<IGrabbable>().OnSpawn(transform.position);
            
        }
        
        PoolManager.instance.EnqueuPooledItem(gameObject);
    }
}
