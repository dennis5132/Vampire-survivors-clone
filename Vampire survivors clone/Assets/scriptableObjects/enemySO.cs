using UnityEngine;

[CreateAssetMenu(fileName = "enemySO", menuName = "Scriptable Objects/enemySO")]
public class enemySO : ScriptableObject
{
    public string entityName;
    public float damage;
    public float health;
    public float speed;

    public float spawnTime;
    public int dropRatio;
    public GameObject Object;
    public float powerIncrease;

    public Color Color;
}
