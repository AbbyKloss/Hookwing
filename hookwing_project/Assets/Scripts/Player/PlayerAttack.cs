using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 1.5f;
	public Animator animator;
    public LayerMask enemyLayers;
    public int atkDmg = 1;

    public void Attack() {
        attackPoint.GetComponent<PlayerAttackFX>().Attack();
		animator.SetTrigger("Attack");

		// Debug.Log("Attacked at " + Time.time);
		Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies) {
            if (enemy.GetComponent<patrollingEnemyController>() != null)
                enemy.GetComponent<patrollingEnemyController>().takeDamage(atkDmg);
        }
	}

    void OnDrawGizmosSelected() {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
