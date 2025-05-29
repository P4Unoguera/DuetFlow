using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// TitleManager class
public class TitleManager : MonoBehaviour
{
    // This method will be called to start the Competition Mode
    public void CompetitionMode()
    {
        // Load the scene named "Competition Mode"
        SceneManager.LoadScene("Competition Mode");
    }

    // This method will be called to quit the game
    public void QuitGame()
    {
        // Quits the application (only works in a built game, not in the editor)
        Application.Quit();
        // Log a message to the console
        Debug.Log("Game Quit");
    }
}
