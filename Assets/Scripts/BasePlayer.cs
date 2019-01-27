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
    void Start () {
        _maxHealth = _health;
        characterRigidBody = GetComponent<Rigidbody2D>();
        characterRigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
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

    protected abstract void updateHealthbar();

    protected abstract void onDeath();
}
