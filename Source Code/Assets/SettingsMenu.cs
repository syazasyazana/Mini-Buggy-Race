using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    public GameObject Panel; // Assign the settings UI panel in Unity

    private void Start()
    {
        Panel.SetActive(false); // Ensure the panel is hidden at the start
    }

    // Function to open the settings menu and pause the game
    public void OpenSettings()
    {
        Panel.SetActive(true);
        Time.timeScale = 0f; // Pause the game
    }

    // Function to close the settings menu and resume the game
    public void CloseSettings()
    {
        Panel.SetActive(false);
        Time.timeScale = 1f; // Resume the game
    }

    // Function to load Scene 2 (Choose Level Menu)
    public void ChooseLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(1);
    }

    // Function to restart the current level
    public void RestartGame()
    {
        Time.timeScale = 1f; // Ensure normal game speed before restarting
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    // Function to quit the game
    public void QuitGame()
    {
        Debug.Log("Quitting the game...");
        Application.Quit();

        // If running in the Unity editor, stop play mode
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}

