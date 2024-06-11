using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Sheep : MonoBehaviour
{
    public float runSpeed;
    public float dropDestroyDelay;
    private bool dropped;
    private Collider myCollider;
    private Rigidbody myRigidbody;

    // Added events
    public class SheepEvent : UnityEvent<Sheep> { }
    public SheepEvent OnAteHay = new SheepEvent();
    public SheepEvent OnDropped = new SheepEvent();

    // Heart prefab and customizable float for how long the heart lingers
    public GameObject heartPrefab;
    public float heartDestroyDelay;

    private void Awake()
    {
        myCollider = GetComponent<BoxCollider>();
        myRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * runSpeed * Time.deltaTime);
    }

    private void HitByHay()
    {
        Destroy(gameObject);

        OnAteHay?.Invoke(this);

        

        SpawnHeart();
    }

    private void Drop()
    {
        dropped = true;
        myRigidbody.isKinematic = false;
        myCollider.isTrigger = false;
        Destroy(gameObject, dropDestroyDelay);

        OnDropped?.Invoke(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hay"))
        {
            Destroy(other.gameObject);
            HitByHay();
        }

        else if (other.CompareTag("DropSheep") && !dropped)
        {
            Drop();
        }
    }

    private void SpawnHeart()
    {
        // This will get the prefabs rotation, in a similar way to the rotation issue of the sheeps spawn rotation in the SheepManager script
        Quaternion heartRotation = heartPrefab.transform.rotation;

        // This will instantiate a heart when the sheep is hit by hay
        GameObject heart = Instantiate(heartPrefab, transform.position, heartRotation);

        Destroy(heart, heartDestroyDelay);
    }
}