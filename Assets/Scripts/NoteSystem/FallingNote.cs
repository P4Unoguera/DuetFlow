using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingNote : MonoBehaviour
{
    public float fallSpeed = 0.0f; // Set by spawner
    public Vector3 dir = Vector3.zero;

    private float duration;

    // Called by the spawner
    public void Initialize(float durationInSeconds)
    {
        duration = durationInSeconds;

        // Scale based on duration
        Vector3 scale = transform.localScale;
        scale.z = duration * 10;  // Adjust axis if needed
        transform.localScale = scale;
    }

    void Update()
    {
        // Move note
        transform.position += dir * fallSpeed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent &&
            (other.transform.parent.CompareTag("Piano1") || other.transform.parent.CompareTag("Piano2")))
        {
            float despawnDelay = (duration * 10f) / fallSpeed;
            StartCoroutine(DespawnAfterDelay(despawnDelay));
        }
    }

    private IEnumerator DespawnAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}