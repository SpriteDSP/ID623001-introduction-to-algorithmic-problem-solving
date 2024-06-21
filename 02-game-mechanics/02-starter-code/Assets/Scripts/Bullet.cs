using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //start with defining damage, speed and a target
    public float damage = 10f;
    public float speed = 5f;

    //public Vector2 target;
    //public GameObject targetPrefab;
    private Transform target;

    void Start()
    {
        //wont work cause i want to attach the target to the enemy prefab
        //target = new Vector2(0, 0);

        //this attempt didnt work either
        //target = targetPrefab.transform;

        //using tags work and makes it super simple
        target = GameObject.FindGameObjectWithTag("Enemy").transform;
    }

    

    void Update()
    {
        //this line was in place before a bullet could be destroyed when its target has already been destroyed
        //transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if (target != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //this manages the collision and damage
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = other.GetComponentInChildren<EnemyHealth>(); //gets reference to enemy health
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage); //calls TakeDamage function on enemyhealth script
            }

            Destroy(gameObject); //destroy bullet after hitting the enemy
        }
    }
}
