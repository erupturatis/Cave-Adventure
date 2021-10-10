using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraScript : MonoBehaviour
{
    public GameObject player;

    public float Ydis;
    public float Xdis;

    private float idealCameraSize;
    [SerializeField]
    private float sizeChangeSpeed;

    private CheckTopDown ck;
    private Camera cameraComp;

    void Start()
    {
        ck = player.GetComponent<CheckTopDown>();
        cameraComp = GetComponent<Camera>();
    }
    void HandleCameraSize()
    {
        float diff = idealCameraSize - cameraComp.orthographicSize;

        
        if(!(cameraComp.orthographicSize<6f && Mathf.Sign(diff) == -1) && Mathf.Abs(diff)>0.5f   )
        {
            cameraComp.orthographicSize = cameraComp.orthographicSize + Mathf.Sign(diff) * sizeChangeSpeed * Time.deltaTime;
        }
        print(cameraComp.orthographicSize);
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 pos = new Vector3(player.transform.position.x + Xdis, player.transform.position.y + Ydis, -10f);
        idealCameraSize = ck.SizeCamera;
        HandleCameraSize();

        
        transform.position = pos;
    }
}
