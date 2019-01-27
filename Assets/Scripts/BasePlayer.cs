using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public abstract class BasePlayer : MonoBehaviour {
    [SerializeField]
    protected float _speed;

    [SerializeField]
    protected float _health = 100;
    protected float _maxHealth;

    protected Rigidbody2D  characterRigidBody;

    // Use this for initialization
    protected virtual void Awake () {
        characterRigidBody = GetComponent<Rigidbody2D>();
        characterRigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
	}

    protected virtual void onStart()
    {
        _maxHealth = _health;
    }


    public void ReduceHealth(float damage)
    {
        _health = Mathf.Max(0, _health - damage);
        updateHealthbar();
        Debug.Log("Health :" + _health);
        if (_health <= 0)
        {
            onDeath();
        }
    }

    protected abstract void updateHealthbar();

    protected abstract void onDeath();
}
