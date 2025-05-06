using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoKey : MonoBehaviour
{
    public Color pressedColor = Color.gray;
    public float pressDepth = 0.05f;
    public float fadeOutDuration = 0.5f;

    private Vector3 originalPosition;
    private Color originalColor;
    private Renderer rend;
    //private bool isPressed = false;
    private AudioSource audioSource;
    private Coroutine fadeCoroutine;

    void Start()
    {
        originalPosition = transform.localPosition;
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1"))
        {
            PressKey();
            if (audioSource != null && !audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }

    /*void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player1") && !isPressed)
        {
            PressKey();
        }
    }*/

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player1"))
        {
            ReleaseKey();
            if (audioSource != null && audioSource.isPlaying)
            {
                fadeCoroutine = StartCoroutine(FadeOut(audioSource, fadeOutDuration));
            }
        }
    }

    void PressKey()
    {
        //isPressed = true;
        transform.localPosition = originalPosition - new Vector3(0, pressDepth, 0);
        rend.material.color = pressedColor;
    }

    void ReleaseKey()
    {
        //isPressed = false;
        transform.localPosition = originalPosition;
        rend.material.color = originalColor;
    }

    IEnumerator FadeOut(AudioSource source, float duration)
    {
        float startVolume = source.volume;

        while (source.volume > 0)
        {
            source.volume -= startVolume * Time.deltaTime / duration;
            yield return null;
        }

        source.Stop();
        source.volume = startVolume; // restaurar volumen para la próxima vez
    }
}
