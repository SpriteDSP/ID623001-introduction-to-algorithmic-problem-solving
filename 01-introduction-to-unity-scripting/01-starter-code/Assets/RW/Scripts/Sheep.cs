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

        // Previous method below
        //GameObject SFXManagerObject = GameObject.Find("SFXManager"); // Finds the gameobject named SFXManager
        //SFXManager sfxManager = SFXManagerObject.GetComponent<SFXManager>(); // turns the gameobject "SFXManager" into a sfxmanager and reads all the components inside
        //sfxManager.SheepHitSFX.Play(); // plays the sound effect from the sfxmanager // line doesnt work

        AudioSource.PlayClipAtPoint(SFXManager.Instance.SheepHitSFX, transform.position);
        SpawnHeart();

        //with instances again, im calling a function from the gamemanager class
        GameManager.Instance.SaveSheep();
    }

    private void Drop()
    {
        dropped = true;
        myRigidbody.isKinematic = false;
        myCollider.isTrigger = false;
        Destroy(gameObject, dropDestroyDelay);

        AudioSource.PlayClipAtPoint(SFXManager.Instance.SheepDropSFX, transform.position);

        OnDropped?.Invoke(this);

        //instance that gives info for the heart icons in the gamemanager
        GameManager.Instance.LoseLife();
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