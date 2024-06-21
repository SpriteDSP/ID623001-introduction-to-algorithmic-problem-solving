using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    // this float was before shotcooldown wasnt affected by monster levels
    //public float shotCooldown;
    private float lastShotTime;

    // enemies list
    private List<GameObject> enemiesInRange = new List<GameObject>();

    public GameObject bulletPrefab;

    //reference to monster data
    private MonsterData monsterData;

    //reference to audio source
    private AudioSource laserSFX;

    private void Start()
    {
        monsterData = GetComponent<MonsterData>(); // gets the monsterdata component

        //gets the audiosource component attached to gameobject
        laserSFX = GetComponent<AudioSource>();
    }

    public void Update()
    {
        //checks the monsterdata and its current levels info such as shootcooldown
        float shootCooldown = monsterData.GetShootCooldown();

        //checks if it can shoot under circumstances
        if (Time.time - lastShotTime >= shootCooldown)
        {
            //checks if there are enemies in range and shoots at the first available one
            foreach (GameObject enemy in enemiesInRange)
            {
                Shoot(enemy);
                lastShotTime = Time.time; // updates lastShotTime to current time
                RotateTowardsEnemy(enemy.transform);

                return; // Exit the loop after shooting one enemy
            }
        }
    }

    public void Shoot(GameObject target)
    {
        // instantiates a bullet
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        // play sound effect
        laserSFX.Play();
    }
    private void RotateTowardsEnemy(Transform enemy)
    {
        Vector3 direction = (enemy.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 180 //previously was facing wrong way
        ));
    }

    // when an enemy tag enters the trigger, it will add to the list
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            GameObject enemy = collision.gameObject;
            if (!enemiesInRange.Contains(enemy))
            {
                enemiesInRange.Add(enemy);
            }
        }
    }

    // vise versa
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            GameObject enemy = collision.gameObject;
            if (enemiesInRange.Contains(enemy))
            {
                enemiesInRange.Remove(enemy);
            }
        }
    }
}
