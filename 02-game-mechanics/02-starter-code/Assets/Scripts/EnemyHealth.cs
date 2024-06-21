using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    public static EnemyHealth Instance;

    public float maxHealth = 100;
    public float currentHealth = 100;
    private float originalXScale;

    //the gold awarded can be changed within the unity inspector now
    public int goldReward = 50;

    private GameObject enemyPrefab;
    //creates a variable, open to be assigned
    private AudioSource audioSource;

    //event
    public UnityEvent EnemyDefeated = new UnityEvent();

    private void Start()
    {
        // The health bar takes note of its initial x scale, so that it can
        // rescale itself relative to that initial scale.
        originalXScale = gameObject.transform.localScale.x;

        //this retrieves the grandparent gameobject of where the script is held
        //previously the line only contained parent once, as i didn't realize i had to grab the grandparent
        enemyPrefab = transform.parent.parent.gameObject;

        GameObject enemyManager = GameObject.Find("EnemyManager"); // Finds the gameobject named EnemyManager
        audioSource = enemyManager.GetComponent<AudioSource>(); // gets the audiosource component from said gameobject and stores it into the variable from earlier
    }

    private void Update()
    {
        UpdateHealthBar();
    }
    void UpdateHealthBar()
    {
        // newScale is going to be what we set the scale to. Initially, it's
        // just whatever the current scale is.
        Vector3 newScale = gameObject.transform.localScale;

        //this calculates the health ratio
        float healthRatio = currentHealth / maxHealth;

        //this updates the x value of newScale based on the health ratio
        newScale.x = originalXScale * healthRatio;

        // Apply the new scale to the health bar
        gameObject.transform.localScale = newScale;
    }

    public void TakeDamage(float damage)
    {
        //subtract damage from health
        currentHealth -= damage;

        //this should ensure health doesn't go below 0 or above maxhealth?
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            //Destroy(gameObject);

            //this will destroy the entire gameobject as a whole instead of just the healthbar
            //Destroy(enemyPrefab);


            //this cant be changed in the unity inspector hence the update below
            //GameManager.Instance.Gold += 50;

            //GameManager.Instance.Gold += goldReward;

            //i put all these contents into a new method
            EnemyDefeat();
        }
    }

    public void EnemyDefeat()
    {
        Destroy(enemyPrefab);
        GameManager.Instance.Gold += goldReward;

        //plays audio contained in enemymanager
        audioSource.Play();

        //invokes event to inform the enemyspawner about the enemy being destroyed
        EnemyDefeated?.Invoke();
    }
    private void Awake()
    {
        Instance = this;
    }
}