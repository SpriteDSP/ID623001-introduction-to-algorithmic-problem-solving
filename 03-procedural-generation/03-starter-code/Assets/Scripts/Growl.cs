using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Growl : MonoBehaviour
{
    public float delayBetweenGrowl = 5.0f;  // Delay between loops in seconds
    private AudioSource audioSource;

    void Start()
    {
        // Get the AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();

        // Start the coroutine to loop the audio
        StartCoroutine(LoopAudio());
    }

    IEnumerator LoopAudio()
    {
        while (true)
        {
            // Play the audio
            audioSource.Play();

            // Wait for the audio to finish playing plus the delay
            yield return new WaitForSeconds(audioSource.clip.length + delayBetweenGrowl);
        }
    }
}
