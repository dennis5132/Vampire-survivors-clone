using UnityEngine;
using static Interfaces;

public class SeekingWeapon : MonoBehaviour, IWeaponable
{
    public WeaponSO SO;
    [SerializeField] private Transform player;

    [SerializeField]private float speed=1;
    public string Name => "seeking";

    public void DoAttack(int Level)
    {
        transform.position = player.position;
        if (Level == 0) { return; }
        bool aHit=false;
        RaycastHit2D hit;
        // for SO.range: spherecast with a size of i
        //while (!aHit) { 
        for (int i = 0; i < SO.range; i++)
        {
                if (aHit) {  break; }
            var h = Physics2D.CircleCastAll(transform.position, i, transform.forward);
            if (h.Length !=0)
            {
                hit = h[0];
                if (hit.rigidbody.gameObject.GetComponent<IEnemyable>() != null)
                {
                        aHit = true;
                    // deque seekinginstance
                    GameObject newEnemy = PoolManager.instance.DeQueuPooledItem("seekingInstance");
                        newEnemy.transform.position = transform.position;
                    //addforce towards goal
                    Vector2 force = (hit.rigidbody.transform.position-transform.position).normalized;
                    newEnemy.GetComponent<Rigidbody2D>().AddForce(force * speed);
                }
            }
        }
        //}
        //throw new System.NotImplementedException();
    }

    public float GetAttackSpeed()
    {
        //throw new System.NotImplementedException();
        return SO.attackSpeed;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Gizmos.DrawWireSphere(transform.position, 10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
