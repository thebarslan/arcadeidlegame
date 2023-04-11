using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class NpcSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] npc = new GameObject[3];
    private Vector3 destinationPoint;
    private int charIndex;
    private void Start()
    {
        StartCoroutine(SpawnNPC());
    }

    private IEnumerator SpawnNPC()
    {
        while (true)
        {
            setDestination();
            setChar();
            int roadSelection = Random.Range(0, 10);
            if (roadSelection < 3)
            {
                destinationPoint = new Vector3(80f, 0f, 0f);
            }
            else
            {
                destinationPoint = new Vector3(47f, 0f, -22f);
            }
            var obj = Instantiate(npc[charIndex].gameObject, transform.position, Quaternion.identity, transform);
            obj.GetComponent<NavMeshAgent>().SetDestination(destinationPoint.normalized);
            obj.GetComponent<Animator>().SetBool("isWalking", true);
            
            yield return new WaitForSeconds(5f);
        }
    }

    private void setDestination()
    {
        
    }

    private void setChar()
    {
        int charSelection = Random.Range(0, 100);
        if (charSelection < 40)
        {
            charIndex = 0;
        }else if (charSelection is > 40 and < 80)
        {
            charIndex = 1;
        }else
        {
            charIndex = 2;
        }
    }
}
