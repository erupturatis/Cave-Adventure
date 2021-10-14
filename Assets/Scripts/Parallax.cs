using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private GameObject player;
    private Vector3 posInit;
    private Vector3 posPlayerInit;

    private Vector3 toMove;

    public float parallax;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        posInit = transform.position;
        posPlayerInit = player.transform.position;
    }


    void Update()
    {
        toMove = player.transform.position - posPlayerInit;
        transform.position = posInit + toMove * parallax;
    }
}
