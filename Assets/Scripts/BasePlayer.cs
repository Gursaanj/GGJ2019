using System.Collections;
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

    private bool _MeleeCooldown = false;
    public float _meleeCooldownTime = 5.0f;
    private bool _RangeCooldown = false;
    public float _RangeCooldownTime = 5.0f;
    private bool _DodgeCooldown = false;
    public float _DodgeCooldownTime = 5.0f;
    private Rigidbody2D  playerRigidbody;


    // Use this for initialization
    void Start () {
        _maxHealth = _health;
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 direction = InputManager.MainInput(); //Get input
        Move();
        Shoot();
        Melee();
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
}
