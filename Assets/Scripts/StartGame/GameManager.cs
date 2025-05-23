using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject platform1;
    public GameObject platform2;

    public TextMeshProUGUI countdownTextPlatform1;
    public TextMeshProUGUI countdownTextPlatform2;

    public bool startGame = false;

    [HideInInspector] public bool player1Ready = false;
    [HideInInspector] public bool player2Ready = false;

    private bool experienceStarted = false;
    private float countdownTimer = 3f;
    private bool countdownActive = false;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (player1Ready && player2Ready && !experienceStarted && !countdownActive)
        {
            countdownActive = true;
            countdownTimer = 3f;
            countdownTextPlatform1.gameObject.SetActive(true);
            countdownTextPlatform2.gameObject.SetActive(true);
        }

        if (!player1Ready || !player2Ready)
        {
            countdownActive = false;
            countdownTimer = 3f;
            countdownTextPlatform1.text = "";
            countdownTextPlatform2.text = "";
            countdownTextPlatform1.gameObject.SetActive(false);
            countdownTextPlatform2.gameObject.SetActive(false);
        }

        if (countdownActive)
        {
            countdownTimer -= Time.deltaTime;

            string displayText = "";

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
            {
                countdownTextPlatform1.text = "";
                countdownTextPlatform2.text = "";
                countdownTextPlatform1.gameObject.SetActive(false);
                countdownTextPlatform2.gameObject.SetActive(false);

                platform1.SetActive(false);
                platform2.SetActive(false);

                startGame = true;
                experienceStarted = true;
                countdownActive = false;

                Debug.Log("Start Experience :)");
                return;
            }

            countdownTextPlatform1.text = displayText;
            countdownTextPlatform2.text = displayText;
        }
    }
}
