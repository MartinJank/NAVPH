using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartNewGame() {
        Debug.Log("Starting");
        LevelCounter.control.level = 3;
        SceneManager.LoadScene("GameScene");
    }

    public void ContinueGame() {
        Debug.Log("Continue");
        SceneManager.LoadScene("GameScene");
    }

    public void StopGame() {
        Application.Quit();
    }

    public void BackToMenu() {
        SceneManager.LoadScene("MenuScene");
    }
}
