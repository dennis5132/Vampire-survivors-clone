using UnityEngine;
using static Interfaces;

public class testEnemyScript : MonoBehaviour,IEnemyable,IDamageable
{
    public enemySO data;
    private Rigidbody2D rb;
    private SpriteRenderer SpriteRenderer;
    //[SerializeField] private float moveSpeed = 50;
    //[SerializeField] private float dmg=1;
    private Transform player;
    private float HP;
    private float powerUp;

    [SerializeField]private int currencyMaxLevel=10;
    [SerializeField] private int currencyLevelIncrease=10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position,player.position,data.speed);
        if (transform.position.x < player.position.x) 
        {
            SpriteRenderer.flipX = false;
        }
        else { SpriteRenderer.flipX = true; }
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

        int[] cLevels = new int[currencyMaxLevel];
        cLevels[0] = dropAmount;
        for (int i = 0; i < cLevels.Length-1; i++)
        {
            while (cLevels[i] > currencyLevelIncrease)
            {
                cLevels[i] -= currencyLevelIncrease;
                cLevels[i+1]++;
            }
        }

        for (int i = 0; i < cLevels.Length; i++)
        {
            for (int j = 0; j < cLevels[i]; j++)
            {
                GameObject newDrop = PoolManager.instance.DeQueuPooledItem("levelCurrency");
                newDrop.GetComponent<IGrabbable>().OnSpawn(transform.position, (int)(Mathf.Pow(currencyLevelIncrease,i+1)/currencyLevelIncrease));
            }
        }

        int r = Random.Range(1, 100);
        if (data.dropChance > r)
        {
            GameObject newDrop = PoolManager.instance.DeQueuPooledItem("UpgradeCurrency");
            //Debug.Log("upgrade");
            newDrop.GetComponent<IGrabbable>().OnSpawn(transform.position,1);
        }
        
        PoolManager.instance.EnqueuPooledItem(gameObject);
    }
}
