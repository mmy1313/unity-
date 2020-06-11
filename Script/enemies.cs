using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemies : MonoBehaviour
{
    protected Animator anim;
    protected AudioSource deathAudio; 
    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        deathAudio = GetComponent<AudioSource>();
    }

    public void death()
    {
        
        Destroy(gameObject);
    }
    public void jumpon()
    {
        GetComponent<Collider2D>().enabled = false;
        anim.SetTrigger("death");
        deathAudio.Play();
    }
}
