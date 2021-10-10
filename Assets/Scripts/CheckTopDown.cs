using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTopDown : MonoBehaviour
{
    [SerializeField]
    private LayerMask whatIsGround;
    [HideInInspector]
    public float SizeCamera;


    private void SlopeCheckVertical(Vector2 checkPos)
    {
        RaycastHit2D hitBottom = Physics2D.Raycast(checkPos, Vector2.down, 1000f, whatIsGround);
        RaycastHit2D hitTop = Physics2D.Raycast(checkPos, Vector2.up, 1000f, whatIsGround);

        SizeCamera = 6.7f / 10.5f * (hitTop.distance + hitBottom.distance );
    }
    private void Update()
    {
        SlopeCheckVertical(gameObject.transform.position);
        //print(SizeCamera);
    }
}
