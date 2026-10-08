using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static Interfaces;

public class PlayerMovement : MonoBehaviour, IDamageable
{
    [SerializeField] private InputActionAsset action;
    private InputAction m_move;
    private Vector2 moveAmt;
    public Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float HP = 10;
    //private Enum moveDirection

    public int[] weaponLevels = new int[0];
    float[] WeaponTimers= new float[0];
    [SerializeField] private GameObject[] weapons = new GameObject[0];
    public IWeaponable[] weapons_ = new IWeaponable[0];
    [SerializeField] private int startWeapon=0;

    [SerializeField] private TextMeshProUGUI healthTxt;
    
    //public IWeaponable x;
    private void OnEnable()
    {
        action.FindActionMap("Player").Enable();
    }
    private void OnDisable()
    {
        action.FindActionMap("Player").Disable();
    }
    private void Awake()
    {
        List<IWeaponable> w = new List<IWeaponable>();
        foreach (GameObject weapon in weapons) 
        {
            IWeaponable newOne = weapon.GetComponent<IWeaponable>();
            if (newOne != null) 
            {
                w.Add(newOne);
            }
        }
        weapons_ = w.ToArray();

        m_move = InputSystem.actions.FindAction("Move");
        weaponLevels = new int[weapons_.Length];
        WeaponTimers = new float[weaponLevels.Length];
        for (int i = 0; i < weaponLevels.Length; i++)
        {
            weaponLevels[i] = 0;
            WeaponTimers[i] = weapons_[i].GetAttackSpeed();
        }
        weaponLevels[startWeapon]= 1;
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        healthTxt.text = $"health: {(int)HP}";

    }
    private void Update()
    {
        moveAmt = m_move.ReadValue<Vector2>();
    }
    private void FixedUpdate()
    {
        Walking();
        for (int i = 0; i < weapons_.Length; i++) 
        {
            WeaponTimers[i]--;
            if (WeaponTimers[i] <= 0)
            {
                weapons_[i].DoAttack(weaponLevels[i]);
                WeaponTimers[i] = weapons_[i].GetAttackSpeed();
            }
        }
    }
    private void Walking()
    {
        rb.MovePosition(new Vector2(rb.position.x + moveAmt.x * moveSpeed, rb.position.y + moveAmt.y * moveSpeed));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IGrabbable grabbable = collision.GetComponent<IGrabbable>();
        if (grabbable != null) 
        {
            grabbable.OnGrab();
        }
    }
    public void Takedamage(float damage)
    {
        HP -= damage;
        healthTxt.text = $"health: {HP}";
        if (HP < 0) 
        {
            Die();
        }
    }

    public void Takedamage(float damage, Vector2 Knockback)
    {
        throw new NotImplementedException();
    }

    public void Die()
    {
        //SceneManager.LoadScene("MenuScene");
        Application.Quit();//
        throw new NotImplementedException();
    }
}
