using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseController : BaseEnemyController {

    private void FixedUpdate()
    {
        Vector3 destination = new Vector3(player.transform.position.x, transform.position.y);
        transform.position = Vector3.MoveTowards(transform.position, destination, _speed * Time.fixedDeltaTime);
    }
}
