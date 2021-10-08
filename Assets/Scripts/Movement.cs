using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody2D rb;
    public Transform Down;
    public Transform Forward;
    public float force = 6f;
    bool IsOnGround = false;
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {

        //print(force);
        if (Input.GetKey("d"))
        {
            //print("apasa");
            Vector3 direction = Forward.position-gameObject.transform.position;
            rb.AddForce(direction * force);
        }
        if (Input.GetKeyDown("w"))
        {
            //print("ceva");
            Vector3 Up = new Vector3(0f, 1f, 0f);
            rb.AddForce(Up * force , ForceMode2D.Impulse);
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
            rb.gravityScale = 10;

        }
        //print(IsOnGround);
        
    }

    private void OnCollisionStay2D(Collision2D collision)
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
