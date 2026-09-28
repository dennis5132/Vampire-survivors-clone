using UnityEngine;
using static Interfaces;

public class LevelCurrencyScript : MonoBehaviour, IGrabbable
{
    [SerializeField] private UpgradeMenu menu;
    [SerializeField] private float spread = 5;
    public void OnGrab()
    {
        PoolManager.instance.EnqueuPooledItem(this.gameObject);
        menu.AddCurrency();
        //Debug.Log("grabbed");
        //throw new System.NotImplementedException();
    }

    public void OnSpawn(Vector2 l)
    {
        transform.position = new Vector2(Random.Range(-spread + l.x, spread + l.x), Random.Range(-spread + l.y,spread + l.y));
        //throw new System.NotImplementedException();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        menu = GameObject.Find("weaponMenuManager").GetComponent<UpgradeMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
