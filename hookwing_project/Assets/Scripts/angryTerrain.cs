using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class angryTerrain : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        Debug.Log(collision.gameObject.name + " : " + gameObject.name + " : " + Time.time);
        if (collision.transform.name == "Player") {
            collision.GetComponent<PlayerHealth>().DamagePlayer(1);
        }
    }
}
