/// <remarks>
/// Bugs: None known at this time.
/// </remarks>
// <summary>
/// This class is in charge of the ui and the health properties, it collects info about sheeps saved and lost lives to represent onto the ui
/// upon losing all lives the gameover ui is called which the buttons controlled through the buttonmanager class is available to press
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    // lets us reference this instance from other script
    public static GameManager Instance;

    // tracks how many sheep we've saved
    [HideInInspector]
    public int sheepSaved;
    public TMP_Text scoreText;

    // array to hold references to heart images
    public Image[] heartImages;
    public int lives = 3;

    public GameObject gameOverScreen;

    //starter code below is no longer needed as ive done the same thing but slightly different
    //[HideInInspector]
    //public int sheepDropped;
    //public int sheepDroppedBeforeGameOver;

    public void Awake()
    {
        Instance = this;
        UpdateScoreText(); // initializes the score text at the start of the game
    }

    public void SaveSheep()
    {
        // in the sheep script, whenever a sheep is hit by hay, this function is called
        // increases the number of sheep saved by 1
        sheepSaved++;
        UpdateScoreText();
    }

    private void GameOver()
    {
        // destroys all the sheep currently in the level
        Sheep[] allSheep = FindObjectsOfType<Sheep>();
        foreach (Sheep sheep in allSheep)
        {
            Destroy(sheep.gameObject);
        }

        // stops the sheep manager from spawning more sheep
        SheepManager.Instance.StopSpawningSheep();

        // displays the gameover screen
        gameOverScreen.SetActive(true);
    }

    public void LoseLife()
    {
        // decrease the number of lives by 1
        if (lives > 0)
        {
            lives--;
            UpdateHeartImages();
        }

        // if lives reach 0, calls gameover
        if (lives == 0)
        {
            GameOver();
        }
    }

    private void UpdateHeartImages()
    {
        // loop through all heart images
        for (int i = 0; i < heartImages.Length; i++)
        {
            // if the index is less than the current lives, enable the heart image, otherwise disable it
            heartImages[i].enabled = i < lives;
        }
    }

    private void UpdateScoreText()
    {
        // Update the score text to show the current number of sheep saved.
        scoreText.text = "Sheep Saved: " + sheepSaved.ToString();
    }
}
