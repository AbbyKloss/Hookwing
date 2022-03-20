using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class egg : MonoBehaviour
{
    private int currentScene;
    private int totalScenes;
    private int nextScene;

    void Start() {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        totalScenes = SceneManager.sceneCountInBuildSettings;
        nextScene = (currentScene + 1) % totalScenes;
        Debug.Log(nextScene);
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.transform.name == "Player") {
            SceneManager.LoadScene(nextScene);
        }
    }
}
