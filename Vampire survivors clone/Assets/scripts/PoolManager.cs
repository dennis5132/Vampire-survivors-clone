using System.Collections.Generic;
using UnityEngine;
using static Interfaces;

public class PoolManager : MonoBehaviour
{
    public static PoolManager instance;
    public List<ObjectPoolItem> poolItems = new List<ObjectPoolItem>();
    public Dictionary<string, Queue<GameObject>> objectsDictionary;

    
    private void Awake()
    {
        instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //spawn alle enemyPoolItems
            objectsDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (ObjectPoolItem item in poolItems)
        {
            Queue<GameObject> queue = new Queue<GameObject>();
            for (int i = 0; i < item.amount; i++) 
            { 
                GameObject newEnemy = Instantiate(item.obj);
                //newEnemy.GetComponent<IEnemyable>().applySO(item);
                newEnemy.SetActive(false);
                newEnemy.transform.parent = item.parent;
                queue.Enqueue(newEnemy);
            }
                objectsDictionary.Add(item.name, queue);
        }
    }
    public GameObject DeQueuPooledItem(string item)
    {
        if (!objectsDictionary.ContainsKey(item))
        {
            return null;
        }

        GameObject newEnemy = objectsDictionary[item].Dequeue();
        if (newEnemy.activeSelf == true) { Debug.LogError($"more of type {item} needed"); }
        newEnemy.SetActive(true);
        objectsDictionary[item].Enqueue(newEnemy);
        return newEnemy;
    }
    public void EnqueuPooledItem(GameObject obj)
    {
        obj.SetActive(false);
    }
    
}
[System.Serializable]
public class ObjectPoolItem
{
    public string name;
    public GameObject obj;
    public int amount;
    public Transform parent;
}
