using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class angryTerrain : MonoBehaviour
{
    public int damageDealt = 1;
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.transform.tag == "Player") {
            collision.GetComponent<PlayerHealth>().DamagePlayer(damageDealt);
        }
    }
}
