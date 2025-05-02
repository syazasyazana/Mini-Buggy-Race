using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Chooselevel : MonoBehaviour
{
    // Function to load Easy level (Scene 3)
    public void PlayEasy()
    {
        SceneManager.LoadSceneAsync(2);
    }

    // Function to load Medium level (Scene 4)
    public void PlayMedium()
    {
        SceneManager.LoadSceneAsync(3);
    }

    // Function to load Hard level (Scene 5)
    public void PlayHard()
    {
        SceneManager.LoadSceneAsync(4);
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

