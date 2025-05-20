using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonTrigger : MonoBehaviour
{
    public float buttonTime = 3f;

    private Vector3 originalPosition;
    private Renderer render;
    private bool isTriggered = false;
    private float timer = 0f;
    
    void Start()
    {
        originalPosition = transform.localPosition;
        render = GetComponent<Renderer>();
    }

    void Update()
    {
        if (isTriggered)
        {
            timer += Time.deltaTime;
            if (timer >= buttonTime)
            {
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

                isTriggered = false;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2") && !isTriggered)
        {
            transform.localPosition = originalPosition - new Vector3(0, 0.05f, 0);
            render.material.color = Color.grey;

            isTriggered = true;
            timer = 0f;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            transform.localPosition = originalPosition;
            render.material.color = Color.white;

            isTriggered = false;
            timer = 0f;
        }
    }
}
