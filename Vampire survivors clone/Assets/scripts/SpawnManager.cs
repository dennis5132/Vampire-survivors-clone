using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using static Interfaces;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private PlayerMovement player;
    [SerializeField] private GameObject levelCurrency;

    public Vector2 spawnArea;
    public Vector2 IgnoreArea;

    [SerializeField] private GameObject enemy;
    [SerializeField] private WaveSO[] waves;

    [SerializeField] private float restart;
    [SerializeField] private int waveTimer;
    private bool[] wavesDone;
    public GameObject activeEnemies;
    private int waveAmount = 1;

    //public System.Random rnd = new System.Random();

    private void Start()
    {
        wavesDone = new bool[waves.Length];
        ResetWaves();
    }
    private void ResetWaves()
    {
        Debug.Log(waveAmount+"wave repeats");
        waveTimer = 0;
        for (int i = 0; i < wavesDone.Length; i++)
        {
            wavesDone[i] = false;
        }
    }

    private void FixedUpdate()
    {
        waveTimer++;
        int z = 0;
        foreach (WaveSO wave in waves)
        {
            if (wave.startTime < waveTimer && wavesDone[z]==false)
            {
                Debug.Log($"wave {z+1} start");
                wavesDone[z] = true;
                int j = 0;
                foreach (WaveEnemy waveEnemy in wave.enemies)
                {
                    for (int i = 0; i < waveEnemy.count; i++)
                    {
                        j++;
                        StartCoroutine(SpawnEnemy(waveEnemy, wave.spawnSpeed * j));
                    }
                }
                StartCoroutine(EndWave(wave.spawnSpeed * j, z));
            }
            z++;
        }
        if (waveTimer > restart)
        {
            int amount = 0;
            for (int i = 0;i < activeEnemies.transform.childCount; i++)
            {
                if (activeEnemies.transform.GetChild(i).gameObject.activeSelf)
                {
                    amount++;
                }
            }
            if (amount == 0 && wavesDone[wavesDone.Length-1])
            {
                waveAmount++;
                ResetWaves();
            }
        }
    }
    IEnumerator EndWave(float delay, int wave)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log($"wave {wave + 1} ended at {waveTimer}");
    }
    IEnumerator SpawnEnemy(WaveEnemy whichOne, float delay)
    {
        yield return new WaitForSeconds(delay);
        var loc = player.rb.position;
        Vector2 spawnLocation= new Vector2();
        while (Mathf.Abs(spawnLocation.x) < Mathf.Abs(IgnoreArea.x) && Mathf.Abs(spawnLocation.y) < Mathf.Abs(IgnoreArea.y))
        {
            spawnLocation = new Vector2(UnityEngine.Random.Range(-spawnArea.x , spawnArea.x ), UnityEngine.Random.Range(-spawnArea.y, spawnArea.y));
        }
        //GameObject newEnemy = Instantiate(whichOne.enemy);//
        GameObject newEnemy = PoolManager.instance.DeQueuPooledItem(whichOne.enemy.name);
        if (newEnemy == null) { Debug.LogError(whichOne.enemyData.prefabName+"was not pooled"); }
        var x = newEnemy.GetComponent<IEnemyable>();
        if (x != null)
        {
            x.applySO(whichOne.enemyData, waveAmount);
        }
        newEnemy.transform.position = spawnLocation+loc;
    }
    private void Update()
    {
        
    }
}
public class Wave
{
    public float startTime { get; set; }
    public float spawnSpeed { get; set; }
    public WaveEnemy enemyGroup { get; set; }

}
[Serializable]
public class WaveEnemy
{
    public GameObject enemy;
    public enemySO enemyData;
    public int count;
}
