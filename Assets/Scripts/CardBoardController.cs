using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBoardController : BaseEnemyController {

    public delegate void CardDelegate();
    public CardDelegate onDeathDelegate;


    bool isDead = false;

    protected override void Move(){}

    protected override void Melee(){}

    protected override void Shoot() {}


    protected override void onDeath()
    {
        isDead = true;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead)
        {
            onDeathDelegate();
        }
    }
}