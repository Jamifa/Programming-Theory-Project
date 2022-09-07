using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToughEnemy : Enemy //INHERITANCE
{
    private void Start() {
        health = 3;
        attackCooldown = 1.5f;
    }

    protected override void Attack() { //POLYMORPHISM
        Player.instance.health -= 10; //remove another 10 health
        base.Attack ();
    }

    protected override void Move() {
        float playerXDirection = Player.instance.transform.position.x - transform.position.x;
        if(playerXDirection > 0.5f) {
            playerXDirection = 1;
        } else if(playerXDirection < -0.5f) {
            playerXDirection = -1;
        } else {
            playerXDirection = 0;
        }
        Vector3 playerDirection = new Vector3 (playerXDirection, 0, 0);
        transform.Translate (playerDirection * moveSpeed);
    }
}
