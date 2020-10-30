using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public GameObject enemie;
    public Transform[] Spawners;
    public float spawnTime = 5f;
    public bool CanSpawn;
    void Start()
    {
        CanSpawn = false;
        //parentBox = transform.parent.gameObject.GetComponent<BoxCollider>();

        var Childrens = GetComponentsInChildren<Transform>();
        Spawners = new Transform[Childrens.Length];
        int i = 0;
        foreach (var child in Childrens)
        {
            Spawners[i] = child;
            i++;
        }

        StartCoroutine(Spawner());
    }
    private IEnumerator Spawner()
    {
        while (!CanSpawn)
        {
            yield return null;
        }
        while (CanSpawn)
        {
            yield return new WaitForSeconds(spawnTime);
            Instantiate(enemie, Spawners[Random.Range(0, Spawners.Length)].position, Quaternion.identity);
        }
        StartCoroutine( Spawner());
    }
}
