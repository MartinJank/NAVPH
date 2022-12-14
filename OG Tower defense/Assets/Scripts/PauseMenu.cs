using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;

    private void CloseMenu() {
        pauseMenu.SetActive(false);
    }

    private void Toggle() {
        pauseMenu.SetActive(!pauseMenu.activeSelf);

        if (pauseMenu.activeSelf) {
            Time.timeScale = 0f;
        } else {
            Time.timeScale = 1f;
        }
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)){
            Toggle();
        }
    }
}
