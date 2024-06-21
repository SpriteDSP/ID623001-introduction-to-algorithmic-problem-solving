/// <remarks>
/// Bugs: None known at this time.
/// </remarks>
// <summary>
/// This class manages all sound effects, other classes will instance this class in order to play sounds
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance;

    public Transform Camera;

    // note: i learnt that audioclips are not the same audio sources
    public AudioClip ShootSFX;
    public AudioClip SheepHitSFX;
    public AudioClip SheepDropSFX;

    private void Awake()
    {
        Instance = this;
    }
}

