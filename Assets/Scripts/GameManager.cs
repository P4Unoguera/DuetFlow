using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [HideInInspector]
    public bool player1Ready = false;

    [HideInInspector]
    public bool player2Ready = false;

    private bool experienceStarted = false;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(player1Ready && player2Ready && !experienceStarted)
        {
            experienceStarted = true;
            Debug.Log("Start Experience :)");       // Escena nova, animació, etc,
        }
    }
}
