using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadTestLevel() {
        SceneManager.LoadScene("Testing Level");
    }

    public void LoadLevel1() {
        SceneManager.LoadScene("Level1");
    }

    public void LoadLevel2() {
        SceneManager.LoadScene("Level2");
    }

    public void LoadLevel3() {
        SceneManager.LoadScene("Level3");
    }

    public void QuitGame() {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
