using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayer : MonoBehaviour {
    public float _speed;
    public float _health = 100;
    private bool _MeleeCooldown = false;
    public float _meleeCooldownTime = 5.0f;
    private bool _RangeCooldown = false;
    public float _RangeCooldownTime = 5.0f;
    private bool _DodgeCooldown = false;
    public float _DodgeCooldownTime = 5.0f;
    private Rigidbody2D  playerRigidbody;


    // Use this for initialization
    void Start () {
        playerRigidbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        //Vector2 amountToMove;
    
        Vector3 direction = InputManager.MainInput(); //Get input
        Move(direction);
    
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        if (Input.GetMouseButtonDown(1))
        {
            Melee();
        }
    }
    public void ReduceHealth(float damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            onDeath();
        }
    }

    private void onDeath()
    {
        Destroy(gameObject);
    }

    private void Shoot()
    {
      // todo Shoot
    }

    private void Melee()
    {
       // todo melee
    }

    private void Move(Vector3 direction)
    {
        playerRigidbody.velocity = (Vector3.Normalize(direction) * _speed);
    }
}
