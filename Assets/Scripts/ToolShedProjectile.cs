using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolShedProjectile : Projectile
{
    string[] blockade = { "Wall", "Player" };

    protected override void Update()
    {
        base.Update();
    }

    protected override void movementPerFrame()
    {
        transform.position += transform.up * Time.deltaTime * base.speed;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.tag;
        if (tag == "Player")
        {
            collision.gameObject.GetComponent<BasePlayer>().ReduceHealth(damage);
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
