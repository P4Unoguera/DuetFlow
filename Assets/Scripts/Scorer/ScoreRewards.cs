using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreRewards : MonoBehaviour
{
    private AudioSource audioSource;
    private ParticleSystem particleEvent;
    public TextMeshProUGUI Text;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        particleEvent = GetComponent<ParticleSystem>();
        Text.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Reward1")
        {
            StartCoroutine(ShowMessage("Nice!"));
            particleEvent.Play();
            audioSource.Play();
        }

        if (other.name == "Reward2")
        {
            StartCoroutine(ShowMessage("Amazing!"));
            particleEvent.Play();
            audioSource.Play();
        }

        if (other.name == "Reward3")
        {
            StartCoroutine(ShowMessage("Incredible!"));
            particleEvent.Play();
            audioSource.Play();
        }
    }

    private IEnumerator ShowMessage(string message)
    {
        Text.gameObject.SetActive(true);
        Text.text = message;
        yield return new WaitForSeconds(2f);
        Text.gameObject.SetActive(false);
    }

}
