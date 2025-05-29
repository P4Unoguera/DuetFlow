using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// FallingNote class controlls the falling notes behaviour
public class FallingNote : MonoBehaviour
{
    // Speed of the note
    public float fallSpeed = 0.0f; // Set by spawner
    // Direction of the note
    public Vector3 dir = Vector3.zero;
    // Duration of the note
    private float duration;

    private bool scoring1 = false;
    private bool scoring2 = false;

    // Initialization of the note
    public void Initialize(float durationInSeconds)
    {
        duration = durationInSeconds;

        // Scale based on duration
        Vector3 scale = transform.localScale;
        scale.z = duration * 10;  // Adjust axis if needed
        transform.localScale = scale;
    }

    // Update is called once per frame
    void Update()
    {
        // Move the note in the specified direction, scaled by speed and frame time
        transform.position += dir * fallSpeed * Time.deltaTime;

        if (scoring1)
        {
            ScoreManager.Instance.AddScore1(1);
        }

        if (scoring2)
        {
            ScoreManager.Instance.AddScore2(1);
        }
    }

    // OnTriggerEnter is automatically called when another collider enters this trigger
    void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is a child of "Piano1" or "Piano2"
        if (other.transform.parent &&
            (other.transform.parent.CompareTag("Piano1") || other.transform.parent.CompareTag("Piano2")))
        {
            // Calculate the delay after which the note should be destroyed
            float despawnDelay = (duration * 10f) / fallSpeed;

            if (other.CompareTag("Active1"))
            {
                scoring1 = true;
            }

            if (other.CompareTag("Active2"))
            {
                scoring2 = true;
            }

            // Start coroutine to destroy the note after the calculated delay
            StartCoroutine(DespawnAfterDelay(despawnDelay));
        }
    }

    // Coroutine to destroy the note after a delay
    private IEnumerator DespawnAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}