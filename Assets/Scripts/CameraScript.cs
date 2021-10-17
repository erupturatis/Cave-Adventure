using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraScript : MonoBehaviour
{

    public GameObject player;
    private float idealCameraSize;
    [SerializeField]
    private float sizeChangeSpeed;
    private CheckTopDown ck;
    [SerializeField]
    private Camera gameCamera;

    void Start()
    {
        ck = player.GetComponent<CheckTopDown>();
        
    }
    void HandleCameraSize()
    {
        float diff = idealCameraSize - gameCamera.orthographicSize;
        if(!(gameCamera.orthographicSize<6f && Mathf.Sign(diff) == -1) && Mathf.Abs(diff)>0.5f && !(gameCamera.orthographicSize > 16f && Mathf.Sign(diff) == 1))
        {
            gameCamera.orthographicSize = gameCamera.orthographicSize + Mathf.Sign(diff) * sizeChangeSpeed * Time.deltaTime;
        }

    }
    void Update()
    {
        idealCameraSize = ck.SizeCamera;
        HandleCameraSize();
    }
}
