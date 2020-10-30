using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    private int characterCount = 0;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Character")
        {
            characterCount++;
               SpawnEnemies spawner = GetComponentInChildren<SpawnEnemies>();
            spawner.CanSpawn = true;
        }
    }
}
