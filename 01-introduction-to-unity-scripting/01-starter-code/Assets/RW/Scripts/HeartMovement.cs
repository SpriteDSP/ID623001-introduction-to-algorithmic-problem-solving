using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartMovement : MonoBehaviour
{
    public float rotationSpeed;
    public float ascendSpeed;

//  public float expandSpeed;
//  public float maxScale;
//  private Vector3 initialScale;

    void Update()
    {
        // Similar to how the HayBale rotates and moves, I used Space.Self and Space.World
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime, Space.Self);

        transform.Translate(Vector3.up * ascendSpeed * Time.deltaTime, Space.World);
    }
//    {
//
//    }
}

//  I wanted to have the heart expand in scale but wasn't sure how to achieve this