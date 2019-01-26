using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour {

    [SerializeField]
    float damage;
    [SerializeField]
    float speed;

	// Update is called once per frame
	void Update () {
        movementPerFrame();
    }

    protected abstract void movementPerFrame();
}
