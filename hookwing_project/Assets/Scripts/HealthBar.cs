using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;
    public PlayerHealth playerHealth;
    public Text textbox;

    private int curhp, hpmax;

    private void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        healthBar = GetComponent<Slider>();
        healthBar.maxValue = playerHealth.maxHealth;
        healthBar.value = playerHealth.maxHealth;
        textbox.text = "" + healthBar.value + "/" + healthBar.maxValue;
    }

    // void Update() {
    //     textbox.text = "" + curhp + "/" + hpmax;
    // }

    public void SetHealth(int hp)
    {
        healthBar.value = hp;
        textbox.text = "" + hp + "/" + healthBar.maxValue;
    }
}