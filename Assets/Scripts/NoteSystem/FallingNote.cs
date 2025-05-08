using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingNote : MonoBehaviour
{
    public float fallSpeed = 0.0f; // Set externally by the spawner
    public Vector3 dir = Vector3.zero;

    private float duration;

    // Called by the spawner to initialize the note
    public void Initialize(float durationInSeconds)
    {
        duration = durationInSeconds;

        // OPTIONAL: Scale the note's length (along Z, or change to Y if needed)
        Vector3 scale = transform.localScale;
        scale.z = duration * 10;  // Adjust axis as needed
        transform.localScale = scale;
    }

    void Update()
    {
        // Move the note each frame
        transform.position +=  dir * fallSpeed * Time.deltaTime;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent)
        {
            Transform parent = other.transform.parent;

            if (parent.CompareTag("Piano1") || parent.CompareTag("Piano2"))
            {
                Destroy(gameObject);
            }
        }
    }
}
