/// <remarks>
/// Bugs: None known at this time.
/// </remarks>
// <summary>
/// This class manages the ui, such as the status text like gold, health, and waves that listens from the gamemanager or the enemyspawner class
/// it also listens to the gamemanager and enemyspawner to trigger the gameover labels and wave labels
/// </summary>

using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{
    public EnemySpawner EnemySpawner;
    [SerializeField] private Text healthLabel;
    [SerializeField] private Text goldLabel;
    [SerializeField] private Text waveLabel;
    [SerializeField] private Animator topHalfWaveStartLabel;
    [SerializeField] private Animator bottomHalfWaveStartLabel;
    [SerializeField] private Animator gameOverLabel;
    [SerializeField] private Animator gameWonLabel;

    //this was to get the cookie tag however it didnt work as i thought it would
    //public string tagFilter;

    //couldnt get cookie collision to work
    //[SerializeField] private GameObject cookie;

    private void Awake()
    {
        GameManager.Instance.OnGoldSet.AddListener(HandleGoldSet);
        GameManager.Instance.OnHealthSet.AddListener(HandleHealthSet);
        EnemySpawner.OnWaveStarted.AddListener(HandleWaveStarted);
        GameManager.Instance.OnGameOver.AddListener(HandleGameOver);
        GameManager.Instance.OnGameWon.AddListener(HandleGameWon);
    }

    private void HandleHealthSet()
    {
        healthLabel.text = "Health: " + GameManager.Instance.Health.ToString();
    }

    private void HandleGoldSet()
    {
        goldLabel.text = "GOLD: " + GameManager.Instance.Gold.ToString();
    }

    private void HandleWaveStarted()
    {
        waveLabel.text = "WAVE: " + (EnemySpawner.currentWaveIndex + 1).ToString();
        topHalfWaveStartLabel.SetTrigger("nextWave");
        bottomHalfWaveStartLabel.SetTrigger("nextWave");
    }

    private void HandleGameOver()
    {
        gameOverLabel.SetTrigger("gameOver");
    }

    private void HandleGameWon()
    {
        gameWonLabel.SetTrigger("gameWon");
    }

    // wasn't able to get the health to decrease when the enemy collided with the cookie
    // will switch to when the enemy reaches its last waypoint instead
    //
    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    // If enemy collides with cookie
    //    if (other.CompareTag("Enemy"))
    //    {
    //        // Check if the collided object is the cookie
    //        if (other.gameObject == cookie)
    //        {
    //            GameManager.Instance.Health -= 1;
    //        }
    //    }
    //}
}
