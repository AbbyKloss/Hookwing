using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    public int curHealth = 0;
    public int maxHealth = 3;
    public float invulnTime = 3f; // seconds
    public float curTime;
    public bool invuln;

    public HealthBar healthBar;
    
    void Start()
    {
        curHealth = maxHealth;
        curTime = 0f;
        invuln = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("k")) {
            DamagePlayer(1);
        }
        if ((invuln == true) && (Time.time >= (curTime + invulnTime))) {
            invuln = false;
        }
    }

    public void DamagePlayer(int damage) {
        if (!invuln) {
            Debug.Log("Took damage at: " + Time.time);
            curTime = Time.time;

            curHealth -= damage;

            healthBar.SetHealth(curHealth);
            GetComponent<stolen>().Damaged();
            invuln = true;
        }
    }
}
