using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Interfaces;

public class UniqueEnemyA : MonoBehaviour, IEnemyable
{
    public enemySO data;

    [SerializeField] private int enemyAmount = 5;
    [SerializeField] private float radius = 5;
    private int wav;

    private List<GameObject> enemies = new List<GameObject>();
    private int liveTime = 50;

    public void applySO(enemySO SO, int wave)
    {
        data = SO;
        wav = wave;
        Begin();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Begin()
    {
        Transform player = GameObject.Find("Player").GetComponent<Transform>();
        for (int i = 0; i <= enemyAmount; i++) 
        {
            //spawn enemies[enemyAmount] in circle
            float circleposition = i / (float)enemyAmount;
            float x = Mathf.Sin(circleposition * Mathf.PI * 2.0f) * radius;
            float z = Mathf.Cos(circleposition * Mathf.PI * 2.0f) * radius;

            GameObject newEnemy = PoolManager.instance.DeQueuPooledItem("enemyAInstance");
            newEnemy.GetComponent<IEnemyable>().applySO(data, wav);
            newEnemy.transform.position = new Vector3(x, z, 0) + player.transform.position;
            enemies.Add(newEnemy);
        }
        Debug.Log("enemyA Spawned");
        StartCoroutine(endThem());
    }
    IEnumerator endThem()
    {
        yield return new WaitForSeconds(liveTime);
        int x = enemies.Count;
        for (int i = 0; i < enemies.Count; i++)
        {
            Destroy(enemies[i]);
        }
        enemies.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
