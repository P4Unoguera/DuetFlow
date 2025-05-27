using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// PianoKey method that reacts to player collisions
public class PianoKey : MonoBehaviour
{
    // Colors used to visually indicate when a key is pressed by Player1 or Player2
    private Color pressedColor1 = Color.blue;
    private Color pressedColor2 = Color.red;

    // Amount the key visually depresses when pressed
    public float pressDepth = 0.0f;
    // Duration for audio fade-out effect when the key is released
    public float fadeOutDuration = 0.5f;

    // Internal state to track the key's original position and color
    private Vector3 originalPosition;
    private Color originalColor;

    // Reference to the key's Renderer component
    private Renderer rend;

    // Reference to the key's AudioSource component
    private AudioSource audioSource;
    // Coroutine used to fade out audio when key is released
    private Coroutine fadeCoroutine;

    // Identifier to distinguish different keys
    public string keyIndex;

    // Start method is called once when the script is initialized
    void Start()
    {
        // Initialization of key properties and cached components
        originalPosition = transform.localPosition;
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;
        audioSource = GetComponent<AudioSource>();
    }

    // OnTriggerEnter is automatically called when another collider enters this trigger
    void OnTriggerEnter(Collider other)
    {
        // If touched by Player1 and key belongs to Piano1...
        if (other.CompareTag("Player1") && transform.parent.tag == "Piano1")
        {
            PressKey1(); // Move key down and change color to blue
            if (audioSource != null)// && !audioSource.isPlaying)
            {
                // audioSource.volume = 1;
                audioSource.Play(); // Play sound
            }
        }
        // If touched by Player2 and key belongs to Piano2...
        if (other.CompareTag("Player2") && transform.parent.tag == "Piano2")
        {
            PressKey2(); // Move key down and change color to red
            if (audioSource != null)// && !audioSource.isPlaying)
            {
                // audioSource.volume = 1;
                audioSource.Play(); // Play sound
            }
        }
    }

    // OnTriggerExit is called when another collider exits this trigger
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player1"))
        {
            ReleaseKey(); // Move key back to original position and reset color
            // Start fading out the sound if it is still playing
            if (audioSource != null && audioSource.isPlaying)
            {
                fadeCoroutine = StartCoroutine(FadeOut(audioSource, fadeOutDuration));
            }
        }

        if (other.CompareTag("Player2"))
        {
            ReleaseKey();
            if (audioSource != null && audioSource.isPlaying)
            {
                fadeCoroutine = StartCoroutine(FadeOut(audioSource, fadeOutDuration));
            }
        }
    }

    // Press action by Player1
    void PressKey1()
    {
        transform.localPosition = originalPosition - new Vector3(0, pressDepth, 0);
        rend.material.color = pressedColor1;
    }

    // Press action by Player2
    void PressKey2()
    {
        transform.localPosition = originalPosition - new Vector3(0, pressDepth, 0);
        rend.material.color = pressedColor2;
    }

    // Reset key to unpressed state
    void ReleaseKey()
    {
        transform.localPosition = originalPosition;
        rend.material.color = originalColor;
    }

    // Coroutine to smoothly fade out the sound over a specified duration
    IEnumerator FadeOut(AudioSource source, float duration)
    {
        float startVolume = source.volume;

        while (source.volume > 0)
        {
            source.volume -= startVolume * Time.deltaTime / duration;
            yield return null;
        }

        source.Stop();
        source.volume = startVolume; // Reset volume for next playback
    }
}
