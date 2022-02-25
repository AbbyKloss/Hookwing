using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    public int curHealth = 0;
    public int maxHealth = 100;

    public HealthBar healthBar;
    
    void Start()
    {
        curHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("k")) {
            DamagePlayer(10);
        }
    }

    public void DamagePlayer(int damage) {
        curHealth -= damage;

        healthBar.SetHealth(curHealth);
    }
}
