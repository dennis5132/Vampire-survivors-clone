using UnityEngine;
using static Interfaces;

public class UpgradeCurrency : MonoBehaviour, IGrabbable
{
    [SerializeField] private float spread = 0;
    private DataManager manager;
    public void OnGrab()
    {
        manager.IncreaseCurrency();
        Debug.Log("grabbed");
        PoolManager.instance.EnqueuPooledItem(gameObject);
    }

    public void OnSpawn(Vector2 pos,int amount)
    {
        transform.position = new Vector2(Random.Range(-spread + pos.x, spread + pos.x), Random.Range(-spread + pos.y, spread + pos.y));
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        manager = GameObject.Find("dataManager").GetComponent<DataManager>();
    }
}
