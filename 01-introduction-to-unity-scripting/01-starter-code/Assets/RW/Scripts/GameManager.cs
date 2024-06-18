using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // lets us reference this instance from other script
    public static GameManager Instance;

    // tracks how many sheep we've saved
    [HideInInspector]
    public int sheepSaved;
    public TMP_Text scoreText;

    // tracks how many sheep have been dropped
    [HideInInspector]
    public int sheepDropped;

    // specifies how many sheep can be dropped before we lose the game
    public int sheepDroppedBeforeGameOver;

    // array to hold references to heart images
    public Image[] heartImages;
    public int lives = 3;

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

        // Stop the sheep manager from spawning more sheep
        SheepManager.Instance.StopSpawningSheep();

        // Optionally, load the Game Over scene or show a game over UI
        // SceneManager.LoadScene("GameOverScene");
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
        // loop through all heart images?
        for (int i = 0; i < heartImages.Length; i++)
        {
            // if the index is less than the current lives, enable the heart image, otherwise disable it
            heartImages[i].enabled = i < lives;
        }
    }

    private void UpdateScoreText()
    {
        // Update the score text to show the current number of sheep saved.
        if (scoreText != null)
        {
            scoreText.text = "Sheep Saved: " + sheepSaved.ToString();
        }
    }
}
