using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Function to start the game by loading the next scene
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
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

