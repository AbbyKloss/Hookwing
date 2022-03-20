using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class angryTerrain : MonoBehaviour
{

    public int damageDealt = 1;
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.transform.name == "Player") {
            collision.GetComponent<PlayerHealth>().DamagePlayer(damageDealt);
        }
    }

    public void takeDamage(int damage) {
        return;
    }
}
