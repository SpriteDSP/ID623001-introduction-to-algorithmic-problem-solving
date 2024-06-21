/// <remarks>
/// Bugs: None known at this time.
/// </remarks>
// <summary>
/// This simple class will rotate a windmill wheel when applied to said gameobject, speed variable can be changed in inspector
/// </summary>

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
