using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    public int curHealth = 0;
    public int maxHealth = 3;
    private float invulnTime = 2f; // seconds
    public float curTime;
    public bool invuln;
    private SpriteRenderer sr;
    
    void Start()
    {
        curHealth = maxHealth;
        curTime = 0f;
        invuln = false;
        sr = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("k")) {
            if (curHealth > 0) {
                invuln = false;
                DamagePlayer(1);
            }
        }
        if ((invuln == true) && (Time.time >= (curTime + invulnTime))) {
            invuln = false;
        }
    }

    public void DamagePlayer(int damage) {
        if (!invuln) {
            // Debug.Log("Took damage at: " + Time.time);
            curTime = Time.time;

            curHealth -= damage;

            GetComponent<stolen>().Damaged();
            invuln = true;
            if (curHealth <= 0) {
                GetComponent<stolen>().Die();
            }
            else StartCoroutine(Flashies());
        }
    }

    public void HealPlayer(int healing) {
        curHealth += healing;
    }

    IEnumerator Flashies() {
        for (int i = 0; i < 6; ++i) {
            sr.enabled = !sr.enabled;
            yield return new WaitForSeconds(invulnTime/6);
            if (curHealth <= 0) {
                sr.enabled = true;
                break;
            }
        }
    }

    
}
