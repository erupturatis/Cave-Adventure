using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Movement : MonoBehaviour
{
    bool IsOnGround = false;

    Rigidbody2D rb;

    public Transform Down;
    public Transform Forward;
    public Transform Up;

    [SerializeField]
    private float force;
    [SerializeField]
    private float ForceJump;
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private float slopeCheckDistance;
    private float slopeDownAngle;

    private Vector2 newVelocity;
    private Vector2 slopeNormalPerp;

    [SerializeField]
    private LayerMask whatIsGround;

    BoxCollider2D bx;
    
    Vector2 colliderSize;

    [SerializeField]
    private PhysicsMaterial2D noFriction;
    [SerializeField]
    private PhysicsMaterial2D fullFriction;

    Quaternion Q;

    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        bx = GetComponent<BoxCollider2D>();
        colliderSize = bx.size;
        rb.sharedMaterial = noFriction;
    }

    private void SlopeCheck()
    {
        Vector2 checkPos = transform.position;
        SlopeCheckVertical(checkPos);

    }

    private void SlopeCheckVertical(Vector2 checkPos)
    {
        RaycastHit2D hit = Physics2D.Raycast(checkPos, Vector2.down, slopeCheckDistance,whatIsGround);
        if (hit)
        {
            slopeNormalPerp = Vector2.Perpendicular(hit.normal).normalized;
            Q = Quaternion.Euler(Quaternion.FromToRotation(Vector3.up, hit.normal).eulerAngles); // calculating the angle of the slope

            slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);

            Debug.DrawRay(hit.point, slopeNormalPerp, Color.red);
            Debug.DrawRay(hit.point, hit.normal, Color.green);
        }

    }


    private void ApplyMovement()
    {
        SlopeCheck();
        if (Input.GetKey("w") && IsOnGround)
        {
            Vector2 NewForce = new Vector2(transform.position.x - Up.position.x, transform.position.y - Up.position.y);
            rb.AddForce(NewForce * -ForceJump, ForceMode2D.Impulse);
            IsOnGround = false;
        }
        
        if (IsOnGround)
        {

            gameObject.transform.rotation = Q; // gameobject has the same angle as the slope
            newVelocity.Set(Forward.position.x - gameObject.transform.position.x, Forward.position.y - gameObject.transform.position.y);
            rb.velocity = newVelocity * force;
            Vector3 V = new Vector3(Down.transform.position.x - transform.position.x, Down.transform.position.y - transform.position.y, 0f);
            rb.AddForce(V * force * 3); // add force down to keep player on track
        }

    }

    private void RotateMidAir()
    {
        if (IsOnGround)
        {
            return;
        }
        //Debug.Log(gameObject.transform.rotation.eulerAngles.z + " " + Q.eulerAngles.z  + "   " + rb.rotation);
        float direction = (transform.rotation.eulerAngles.z-Q.eulerAngles.z);
        if (Mathf.Abs(direction) > 180f)
        {
            direction = -direction;
        }
        direction = -direction;
        rb.MoveRotation(transform.rotation.eulerAngles.z + Mathf.Sign(direction) * rotationSpeed*Time.fixedDeltaTime);

    }


    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        ApplyMovement();
        RotateMidAir();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            IsOnGround = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            IsOnGround = false;
        }
    }
}
