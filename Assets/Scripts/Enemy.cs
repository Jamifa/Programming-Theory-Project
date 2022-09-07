using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected bool canAttack = true;
    protected float attackCooldown = 1.0f;
    protected int health = 1;
    [SerializeField] protected float moveSpeed;

    private void FixedUpdate() {
        Move ();
    }
    protected virtual void Attack() {
        Player.instance.health -= 10;
        canAttack = false;
        StartCoroutine (AttackCooldown ());
    }

    protected abstract void Move();

    protected IEnumerator AttackCooldown() { //ABSTRACTION
        yield return new WaitForSeconds (attackCooldown);
        canAttack = true;
    }

    public void ChangeHealth(int amount) {
        health += amount;
        if(health <= 0) {
            GameManager.instance.enemyCount--;
            Destroy (gameObject);
        }
    }

    protected void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.CompareTag("Player") && canAttack) {
            canAttack = false;
            Attack ();
        }
    }
}
