using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoKey : MonoBehaviour
{
    private Color pressedColor1 = Color.blue;
    private Color pressedColor2 = Color.red;
    public float pressDepth = 0.05f;
    public float fadeOutDuration = 0.5f;

    private Vector3 originalPosition;
    private Color originalColor;
    private Renderer rend;
    //private bool isPressed = false;
    private AudioSource audioSource;
    private Coroutine fadeCoroutine;

    public string keyIndex;

    void Start()
    {
        originalPosition = transform.localPosition;
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1") && transform.parent.tag == "Piano1")
        {
            PressKey1();
            if (audioSource != null && !audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }

        if (other.CompareTag("Player2") && transform.parent.tag == "Piano2")
        {
            PressKey2();
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

        if (other.CompareTag("Player2"))
        {
            ReleaseKey();
            if (audioSource != null && audioSource.isPlaying)
            {
                fadeCoroutine = StartCoroutine(FadeOut(audioSource, fadeOutDuration));
            }
        }
    }

    void PressKey1()
    {
        //isPressed = true;
        transform.localPosition = originalPosition - new Vector3(0, pressDepth, 0);
        rend.material.color = pressedColor1;
    }

    void PressKey2()
    {
        //isPressed = true;
        transform.localPosition = originalPosition - new Vector3(0, pressDepth, 0);
        rend.material.color = pressedColor2;
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
