using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame() {
        Debug.Log("Starting");
        SceneManager.LoadScene("GameScene");
    }

    public void StopGame() {
        Application.Quit();
    }
}
