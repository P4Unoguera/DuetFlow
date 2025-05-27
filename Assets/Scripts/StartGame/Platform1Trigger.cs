using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is attached to a platform that detects when Player1 enters or exits a trigger collider
public class Platform1Trigger : MonoBehaviour
{
    // Reference to the Renderer component, used to change the platform's color
    private Renderer render;

    // Start method is called once when the script is initialized
    private void Start()
    {
        // Get the Renderer component attached to this GameObject
        render = GetComponent <Renderer>();
    }

    // OnTriggerEnter is automatically called when another collider enters this trigger
    private void OnTriggerEnter(Collider other)
    {
        // If the entering collider has the tag "Player1"...
        if (other.tag == "Player1")
        {
            // Mark Player1 as ready in the GameManager
            GameManager.Instance.player1Ready = true;
            // Change the platform's color to blue to indicate readiness
            render.material.color = Color.blue;
        }
    }

    // OnTriggerExit is called when another collider exits this trigger
    private void OnTriggerExit(Collider other)
    {
        // If the exiting collider has the tag "Player1"...
        if (other.tag == "Player1")
        {
            // Mark Player1 as not ready in the GameManager
            GameManager.Instance.player1Ready = false;
            // Change the platform's color back to white
            render.material.color = Color.white;
        }
    }
}
