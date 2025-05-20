using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform2Trigger : MonoBehaviour
{
    private Renderer render;

    private void Start()
    {
        render = GetComponent<Renderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player2")
        {
            GameManager.Instance.player2Ready = true;
            render.material.color = Color.red;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player2")
        {
            GameManager.Instance.player2Ready = false;
            render.material.color = Color.white;
        }
    }
}
