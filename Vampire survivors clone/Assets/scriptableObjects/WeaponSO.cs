using UnityEngine;

[CreateAssetMenu(fileName = "WeaponSO", menuName = "Scriptable Objects/WeaponSO")]
public class WeaponSO : ScriptableObject
{
    public string thisName;
    public float attackPower;
    public float attackSpeed;
    public float range;
    public float distance;

    public float powerMod;
    public float speedMod;
    public float rangeMod;
    public float distanceMod;
}
