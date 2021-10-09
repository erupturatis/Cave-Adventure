using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject[] floorPrefabs;
    [SerializeField]
    private GameObject End;
    private GameObject Player;
    private int length;
    private bool playerEntered;
    void Start()
    {
        length = floorPrefabs.Length;
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        if(Player.transform.position.x - transform.position.x > 75f)
        {
            Destroy(gameObject);
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
