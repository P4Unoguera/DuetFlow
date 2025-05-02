using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform1Trigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player1")
        {
            GameManager.Instance.player1Ready = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player1")
        {
            GameManager.Instance.player1Ready = false;
        }
    }
}
