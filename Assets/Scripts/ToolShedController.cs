using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolShedController : BasePlayer {

    Vector3 playerDestination;
    bool keepShooting = true;

    private void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) { 
            playerDestination = player.transform.position;
        } else {
            keepShooting = false;
        }

    }


    protected override void Shoot()
    {
        if (!_isRangeOnCoolDown && keepShooting) {

            float xChange = playerDestination.x - transform.position.x;
            float yChange = playerDestination.y - transform.position.y;
            float angle = Mathf.Atan2(yChange, xChange) * Mathf.Rad2Deg;


            ObjectPooler.Instance.SpawnFromPool("EnemyBullet", transform.position, Quaternion.Euler(0, 0, angle - 90));
            _isRangeOnCoolDown = true;
        }
    }

}
