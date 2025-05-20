using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject platform1;
    public GameObject platform2;

    public bool startGame = false;

    [HideInInspector] public bool player1Ready = false;
    [HideInInspector] public bool player2Ready = false;

    private bool experienceStarted = false;
    private float startTimer = 0f;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (player1Ready && player2Ready && !experienceStarted)
        {
            startTimer += Time.deltaTime;

            if (startTimer >= 4)
            {
                platform1.SetActive(false);
                platform2.SetActive(false);

                experienceStarted = true;
                startGame = true;
                Debug.Log("Start Experience :)");
            }
        }

        else
        {
            startTimer = 0f;    // es resetea el timer si algun player surt de la plataforma
        }
    }
}
