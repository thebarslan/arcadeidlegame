using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Source"))
        {
            other.GetComponent<Source>().CoroutineStart();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Source"))
        {
            other.GetComponent<Source>().CoroutineStop();
        }
    }
}
