using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
public class BulletController : MonoBehaviour
{


    [DllImport("EntityComponentSystemChallenge")]
    public static extern void UpdateComponent(IntPtr component);

    [DllImport("EntityComponentSystemChallenge")]
    public static extern int GetScore(IntPtr component);

    private IntPtr entityComponent;

    public float speed;
    public float screenHeight;



    // Use this for initialization
    void Start()
    {
        entityComponent = GameObject.FindWithTag("Player").GetComponent<PlayerController>().myEntityComponent;
    }

    // Update is called once per frame
    void Update()
    {
        if ((GameController.GamePlaying) && (!UIManager.isVisible))
        {
            transform.position += Vector3.up * speed;
        }


    }

    private void _checkBounds()
    {
        if (transform.position.y > screenHeight)
        {
            Destroy(this.gameObject);
        }
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if ((GameController.GamePlaying) && (!UIManager.isVisible))
        {
            /* These conditional statements are separate to allow for
              allocating a variant point value for each enemy type hit */ 

            if (other.tag == "Enemy")
            {
                UpdateComponent(entityComponent);
                Score.scoreVal = GetScore(entityComponent);
                Destroy(other.gameObject);
                StartCoroutine(_destroyBullet());

                /* CPP Code goes here */
            }

            if (other.name == "Centipede_Head")
            {
                UpdateComponent(entityComponent);
                Score.scoreVal = GetScore(entityComponent);
                Destroy(other.gameObject);
                StartCoroutine(_destroyBullet());
            }

            if(other.name == "Centipede_Body")
            {
                UpdateComponent(entityComponent);
                Score.scoreVal = GetScore(entityComponent);
                Destroy(other.gameObject);
                StartCoroutine(_destroyBullet());
            }

            if ((other.tag == "Grid") || (other.tag == "Canvas"))
            {
                 Destroy(this.gameObject);
            }
        }
    }

    private IEnumerator _destroyBullet() 
    {
        GameController.enemyHitSound.Play();
        yield return new WaitForSeconds(0.01f);
        Destroy(this.gameObject);
    }
}
