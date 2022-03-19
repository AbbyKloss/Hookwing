using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject deathMenuUI;
    public bool isDead;
    public bool pubPaused;
    
    public void Arise() {
        deathMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        pubPaused = true;
        isDead = GameObject.FindGameObjectWithTag("Player").GetComponent<stolen>().playerDead;
    }
    
    public void LoadMenu() {
        Time.timeScale = 1f;
        isPaused = false;
        pubPaused = isPaused;
        SceneManager.LoadScene("Main Menu");
    }

    public void QuitGame() {
        Debug.Log("Quitting game...");
        Application.Quit();
    }

    public void Retry() {
        Time.timeScale = 1f;
        isPaused = false;
        pubPaused = isPaused;
        deathMenuUI.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
