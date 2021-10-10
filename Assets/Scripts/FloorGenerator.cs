using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject[] floorPrefabs;
    [SerializeField]
    private GameObject End;
    [SerializeField]
    private GameObject player;

    private int length;
    private bool playerEntered;
    void Start()
    {
        length = floorPrefabs.Length;
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        if(player.transform.position.x - transform.position.x > 75f)
        {
            Destroy(gameObject);
        }
        if(player.transform.position.x > transform.position.x)
        {
            SpawnNextChunk();
        }
    }
    public void SpawnNextChunk()
    {
        if (playerEntered == false)
        {
            playerEntered = true;
            Instantiate(floorPrefabs[Random.Range(0, length)], End.transform.position, Quaternion.identity);
        }
    }
}
