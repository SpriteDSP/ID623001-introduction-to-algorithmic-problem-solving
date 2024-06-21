using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public float MoveSpeed = 5;

    //array of waypoints for the enemy
    public Transform[] waypoints;
    private int currentWaypointIndex;

    //used for rotation
    private Vector3 lastPosition;
    [SerializeField] private GameObject body;

    //damage number that affects the player health state
    public float damage = 1f;

    //event 
    public UnityEvent<Enemy> EnemyDefeated = new UnityEvent<Enemy>();

    private void Awake()
    {
        lastPosition = transform.position;
    }

    private void Update()
    {
        if (currentWaypointIndex == waypoints.Length)
        {
            // enemy reached the final waypoint, reduce player's health
            GameManager.Instance.Health -= (int)damage;

            // loads a method that will destroy and invoke an event to inform the enemyspawner
            OnDestroy();
            return;
        }

        Transform toWaypoint = waypoints[currentWaypointIndex];
        Vector2 moveVector = Vector2.MoveTowards(transform.position, toWaypoint.position, MoveSpeed * Time.deltaTime);
        transform.position = (Vector3)moveVector;

        if (Vector2.Distance(transform.position, toWaypoint.position) <= float.Epsilon)
        {
            currentWaypointIndex++;
        }

        RotateIntoMoveDirection();
        lastPosition = transform.position;
    }

    private void RotateIntoMoveDirection()
    {
        Vector2 newDirection = (transform.position - lastPosition);
        body.transform.right = newDirection;
    }
    private void OnDestroy()
    {
        EnemyDefeated?.Invoke(this);
        Destroy(gameObject);
    }
}
