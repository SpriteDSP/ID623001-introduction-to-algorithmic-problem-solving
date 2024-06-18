using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindmillRotation : MonoBehaviour
{
    public GameObject wheel;
    public float rotationSpeed;

    void Update()
    {
        wheel.transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
