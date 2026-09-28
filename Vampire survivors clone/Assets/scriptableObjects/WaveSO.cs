using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveSO", menuName = "Scriptable Objects/WaveSO")]
public class WaveSO : ScriptableObject
{
    public float startTime = 1;
    public List<WaveEnemy> enemies = new List<WaveEnemy>();
    public float spawnSpeed=1;
}
