/// <remarks>
/// Bugs: None known at this time.
/// </remarks>
// <summary>
/// This class controls the hay machines movement, the limitations of its movement such as boundaries
/// and its shooting hay mechanic, the fire rate can be changed, upon shooting a sound effect is played through the sfxmanager class
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HayMachine : MonoBehaviour
{
    //    //public Vector3 MovementSpeed = new Vector3(0f, 0f, 10f);
    //
    //    public float movementSpeed = 10f;
    //
    //    void Update()
    //    {
    //        //transform.Translate(MovementSpeed * Time.deltaTime);
    //
    //       float horizontalInput = Input.GetAxisRaw("Horizontal");
    //
    //        if (horizontalInput < 0)
    //        {
    //            transform.Translate(transform.right * -movementSpeed * Time.deltaTime);
    //        }
    //        else if (horizontalInput > 0)
    //        {
    //            transform.Translate(transform.right * movementSpeed * Time.deltaTime);
    //        }
    //    }

    // Above is my attempts, below is the update where boundaries are implemented

    public float movementSpeed = 10f;
    public float horizontalBoundary = 22;

    public GameObject hayBalePrefab;
    public Transform haySpawnpoint;
    public float shootInterval;
    private float shootTimer;


    void Update()
    {
        PerformMovement();
        HandleShooting();
    }

    void PerformMovement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        if (horizontalInput < 0 && transform.position.x > -horizontalBoundary)
        {
            transform.Translate(transform.right * -movementSpeed * Time.deltaTime);
        }
        else if (horizontalInput > 0 && transform.position.x < horizontalBoundary)
        {
            transform.Translate(transform.right * movementSpeed * Time.deltaTime);
        }
    }

    void HandleShooting()
    {
        shootTimer -= Time.deltaTime;

        if (shootTimer <= 0 && Input.GetKey(KeyCode.Space))
        {
            shootTimer = shootInterval;
            ShootHay();
        }
    }

    private void ShootHay()
    {
        Instantiate(hayBalePrefab, haySpawnpoint.position, Quaternion.identity);

        AudioSource.PlayClipAtPoint(SFXManager.Instance.ShootSFX, transform.position);
    }

}
