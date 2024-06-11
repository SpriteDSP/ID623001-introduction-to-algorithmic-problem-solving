using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance;

    public Transform Camera;
    public AudioClip ShootSFX;
    public AudioClip SheepHitSFX;
    public AudioClip SheepDropSFX;

    private void Awake()
    {
        Instance = this;
    }
}

