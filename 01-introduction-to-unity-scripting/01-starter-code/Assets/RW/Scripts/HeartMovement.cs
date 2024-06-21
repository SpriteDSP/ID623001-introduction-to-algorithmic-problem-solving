using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HeartMovement : MonoBehaviour
{
    //    public float rotationSpeed;
    //    public float ascendSpeed;
    //
    //    void Update()
    //    {
    //        // Similar to how the HayBale rotates and moves, I used Space.Self and Space.World
    //       transform.Rotate(0, 0, rotationSpeed * Time.deltaTime, Space.Self);
    //
    //        transform.Translate(Vector3.up * ascendSpeed * Time.deltaTime, Space.World);
    //    }

    //dotween version

    void Start()
    {
        // makes the heart grow in size and then over shortly after shrink to 0
        transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f).OnComplete(() => { transform.DOScale(Vector3.zero, 0.5f); });

        // rotate
        transform.DORotate(new Vector3(0f, 360f, 0f), 1f, RotateMode.WorldAxisAdd).OnComplete(() => { transform.DORotate(Vector3.zero, 1f); });
    }

}