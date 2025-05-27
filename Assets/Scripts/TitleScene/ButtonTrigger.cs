using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// ButtonTrigger class to manage a trigger-based button
public class ButtonTrigger : MonoBehaviour
{
    // Duration the button must be held before it triggers an action
    public float buttonTime = 3f;

    // Store the button's original position
    private Vector3 originalPosition;

    // Used to change the button's color
    private Renderer render;

    // Tracks if the button is currently being pressed
    private bool isTriggered = false;

    // Timer to track how long the button has been held
    private float timer = 0f;

    // Start method is called once when the script is initialized
    void Start()
    {
        // Store the initial local position of the button
        originalPosition = transform.localPosition;
        // Get the Renderer component attached to this GameObject
        render = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // If the button is being pressed...
        if (isTriggered)
        {
            // Increment the timer by the time passed since last frame
            timer += Time.deltaTime;

            // If the timer exceeds the required hold duration..
            if (timer >= buttonTime)
            {
                // Load the appropriate scene based on the object's tag
                if (CompareTag("Competition Mode"))
                {
                    SceneManager.LoadScene("Competition Mode");
                }
                else if (CompareTag("Menu"))
                {
                    SceneManager.LoadScene("Title");
                }
                else if (CompareTag("Quit Game"))
                {
                    Application.Quit();
                    Debug.Log("Game Quit");
                }
                // Reset trigger flag to prevent repeat activation
                isTriggered = false;
            }
        }
    }

    // OnTriggerEnter is automatically called when another collider enters this trigger
    void OnTriggerEnter(Collider other)
    {
        // If the collider belongs to Player1 or Player2 and the button is not already pressed
        if (other.CompareTag("Player1") || other.CompareTag("Player2") && !isTriggered)
        {
            // Move the button down slightly to simulate a press
            transform.localPosition = originalPosition - new Vector3(0, 0.05f, 0);
            // Change the button color to indicate it's pressed
            render.material.color = Color.grey;
            // Mark the button as triggered and reset the timer
            isTriggered = true;
            timer = 0f;
        }
    }

    // OnTriggerExit is called when another collider exits this trigger
    void OnTriggerExit(Collider other)
    {
        // If the collider is from Player1 or Player2...
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            // Move the button back to its original position
            transform.localPosition = originalPosition;
            // Reset the color to white
            render.material.color = Color.white;
            // Reset the button state and timer
            isTriggered = false;
            timer = 0f;
        }
    }
}
