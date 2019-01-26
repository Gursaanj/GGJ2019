using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : Projectile {

    string[] blockade = { "Wall", "Enemy" };

    protected override void movementPerFrame()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.tag;
        if (tag == "Enemy")
        {
            //TODO - pass in damage
        }

        disableIfNeeded(tag);
    }

    void disableIfNeeded(string tag)
    {
        for (int i = 0; i < blockade.Length; i++)
        {
            if (tag == blockade[i])
            {
                gameObject.SetActive(false);
            }
        }
    }
}
