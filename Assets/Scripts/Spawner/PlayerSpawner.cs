using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private Transform playerParent;
    [SerializeField] private GameObject player;

    private void Start()
    {
        Instantiate(player, transform.position, Quaternion.identity, playerParent);
    }
}
