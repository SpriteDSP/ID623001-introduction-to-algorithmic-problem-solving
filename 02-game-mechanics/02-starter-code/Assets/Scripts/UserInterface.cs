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

    public string tagFilter;

    private void Awake()
    {
        GameManager.Instance.OnGoldSet.AddListener(HandleGoldSet);
        GameManager.Instance.OnHealthSet.AddListener(HandleHealthSet);
        EnemySpawner.OnWaveStarted.AddListener(HandleWaveStarted);
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

    private void OnTriggerEnter(Collider other)
    {
        // If enemy collides with cookie
        if (other.CompareTag("Enemy"))
        {
            GameManager.Instance.Health -= 1;
        }
    }
}
