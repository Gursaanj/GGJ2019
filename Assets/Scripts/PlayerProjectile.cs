using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : Projectile {

    string[] blockade = { "Wall", "Enemy" };

    protected override void movementPerFrame()
    {
        
        transform.position += transform.up * Time.deltaTime * base.speed;
        // delete when properly pooled 
        //StartCoroutine(timeToDie(0.5f));
    }

    //IEnumerator timeToDie(float wait)
    //{
    //    yield return new WaitForSeconds(wait);
    //    Destroy(gameObject);
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.tag;
        if (tag == "Enemy")
        {
            //TODO - pass in damage
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
