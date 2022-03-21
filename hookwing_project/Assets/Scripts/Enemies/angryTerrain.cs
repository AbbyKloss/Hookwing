using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class angryTerrain : MonoBehaviour
{
    [SerializeField] private int damageDealt = 1;
    [SerializeField] private bool invuln = true;
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.transform.tag == "Player") {
            collision.GetComponent<PlayerHealth>().DamagePlayer(damageDealt, invuln);
        }
    }
}
