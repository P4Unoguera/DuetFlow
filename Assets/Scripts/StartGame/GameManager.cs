using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// GameManager controlls the game flow once we are in Competition Mode
public class GameManager : MonoBehaviour
{
    // Static instance for other scripts to easily access the GameManager
    public static GameManager Instance;

    // References to two starting platforms
    public GameObject platform1;
    public GameObject platform2;

    // Texts that show the countdown on each platform
    public TextMeshProUGUI countdownTextPlatform1;
    public TextMeshProUGUI countdownTextPlatform2;

    // Sound to play when the game starts
    public AudioClip startSound;
    public AudioClip counterSound;
    // Audio source for playing sounds
    private AudioSource audioSource;

    // Flag indicating whether the game has started
    public bool startGame = false;

    // Flags to track if each player is ready (hidden in the inspector)
    [HideInInspector] public bool player1Ready = false;
    [HideInInspector] public bool player2Ready = false;

    // Internal state flags
    private bool experienceStarted = false; // Indicates if the experience has already started
    private float countdownTimer = 3f;      // Countdown starting at 3 seconds
    private bool countdownActive = false;   // Controls if countdown is currently running

    // Awake is called before the first frame update
    void Awake()
    {
        // Set the static instance reference
        Instance = this;

        // Get the AudioSource component and play the start sound once
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(startSound);
    }

    // Update is called once per frame
    void Update()
    {
        // If both players are ready, the experience has not started and countdown is not active...
        if (player1Ready && player2Ready && !experienceStarted && !countdownActive)
        {
            countdownActive = true; // Start the countdown
            countdownTimer = 3f; // Reset timer to 3 seconds
            audioSource.clip = counterSound;
            audioSource.Play();
            //audioSource.PlayOneShot(counterSound);
            // Show countdown UI on both platforms
            countdownTextPlatform1.gameObject.SetActive(true);
            countdownTextPlatform2.gameObject.SetActive(true);
        }

        // If one of the players is no longer ready...
        if (!player1Ready || !player2Ready)
        {
            countdownActive = false; // Cancel countdown
            countdownTimer = 3f; // Reset timer to 3 seconds
            audioSource.Stop();
            // Reset countdown UI on both platforms
            countdownTextPlatform1.text = "";
            countdownTextPlatform2.text = "";
            countdownTextPlatform1.gameObject.SetActive(false);
            countdownTextPlatform2.gameObject.SetActive(false);
        }

        // If countdown is active, we update the countdown logic
        if (countdownActive)
        {
            countdownTimer -= Time.deltaTime; // Decrease the timer based on real time

            string displayText = ""; // Initialize the display text

            // Set display text based on remaining time
            if (countdownTimer > 2f)
            {
                displayText = "3";

            } else if (countdownTimer > 1f)
            {
                displayText = "2";

            } else if (countdownTimer > 0f)
            {
                displayText = "1";

            } else if (countdownTimer > -1f)
            {
                displayText = "GO!";

            } else
            { // Countdown complete, hide texts
                countdownTextPlatform1.text = "";
                countdownTextPlatform2.text = "";
                countdownTextPlatform1.gameObject.SetActive(false);
                countdownTextPlatform2.gameObject.SetActive(false);

                // Disable both platforms
                platform1.SetActive(false);
                platform2.SetActive(false);

                // Start the game and mark experience as started
                startGame = true;
                experienceStarted = true;
                countdownActive = false;

                Debug.Log("Start Experience :)");
                return; // Exit Update to avoid further processing this frame
            }

            // Update the countdown display text
            countdownTextPlatform1.text = displayText;
            countdownTextPlatform2.text = displayText;
        }
    }
}
