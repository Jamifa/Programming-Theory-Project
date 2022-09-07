using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySwordsman : Enemy //INHERITANCE
{
    protected override void Move() { //POLYMORPHISM
        float playerXDirection = Player.instance.transform.position.x - transform.position.x;
        if (playerXDirection > 0) {
            playerXDirection = 1;
        }
        else {
            playerXDirection = -1;
        }
        Vector3 playerDirection = new Vector3 (playerXDirection, 0, 0);
        transform.Translate (playerDirection * moveSpeed);
    }
}
