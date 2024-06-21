/// <remarks>
/// Bugs: None known at this time.
/// </remarks>
// <summary>
/// This class controls scene management as the methods can be assigned to buttons
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadTitle()
    {
        SceneManager.LoadScene("Title");
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }
}
