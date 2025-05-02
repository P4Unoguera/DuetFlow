using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform2Trigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player2")
        {
            GameManager.Instance.player2Ready = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player2")
        {
            GameManager.Instance.player2Ready = false;
        }
    }
}
