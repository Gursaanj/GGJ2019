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


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 amountToMove;
        if (Input.GetKey(KeyCode.S))
        {
           amountToMove = new Vector2(0, -Time.deltaTime * _speed);
           transform.Translate(amountToMove);
        }
        if (Input.GetKey(KeyCode.W))
        {
            amountToMove = new Vector2(0, Time.deltaTime * _speed);
            transform.Translate(amountToMove);
        }
        if (Input.GetKey(KeyCode.A))
        {
            amountToMove = new Vector2(-Time.deltaTime * _speed,0);
            transform.Translate(amountToMove);
        }
        if (Input.GetKey(KeyCode.D))
        {
            amountToMove = new Vector2(Time.deltaTime * _speed, 0);
            transform.Translate(amountToMove);
        }
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
}
