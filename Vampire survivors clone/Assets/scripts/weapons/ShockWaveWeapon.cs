using UnityEngine;
using static Interfaces;

public class ShockWaveWeapon : MonoBehaviour, IWeaponable
{
    public WeaponSO SO;
    [SerializeField] private Transform player;

    //bool isAttacking;
    [SerializeField] private float animationSpeed;
    private float slowdown; // SO.range
    private float currentSpeed;

    [SerializeField] private GameObject wave;
    [SerializeField] private SpriteRenderer spriteR;
    public float visibility;
    public string Name => "shockWave";

    public void DoAttack(int Level)
    {
        transform.position = player.position;
        if (Level == 0) { return; }
        slowdown = SO.range;
        wave.transform.localScale = new Vector2(0, 0);
        animationSpeed = SO.distance;
        currentSpeed = animationSpeed;
        wave.SetActive(true);
    }

    public float GetAttackSpeed()
    {
        return SO.attackSpeed;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        visibility = currentSpeed / animationSpeed;
        if (wave.activeSelf)
        {
            wave.transform.localScale += new Vector3(currentSpeed, currentSpeed, 0);
            currentSpeed -= slowdown;
            spriteR.color = new Color(1f, 1f, 1f, visibility);

            Debug.Log(currentSpeed);
            if (currentSpeed <= 0)
            {
                wave.SetActive(false);
            }
        }
    }
}
