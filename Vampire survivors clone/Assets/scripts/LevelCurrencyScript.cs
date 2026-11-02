using UnityEngine;
using static Interfaces;

public class LevelCurrencyScript : MonoBehaviour, IGrabbable
{
    private UpgradeMenu menu;
    [SerializeField] private float spread = 5;
    private int worth=1;
    [SerializeField] private float sizeIncrease=0.1f;
    public void OnGrab()
    {
        PoolManager.instance.EnqueuPooledItem(gameObject);
        menu.AddCurrency(worth);
        //Debug.Log("grabbed");
        //throw new System.NotImplementedException();
    }

    public void OnSpawn(Vector2 pos,int amount)
    {
        transform.position = new Vector2(Random.Range(-spread + pos.x, spread + pos.x), Random.Range(-spread + pos.y,spread + pos.y));
        worth = amount;
        float scale = 1 + sizeIncrease * worth;
        transform.localScale = new Vector2(scale,scale);
        //throw new System.NotImplementedException();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        menu = GameObject.Find("weaponMenuManager").GetComponent<UpgradeMenu>();
    }

}
