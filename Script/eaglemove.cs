using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class eaglemove : enemies
{

    private Rigidbody2D rb;
    public Transform toppoint, bottompoint;
    public float speed;
    private float topy, bottomy;
    private bool isUp = true;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        topy = toppoint.position.y;
        bottomy = bottompoint.position.y;
        Destroy(toppoint.gameObject);
        Destroy(bottompoint.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        movement();
    }

    void movement()
    {
        if (isUp)
        {
            rb.velocity = new Vector2(rb.velocity.x, speed);
            if (rb.transform.position.y > topy)
            {
                isUp = false;
            }
        }      
        else 
        {
            rb.velocity = new Vector2(rb.velocity.x, -speed);
            if (rb.transform.position.y < bottomy)
            {
                isUp = true;
            }
        }

    }
}
