using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public EnemySpawner EnemySpawner;

    public int StartingGold = 1000;
    public int StartingHealth = 5;
    private int gold;
    private int health;
    public bool gameOver = false;
    public bool gameWon = false;

    public UnityEvent OnHealthSet = new UnityEvent();
    public UnityEvent OnGoldSet = new UnityEvent();
    public UnityEvent OnGameOver = new UnityEvent();
    public UnityEvent OnGameWon = new UnityEvent();

    public AudioSource loseHealth;
    public int Health
    {
        get { return health; }
        set
        {
            if (value < health)
            {
                loseHealth.Play(); // Play the health decrease sound
            }

            health = value;
            OnHealthSet?.Invoke();
            if (health <= 0 && !gameOver && !gameWon)
            {
                OnGameOver?.Invoke();
                gameOver = true;
            }
        }
    }
    public int Gold
    {
        get { return gold; }
        set
        {
            gold = value;
            OnGoldSet?.Invoke();
        }
    }

    public void WinState()
    {
        if (!gameOver && !gameWon)
        {
            gameWon = true;
            OnGameWon?.Invoke();
            // Additional logic for what happens when the game is won
        }
    }

    private void Awake()
    {
        // Just ensures there can only ever be one instance of Game Manager.
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;

        EnemySpawner.OnGameWon.AddListener(WinState);
    }

    private void Start()
    {
        Gold = StartingGold;
        Health = StartingHealth;
    }
}
