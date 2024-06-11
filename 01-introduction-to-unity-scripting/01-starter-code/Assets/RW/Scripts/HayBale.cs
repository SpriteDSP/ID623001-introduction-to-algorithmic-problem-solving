using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HayBale : MonoBehaviour
{
    public Vector3 MovementSpeed = new Vector3(0f, 0f, 10f);
    public Vector3 RotationSpeed = new Vector3(0f, 90f, 0f);


    private void Update()
    {
        //added Space.World
        transform.Translate(MovementSpeed * Time.deltaTime, Space.World);

        //added Space.Self to be able to rotate the object whilst it moves forward on a axis without curving a direction
        transform.Rotate(RotationSpeed * Time.deltaTime, Space.Self);
    }
}
