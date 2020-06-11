using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class frogmove : enemies
{


    private Rigidbody2D rb;
   // private Animator anim;
    public Transform leftpoint, rightpoint;
    private bool faceleft = true;
    private float rightx, leftx;
    public float speed;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        transform.DetachChildren();
        leftx = leftpoint.position.x;
        rightx = rightpoint.position.x;
        Destroy(leftpoint.gameObject);
        Destroy(rightpoint.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        movement();
    }

    void movement()
    {
        anim.SetFloat("running", speed);
        if (faceleft)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            if (transform.position.x < leftx)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                faceleft = false;
            }
        }
        else
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            if (transform.position.x > rightx)
            {
                transform.localScale = new Vector3(1, 1, 1);
                faceleft = true;
            }
        }
    }

    
}
