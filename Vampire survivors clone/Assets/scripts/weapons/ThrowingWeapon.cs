using UnityEngine;
using static Interfaces;

public class ThrowingWeapon : MonoBehaviour, IWeaponable
{
    public WeaponSO SO;
    [SerializeField] private Transform player;
    [SerializeField] private GameObject instance;
    public string Name => "throwing";

    //public int Lvl { get => Lvl; set => Lvl; }

    public void DoAttack(int Level)
    {
        if (Level == 0) {  return; }
        var axe = Instantiate(instance,player.position,Quaternion.identity);
        if (axe != null)
        {
            axe.GetComponent<ThrowingWeaponInstance>().weaponDmg = SO.attackPower* Mathf.Pow(SO.powerMod,Level);
            axe.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-SO.range,SO.range),Random.Range(SO.distance-300,SO.distance)));
        }
        Destroy(axe, 3f);
    }

    public float GetAttackSpeed()
    {
        return 100;
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
