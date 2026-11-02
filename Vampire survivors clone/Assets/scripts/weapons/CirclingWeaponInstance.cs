using Unity.VisualScripting;
using UnityEngine;
using static Interfaces;

public class CirclingWeaponInstance : MonoBehaviour
{
    private Vector2 point;
    private float moveTime;
    private float time=0;
    private int timeAroundPlayer=110;
    public float radius = 0;
    //public float Damage;
    [SerializeField] float radiusMax = 1;
    //public CirclingWeapon parent;
    [SerializeField] private float baseSpeed;

    public float Damage;
    Transform playerPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Begin(float EndTime,Transform player,float timesAround)
    {
        moveTime = EndTime;
        time = 0;
        transform.position = player.position;
        timeAroundPlayer = (int)timesAround;

        playerPos = player;
    }
    //ontriggerenter: doDamage
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable collide = collision.gameObject.GetComponent<IDamageable>();
        if (collide != null)
        {
            collide.Takedamage(Damage);
        }
    }
    private void FixedUpdate()
    {
        if (gameObject.activeSelf)
        {
            time++;
                        
            //weaponsprite circles around player
                // get move time
                // weaponSprite position along circle based on how far the timer is
                    // spriteamount = times around the player * 100
                    // i = 100 / endTime * time
            int i = (int)(100 / moveTime * time);
                // distance from player also based on formula
                     float moveAway = time / 2;//
            
                     //radius += moveAway;//
                     moveAway-=radiusMax;//
            //radius = parent.SO.distance;
            //Debug.Log(radius);
                // generate circle
                     float circleposition = i / (float)timeAroundPlayer;
                     float x = Mathf.Sin(circleposition * Mathf.PI * 2.0f) * radius;
                     float z = Mathf.Cos(circleposition * Mathf.PI * 2.0f) * radius;
                     point = new Vector2(x+ playerPos.position.x, z+ playerPos.position.y)/*+transform.parent.position*/;

            //move towards point
            transform.position = Vector2.MoveTowards(transform.position,point,baseSpeed);
            if (time > moveTime)
            {
                PoolManager.instance.EnqueuPooledItem(gameObject);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf) 
        {

        }
    }
}
