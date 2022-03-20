using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heartEgg : MonoBehaviour
{
    public int healing = 1;
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.transform.name == "Player") {
            collision.GetComponent<PlayerHealth>().HealPlayer(healing);
            Destroy(gameObject);
        }
    }
}
