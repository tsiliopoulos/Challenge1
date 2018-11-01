using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    public float speed;
    public float screenHeight;

    // Use this for initialization
    void Start()
    {

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
                Destroy(other.gameObject);
                StartCoroutine(_destroyBullet());

                /* CPP Code goes here */
            }

            if (other.name == "Centipede_Head")
            {
                Destroy(other.gameObject);
                StartCoroutine(_destroyBullet());
            }

            if(other.name == "Centipede_Body") {
                Destroy(other.gameObject);
                StartCoroutine(_destroyBullet());
            }

            if (other.tag == "Grid")
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
