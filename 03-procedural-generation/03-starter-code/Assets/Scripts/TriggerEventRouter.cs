﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEventRouter : MonoBehaviour
{
    //public UnityEvent OnPlayerEntered = new UnityEvent();
    //public UnityEvent OnPlayerExited = new UnityEvent();

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            GameSetup.Instance.GameLose();
        }
        else if (other.CompareTag("Treasure"))
        {
            GameSetup.Instance.GameWon();
        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //    }
    //}
}
