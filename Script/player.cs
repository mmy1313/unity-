using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    public Transform cellingcheck,groundcheck;
    public Collider2D coll;
    public Collider2D discoll;
    public LayerMask ground;
    public float speed;
    public float jumpforce;
    public int cherry;
    public Text cherrynum;
    private bool isHurt,isground;
    public AudioSource jumpAudio,hurtAudio,cherryAudio;
    private int jump2;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

   
    void FixedUpdate()
    {
        if(!isHurt)
        {
            Movement();
        }
       
        SwitchAnim();
        //判断是否和地面有接触
        isground = Physics2D.OverlapCircle(groundcheck.position,0.2f,ground);
    }
    private void Update()
    {
        Jump();
        Crouch();
    }
    //角色移动
    void Movement()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        float facediretion = Input.GetAxisRaw("Horizontal");

        //角色移动
        if (horizontalMove!=0)
        {
            rb.velocity = new Vector2(horizontalMove * speed * Time.fixedDeltaTime, rb.velocity.y);
            //角色动画变为跑
            anim.SetFloat("running", math.abs(facediretion));
        }

        //角色转向
        if (facediretion != 0)
        {
            transform.localScale = new Vector3(facediretion, 1, 1);
        }
       
        
    }

    //角色跳跃
    /*void Jump()
    {
        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))
        {
            jumpAudio.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpforce);
            //角色动画变为跳
            anim.SetBool("jumping", true);
            anim.SetBool("falling", false);
        }
    }*/

    void Jump()
    {
        if (isground)
        {
            jump2 = 1;
        }
        if(Input.GetButtonDown("Jump")&& jump2 > 0)
        {
            jumpAudio.Play();
            rb.velocity = Vector2.up * jumpforce;
            anim.SetBool("jumping", true);
            anim.SetBool("falling", false);
            jump2--;
        }
    }

    //角色下蹲
    void Crouch()
    {
        if (!Physics2D.OverlapCircle(cellingcheck.position, 0.2f, ground))
        {
            if (Input.GetButton("Crouch"))
            {
                anim.SetBool("crouching", true);
                discoll.enabled = false;
            }
            else
            {
                anim.SetBool("crouching", false);
                discoll.enabled = true;
            }
        }
    }

    //控制角色动画
    void SwitchAnim()
    {
        //anim.SetBool("idle", false);
        //不在跳跃且没碰到地面
        if (rb.velocity.y<0.1f&& !coll.IsTouchingLayers(ground))
        {
            anim.SetBool("falling", true);
        }
        //角色动画从跳变为下落
        if (anim.GetBool("jumping"))
        {
            if (rb.velocity.y < 0)
            {
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
            }
        }
        //角色动画从下落变为站立
         if (coll.IsTouchingLayers(ground))
        {
            anim.SetBool("falling", false);
            //anim.SetBool("idle", true);
        }
         //角色受伤
         if (isHurt)
        {
            //Debug.Log(math.abs(rb.velocity.x));
            hurtAudio.Play();
            anim.SetBool("hurt",true);
            if (math.abs(rb.velocity.x) < 5)
            {
                anim.SetBool("hurt", false);
                //anim.SetBool("idle", true);
                isHurt = false;
               
            }
        }
    }
    //吃东西
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "collection")
        {
            cherryAudio.Play();
            Destroy(collision.gameObject);
            cherry += 1;
            cherrynum.text = cherry.ToString();
        }
    }

    //消灭怪物
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemies")
        {
            //frogmove frog = collision.gameObject.GetComponent<frogmove>();
            enemies enemies = collision.gameObject.GetComponent<enemies>();
            if (anim.GetBool("falling"))
            {
                //Debug.Log("1");
                enemies.jumpon();
                rb.velocity = new Vector2(rb.velocity.x, jumpforce);
                anim.SetBool("jumping", true);
            }else if (transform.position.x<collision.gameObject.transform.position.x)
            {
                //Debug.Log("2");
                rb.velocity = new Vector2(-10, rb.velocity.y);
                isHurt = true;
            }else if (transform.position.x > collision.gameObject.transform.position.x)
            {
                //Debug.Log("3");
                rb.velocity = new Vector2(10, rb.velocity.y);
                isHurt = true;
            }
        }
        
    }
}
