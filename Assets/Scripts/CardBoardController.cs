using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBoardController : BaseEnemyController {

    bool isDead = false;

    private void Awake()
    {
        GetComponent<Animator>().enabled = false;
    }

    protected override void Move()
    { }

    protected override void Melee()
    {
    }

    protected override void Shoot()
    {
    }

    protected override void onDeath()
    {
        GetComponent<Animator>().enabled = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead)
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            base.onDeathDelegate();
        }
    }

    public void onDeathAnimationComplete()
    {
        isDead = true;
    }
}