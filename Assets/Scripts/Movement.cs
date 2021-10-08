using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody2D rb;
    public Transform Down;
    public Transform Forward;
    public Transform Up;
    public float force = 6f;
    public float ForceJump = 5f;
    bool IsOnGround = false;

    private Vector2 NewVelocity;
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {

        //print(force);
        if (Input.GetKeyDown("w") && IsOnGround)
        {
            print("jumpuit");
            NewVelocity.Set(0f,0f);
            //rb.velocity = NewVelocity;
            Vector2 NewForce = new Vector2( transform.position.x - Up.position.x , transform.position.y - Up.position.y);
            rb.AddForce(NewForce * -ForceJump , ForceMode2D.Impulse);
            IsOnGround = false;
        }
        if (IsOnGround)
        {
            NewVelocity.Set(Forward.position.x - gameObject.transform.position.x, Forward.position.y - gameObject.transform.position.y);
            rb.velocity = NewVelocity * force;
        }
        
        if (IsOnGround)
        {
            //print("a inceput");
            rb.gravityScale = 0;
            Vector3 direction = Down.position - transform.position;
            rb.AddForce(direction * force );
            
        }
        else
        {
            rb.gravityScale = 1;

        }
        //print(IsOnGround);
        
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
