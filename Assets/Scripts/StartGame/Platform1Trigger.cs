using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform1Trigger : MonoBehaviour
{
    private Renderer render;

    private void Start()
    {
        render = GetComponent <Renderer>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player1")
        {
            GameManager.Instance.player1Ready = true;
            render.material.color = Color.blue;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player1")
        {
            GameManager.Instance.player1Ready = false;
            render.material.color = Color.white;
        }
    }
}
