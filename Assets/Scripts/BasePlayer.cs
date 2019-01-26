﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider))]
public class BasePlayer : MonoBehaviour {
    [SerializeField]
    protected float _speed;
    [SerializeField]
    RectTransform healthBar;

    [SerializeField]
    protected float _health = 100;
    private float _maxHealth;

    [Header("Cool Down")]
    [SerializeField]
    float _meleeCooldownTime = 5.0f;
    [SerializeField]
    float _RangeCooldownTime = 5.0f;
    [SerializeField]
    float _DodgeCooldownTime = 5.0f;

    float meleeCurrentCoolDownTime;
    float rangeCurrentCoolDownTime;
    float dodgeCurrentCoolDownTime;

    [Header("Damage")]
    [SerializeField]
    protected float _meleeDamage = 10;

    protected bool _isMeleeOnCoolDown = false;
    protected bool _isRangeOnCoolDown = false;
    protected bool _isDodgeOnCoolDown = false;


    private Rigidbody2D  playerRigidbody;

    // Use this for initialization
    void Start () {
        _maxHealth = _health;
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
	}
	
    void setupCoolDownTime()
    {
        meleeCurrentCoolDownTime = _meleeCooldownTime;
        rangeCurrentCoolDownTime = _RangeCooldownTime;
        dodgeCurrentCoolDownTime = _DodgeCooldownTime;
    }

    // Update is called once per frame
    void FixedUpdate () {
        Move();
        Shoot();
        Melee();
        CoolDown();
    }
    public void ReduceHealth(float damage)
    {
        _health = Mathf.Max(0, _health - damage);
        updateHealthbar();
        if (_health <= 0)
        {
            onDeath();
        }
    }

    void updateHealthbar()
    {
        healthBar.transform.localScale = new Vector3(_health / _maxHealth, 1, 1);
    }

    private void onDeath()
    {
        Destroy(gameObject);
    }

    protected virtual void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //TODO - Shoot function
        }
    }

    protected virtual void Melee()
    {
        if (Input.GetMouseButtonDown(1))
        {
            //TODO Melee function
        }
    }

    protected virtual void Move()
    {
        Vector3 direction = InputManager.MainInput();
        playerRigidbody.velocity = (Vector3.Normalize(direction) * _speed);
    }

    private void CoolDown()
    {
        if (_isMeleeOnCoolDown)
        {
            meleeCurrentCoolDownTime -= Time.fixedDeltaTime;
            if (meleeCurrentCoolDownTime <= 0)
            {
                _isMeleeOnCoolDown = false;
                meleeCurrentCoolDownTime = _meleeCooldownTime;
            }
        }

        if (_isRangeOnCoolDown)
        {
            rangeCurrentCoolDownTime -= Time.fixedDeltaTime;
            if(rangeCurrentCoolDownTime <= 0)
            {
                _isRangeOnCoolDown = false;
                rangeCurrentCoolDownTime = _RangeCooldownTime;
            }
        }

        if (_isDodgeOnCoolDown)
        {
            dodgeCurrentCoolDownTime -= Time.fixedDeltaTime;
            if (dodgeCurrentCoolDownTime <= 0)
            {
                _isDodgeOnCoolDown = false;
                dodgeCurrentCoolDownTime = _DodgeCooldownTime;
            }
        }
    }
}
