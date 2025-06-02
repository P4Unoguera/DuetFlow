using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// This script is attached to a platform that detects when Player1 enters or exits a trigger collider
public class Platform1Trigger : MonoBehaviour
{
    // Reference to the Renderer component, used to change the platform's color
    private Renderer render;
    public TextMeshProUGUI Text;

    // Start method is called once when the script is initialized
    private void Start()
    {
        // Get the Renderer component attached to this GameObject
        render = GetComponent <Renderer>();
        Text.gameObject.SetActive(false);
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
        // If the player is not the correct one...
        else if (other.tag == "Player2")
        {
            // Tell them
            Text.gameObject.SetActive(true);
            Text.text = "Wrong platform!\nGo to the other side.";
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
        // If the exiting collider has the tag "Player2"...
        else if (other.tag == "Player2")
        {
            Text.gameObject.SetActive(false);
        }
    }
}
